using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AdvancedToDoList.Models;
using AdvancedToDoList.Properties;

namespace AdvancedToDoList
{
    public partial class ProjectDetailForm : Form
    {
        private Project _project;
        private ProjectDataStore _dataStore;

        public event EventHandler ProjectChanged;

        public ProjectDetailForm(Project project, ProjectDataStore dataStore)
        {
            InitializeComponent();
            _project = project;
            _dataStore = dataStore;

            // Läs in sparad storlek
            this.StartPosition = FormStartPosition.Manual;
            this.Width = Settings.Default.ProjFormWidth;
            this.Height = Settings.Default.ProjFormHeight;

            // Läs in sparade splitter-avstånd
            splitContainerMain.SplitterDistance = Settings.Default.ProjFormSplitterMainDist;
            splitContainerTasks.SplitterDistance = Settings.Default.ProjFormSplitterTasksDist;
            splitContainerParts.SplitterDistance = Settings.Default.ProjFormSplitterPartsDist;

            // Fyll inledande fält
            txtTitle.Text = _project.Title;
            txtDescription.Text = _project.Description;
            RefreshFileList();
            LoadTasksGrid();

            if (dgvTasks.Rows.Count > 0)
                dgvTasks.Rows[0].Selected = true;

            // Koppla event för part‐lista
            dgvTaskParts.SelectionChanged += dgvTaskParts_SelectionChanged;
            dgvTaskParts.CellValueChanged += DgvTaskParts_CellValueChanged;
            dgvTaskParts.CellEndEdit += DgvTaskParts_CellEndEdit;

            // Nytt: koppla TextChanged på rtbPartDescription så att beskrivningen sparas direkt
            rtbPartDescription.TextChanged += rtbPartDescription_TextChanged;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Spara aktuell storlek
            Settings.Default.ProjFormWidth = this.Width;
            Settings.Default.ProjFormHeight = this.Height;

            // Spara aktuella splitter-avstånd
            Settings.Default.ProjFormSplitterMainDist = splitContainerMain.SplitterDistance;
            Settings.Default.ProjFormSplitterTasksDist = splitContainerTasks.SplitterDistance;
            Settings.Default.ProjFormSplitterPartsDist = splitContainerParts.SplitterDistance;

            Settings.Default.Save();
        }

        // =========================
        //  Filhantering
        // =========================

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Title = "Välj fil att bifoga";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _project.AttachedFilePaths.Add(ofd.FileName);
                    RefreshFileList();
                    ProjectChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0) return;
            var selected = lvFiles.SelectedItems[0];
            var path = selected.SubItems[1].Text;
            _project.AttachedFilePaths.Remove(path);
            RefreshFileList();
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RefreshFileList()
        {
            lvFiles.Items.Clear();
            foreach (var path in _project.AttachedFilePaths)
            {
                var fi = new System.IO.FileInfo(path);
                var item = new ListViewItem(new string[] { fi.Name, path });
                lvFiles.Items.Add(item);
            }
        }

