using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvancedToDoList.Models;

namespace AdvancedToDoList
{
    public partial class TaskDetailForm : Form
    {
        private TaskItem _task;
        private ProjectDataStore _dataStore;

        public event EventHandler TaskChanged;

        public TaskDetailForm(TaskItem task, ProjectDataStore dataStore)
        {
            InitializeComponent();
            _task = task;
            _dataStore = dataStore;

            // Sätt in grundvärden
            txtTaskTitle.Text = _task.Title;
            InitializePersonComboBox();
            RefreshAssignedList();
        }

        private void InitializePersonComboBox()
        {
            var source = new AutoCompleteStringCollection();
            source.AddRange(_dataStore.AllPersons.Select(p => $"{p.Name} <{p.Email}>").ToArray());
            cmbPersons.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPersons.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbPersons.AutoCompleteCustomSource = source;
        }

        private void RefreshAssignedList()
        {
            lbAssigned.Items.Clear();
            foreach (var pers in _task.AssignedTo)
            {
                lbAssigned.Items.Add($"{pers.Name} <{pers.Email}>");
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var text = cmbPersons.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            string name = text, email = text;
            if (text.Contains("<") && text.Contains(">"))
            {
                int start = text.IndexOf('<');
                int end = text.IndexOf('>');
                name = text.Substring(0, start).Trim();
                email = text.Substring(start + 1, end - start - 1).Trim();
            }

            var existing = _dataStore.AllPersons.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                if (!_task.AssignedTo.Any(p => p.Id == existing.Id))
                    _task.AssignedTo.Add(existing);
            }
            else
            {
                var newPers = new Person { Id = Guid.NewGuid(), Name = name, Email = email };
                _task.AssignedTo.Add(newPers);
                _dataStore.AllPersons.Add(newPers);
            }

            RefreshAssignedList();
            cmbPersons.Text = "";
            TaskChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            if (lbAssigned.SelectedIndex < 0) return;
            var selectedIndex = lbAssigned.SelectedIndex;
            var pers = _task.AssignedTo[selectedIndex];
            _task.AssignedTo.Remove(pers);
            RefreshAssignedList();
            TaskChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _task.Title = txtTaskTitle.Text.Trim();
            TaskChanged?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