        // =========================
        //  Task‐sektion
        // =========================

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            var newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Ny uppgift",
                AssignedTo = new List<Person>(),
                Parts = new List<TaskPart>(),
                Comments = new List<Comment>()
            };
            _project.Tasks.Add(newTask);

            using (var form = new TaskDetailForm(newTask, _dataStore))
            {
                form.TaskChanged += (s, ev) =>
                {
                    foreach (var pers in newTask.AssignedTo)
                        if (!_dataStore.AllPersons.Any(p => p.Id == pers.Id))
                            _dataStore.AllPersons.Add(pers);

                    ProjectChanged?.Invoke(this, EventArgs.Empty);
                };
                form.ShowDialog();
            }

            // Ladda om grid, markera ny rad, ladda dess delar
            LoadTasksGrid();
            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                if (row.Tag is TaskItem t && t.Id == newTask.Id)
                {
                    row.Selected = true;
                    LoadTaskParts(newTask);
                    break;
                }
            }
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;
            var selectedRow = dgvTasks.SelectedRows[0];
            var task = (TaskItem)selectedRow.Tag;

            using (var form = new TaskDetailForm(task, _dataStore))
            {
                form.TaskChanged += (s, ev) =>
                {
                    foreach (var pers in task.AssignedTo)
                        if (!_dataStore.AllPersons.Any(p => p.Id == pers.Id))
                            _dataStore.AllPersons.Add(pers);

                    ProjectChanged?.Invoke(this, EventArgs.Empty);
                };
                form.ShowDialog();
            }

            // Ladda om grid, markera redigerad rad, ladda dess delar
            LoadTasksGrid();
            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                if (row.Tag is TaskItem t && t.Id == task.Id)
                {
                    row.Selected = true;
                    LoadTaskParts(task);
                    break;
                }
            }
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnRemoveTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;
            var selectedRow = dgvTasks.SelectedRows[0];
            var task = (TaskItem)selectedRow.Tag;
            _project.Tasks.Remove(task);
            ClearTaskDetail();
            LoadTasksGrid();
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadTasksGrid()
        {
            dgvTasks.Columns.Clear();
            dgvTasks.Rows.Clear();

            dgvTasks.Columns.Add("colTaskTitle", "Titel");
            dgvTasks.Columns.Add("colTaskAssignee", "Tilldelad");

            foreach (var task in _project.Tasks)
            {
                int rowIndex = dgvTasks.Rows.Add();
                var row = dgvTasks.Rows[rowIndex];
                row.Cells["colTaskTitle"].Value = task.Title;
                row.Cells["colTaskAssignee"].Value = string.Join(", ", task.AssignedTo.Select(p => p.Name));
                row.Tag = task;
            }
        }

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                ClearTaskDetail();
                return;
            }

            var selectedRow = dgvTasks.SelectedRows[0];
            var task = (TaskItem)selectedRow.Tag;
            LoadTaskParts(task);
        }

        private void ClearTaskDetail()
        {
            dgvTaskParts.Columns.Clear();
            dgvTaskParts.Rows.Clear();
            rtbPartDescription.Clear();
        }

        private void LoadTaskParts(TaskItem task)
        {
            if (task == null) return;
            if (task.Parts == null)
                task.Parts = new List<TaskPart>();

            dgvTaskParts.Columns.Clear();
            dgvTaskParts.Rows.Clear();

            dgvTaskParts.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colPartDone", HeaderText = "Färdig?", Width = 60 });
            dgvTaskParts.Columns.Add("colPartTitle", "Delmoment");

            foreach (var part in task.Parts)
            {
                int idx = dgvTaskParts.Rows.Add(part.IsCompleted, part.Title);
                dgvTaskParts.Rows[idx].Tag = part;
            }
        }

        private void dgvTaskParts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTaskParts.SelectedRows.Count == 0)
            {
                rtbPartDescription.Clear();
                return;
            }
            var selectedPart = (TaskPart)dgvTaskParts.SelectedRows[0].Tag;
            if (selectedPart != null)
                rtbPartDescription.Text = selectedPart.Description;
        }

        private void DgvTaskParts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            var part = (TaskPart)dgvTaskParts.Rows[e.RowIndex].Tag;
            part.IsCompleted = Convert.ToBoolean(dgvTaskParts.Rows[e.RowIndex].Cells[0].Value);
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DgvTaskParts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 1) return;
            var row = dgvTaskParts.Rows[e.RowIndex];
            var changedPart = (TaskPart)row.Tag;
            var newTitle = row.Cells[1].Value?.ToString().Trim() ?? "";
            if (changedPart.Title != newTitle)
            {
                changedPart.Title = newTitle;
                ProjectChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // =========================
        //  Del‐knappar i ProjectDetailForm
        // =========================

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;
            var task = (TaskItem)dgvTasks.SelectedRows[0].Tag;
            if (task.Parts == null)
                task.Parts = new List<TaskPart>();

            var part = new TaskPart
            {
                Id = Guid.NewGuid(),
                Title = "Ny del",
                IsCompleted = false,
                Description = ""
            };
            task.Parts.Add(part);
            LoadTaskParts(task);

            // Markera den nya raden
            foreach (DataGridViewRow row in dgvTaskParts.Rows)
            {
                if (row.Tag is TaskPart p && p.Id == part.Id)
                {
                    row.Selected = true;
                    break;
                }
            }
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnRemovePart_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;
            var task = (TaskItem)dgvTasks.SelectedRows[0].Tag;
            if (dgvTaskParts.SelectedRows.Count == 0) return;

            var part = (TaskPart)dgvTaskParts.SelectedRows[0].Tag;
            task.Parts.Remove(part);
            LoadTaskParts(task);
            rtbPartDescription.Clear();
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        // =========================
        //  Direkt sparande av beskrivning
        // =========================

        private void rtbPartDescription_TextChanged(object sender, EventArgs e)
        {
            if (dgvTaskParts.SelectedRows.Count == 0) return;
            var part = (TaskPart)dgvTaskParts.SelectedRows[0].Tag;
            if (part != null)
            {
                part.Description = rtbPartDescription.Text;
                ProjectChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // =========================
        //  Spara/avbryt/ta‐bort projekt
        // =========================

        private void btnSave_Click(object sender, EventArgs e)
        {
            _project.Title = txtTitle.Text.Trim();
            _project.Description = txtDescription.Text.Trim();

            // Kan tas bort, eftersom vi redan sparar i realtid:
            // if (dgvTaskParts.SelectedRows.Count > 0)
            // {
            //     var selPart = (TaskPart)dgvTaskParts.SelectedRows[0].Tag;
            //     selPart.Description = rtbPartDescription.Text.Trim();
            // }

            ProjectChanged?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteProject_Click(object sender, EventArgs e)
        {
            var msg = $"Är du säker på att du vill ta bort projektet \"{_project.Title}\"?";
            var result = MessageBox.Show(msg, "Bekräfta borttagning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _dataStore.AllProjects.Remove(_project);
                ProjectChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }
    }
}
