namespace AdvancedToDoList
{
    partial class ProjectDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;

        // Taget bort: private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;

        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.SplitContainer splitContainerMain;

        // Panel1 (Files)
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chFilePath;
        private System.Windows.Forms.Panel panelFileButtons;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnRemoveFile;

        // Panel2 (Tasks)
        private System.Windows.Forms.Panel panelTaskButtons;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnEditTask;
        private System.Windows.Forms.Button btnRemoveTask;
        private System.Windows.Forms.SplitContainer splitContainerTasks;

        // splitContainerTasks.Panel1 (Task list)
        private System.Windows.Forms.DataGridView dgvTasks;

        // splitContainerTasks.Panel2 (Parts & Description)
        private System.Windows.Forms.Panel panelPartButtons;
        private System.Windows.Forms.Button btnAddPart;
        private System.Windows.Forms.Button btnRemovePart;
        private System.Windows.Forms.SplitContainer splitContainerParts;

        // splitContainerParts.Panel1 (Parts list)
        private System.Windows.Forms.DataGridView dgvTaskParts;

        // splitContainerParts.Panel2 (Part description)
        private System.Windows.Forms.RichTextBox rtbPartDescription;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteProject;

        /// <summary> 
        /// Rengör resurser som används. 
        /// </summary> 
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();

            // Ta bort lblDescription
            this.txtDescription = new System.Windows.Forms.TextBox();

            this.lblFiles = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();

            this.lvFiles = new System.Windows.Forms.ListView();
            this.chFileName = new System.Windows.Forms.ColumnHeader();
            this.chFilePath = new System.Windows.Forms.ColumnHeader();
            this.panelFileButtons = new System.Windows.Forms.Panel();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnRemoveFile = new System.Windows.Forms.Button();

            this.panelTaskButtons = new System.Windows.Forms.Panel();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnEditTask = new System.Windows.Forms.Button();
            this.btnRemoveTask = new System.Windows.Forms.Button();
            this.splitContainerTasks = new System.Windows.Forms.SplitContainer();

            this.dgvTasks = new System.Windows.Forms.DataGridView();

            this.panelPartButtons = new System.Windows.Forms.Panel();
            this.btnAddPart = new System.Windows.Forms.Button();
            this.btnRemovePart = new System.Windows.Forms.Button();
            this.splitContainerParts = new System.Windows.Forms.SplitContainer();

            this.dgvTaskParts = new System.Windows.Forms.DataGridView();

            // Ta bort lblPartDescription, behåll endast RichTextBox
            this.rtbPartDescription = new System.Windows.Forms.RichTextBox();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeleteProject = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();

            this.panelFileButtons.SuspendLayout();

            this.panelTaskButtons.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTasks)).BeginInit();
            this.splitContainerTasks.Panel1.SuspendLayout();
            this.splitContainerTasks.Panel2.SuspendLayout();
            this.splitContainerTasks.SuspendLayout();

            this.panelPartButtons.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerParts)).BeginInit();
            this.splitContainerParts.Panel1.SuspendLayout();
            this.splitContainerParts.Panel2.SuspendLayout();
            this.splitContainerParts.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskParts)).BeginInit();

            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(87, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Projektnamn:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(110, 12);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(670, 22);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // txtDescription (anm: placeholder istället för separat label)
            // 
            this.txtDescription.Location = new System.Drawing.Point(110, 47);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(670, 80);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right)));
            // Sätt placeholder‐text (obs: kräver .NET 6+)
            this.txtDescription.PlaceholderText = "Beskrivning...";
            // Om .NET-versionen inte stöder PlaceholderText, ersätt ovan rad med:
            // this.txtDescription.Text = "Beskrivning...";
            // this.txtDescription.ForeColor = System.Drawing.Color.Gray;
            // och hantera Enter/Leave‐events i kodfilen för att rensa/färga om texten.

            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(16, 140);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(80, 17);
            this.lblFiles.TabIndex = 4;
            this.lblFiles.Text = "Bilagor (filer):";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Location = new System.Drawing.Point(16, 160);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerMain.SplitterWidth = 6;
            this.splitContainerMain.BackColor = System.Drawing.Color.LightGray;
            // 
            // splitContainerMain.Panel1 (Files)
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.lvFiles);
            this.splitContainerMain.Panel1.Controls.Add(this.panelFileButtons);
            // 
            // splitContainerMain.Panel2 (Tasks)
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerTasks);
            this.splitContainerMain.Panel2.Controls.Add(this.panelTaskButtons);
            this.splitContainerMain.Size = new System.Drawing.Size(764, 330);
            this.splitContainerMain.SplitterDistance = 110;
            this.splitContainerMain.TabIndex = 5;
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // panelFileButtons (i splitContainerMain.Panel1)
            // 
            this.panelFileButtons.Controls.Add(this.btnRemoveFile);
            this.panelFileButtons.Controls.Add(this.btnAddFile);
            this.panelFileButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelFileButtons.Location = new System.Drawing.Point(684, 0);
            this.panelFileButtons.Name = "panelFileButtons";
            this.panelFileButtons.Size = new System.Drawing.Size(80, 110);
            this.panelFileButtons.TabIndex = 0;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddFile.Location = new System.Drawing.Point(0, 0);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(80, 28);
            this.btnAddFile.TabIndex = 6;
            this.btnAddFile.Text = "Lägg till";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRemoveFile.Location = new System.Drawing.Point(0, 28);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(80, 28);
            this.btnRemoveFile.TabIndex = 7;
            this.btnRemoveFile.Text = "Ta bort";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // lvFiles
            // 
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.chFileName,
                this.chFilePath});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(684, 110);
            this.lvFiles.TabIndex = 5;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // chFileName
            // 
            this.chFileName.Text = "Filnamn";
            this.chFileName.Width = 200;
            // 
            // chFilePath
            // 
            this.chFilePath.Text = "Sökväg";
            this.chFilePath.Width = 460;
            // 
            // panelTaskButtons (i splitContainerMain.Panel2)
            // 
            this.panelTaskButtons.Controls.Add(this.btnRemoveTask);
            this.panelTaskButtons.Controls.Add(this.btnEditTask);
            this.panelTaskButtons.Controls.Add(this.btnAddTask);
            this.panelTaskButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTaskButtons.Location = new System.Drawing.Point(0, 0);
            this.panelTaskButtons.Name = "panelTaskButtons";
            this.panelTaskButtons.Size = new System.Drawing.Size(764, 30);
            this.panelTaskButtons.TabIndex = 1;
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(0, 1);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(90, 28);
            this.btnAddTask.TabIndex = 8;
            this.btnAddTask.Text = "Ny uppg.";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnEditTask
            // 
            this.btnEditTask.Location = new System.Drawing.Point(96, 1);
            this.btnEditTask.Name = "btnEditTask";
            this.btnEditTask.Size = new System.Drawing.Size(90, 28);
            this.btnEditTask.TabIndex = 9;
            this.btnEditTask.Text = "Redigera";
            this.btnEditTask.UseVisualStyleBackColor = true;
            this.btnEditTask.Click += new System.EventHandler(this.btnEditTask_Click);
            // 
            // btnRemoveTask
            // 
            this.btnRemoveTask.Location = new System.Drawing.Point(192, 1);
            this.btnRemoveTask.Name = "btnRemoveTask";
            this.btnRemoveTask.Size = new System.Drawing.Size(90, 28);
            this.btnRemoveTask.TabIndex = 10;
            this.btnRemoveTask.Text = "Ta bort";
            this.btnRemoveTask.UseVisualStyleBackColor = true;
            this.btnRemoveTask.Click += new System.EventHandler(this.btnRemoveTask_Click);
            // 
            // splitContainerTasks
            // 
            this.splitContainerTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTasks.Location = new System.Drawing.Point(0, 30);
            this.splitContainerTasks.Name = "splitContainerTasks";
            this.splitContainerTasks.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerTasks.SplitterWidth = 6;
            this.splitContainerTasks.BackColor = System.Drawing.Color.LightGray;
            // 
            // splitContainerTasks.Panel1 (Task list)
            // 
            this.splitContainerTasks.Panel1.Controls.Add(this.dgvTasks);
            // 
            // splitContainerTasks.Panel2 (Parts & Description)
            // 
            this.splitContainerTasks.Panel2.Controls.Add(this.panelPartButtons);
            this.splitContainerTasks.Panel2.Controls.Add(this.splitContainerParts);
            this.splitContainerTasks.Size = new System.Drawing.Size(764, 300);
            this.splitContainerTasks.SplitterDistance = 100;
            this.splitContainerTasks.TabIndex = 2;
            // 
            // dgvTasks
            // 
            this.dgvTasks.AllowUserToAddRows = false;
            this.dgvTasks.AllowUserToDeleteRows = false;
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTasks.Location = new System.Drawing.Point(0, 0);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.ReadOnly = true;
            this.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.Size = new System.Drawing.Size(764, 100);
            this.dgvTasks.TabIndex = 0;
            this.dgvTasks.SelectionChanged += new System.EventHandler(this.dgvTasks_SelectionChanged);
            // 
            // panelPartButtons (i splitContainerTasks.Panel2)
            // 
            this.panelPartButtons.Controls.Add(this.btnRemovePart);
            this.panelPartButtons.Controls.Add(this.btnAddPart);
            this.panelPartButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPartButtons.Location = new System.Drawing.Point(0, 0);
            this.panelPartButtons.Name = "panelPartButtons";
            this.panelPartButtons.Size = new System.Drawing.Size(764, 30);
            this.panelPartButtons.TabIndex = 1;
            // 
            // btnAddPart
            // 
            this.btnAddPart.Location = new System.Drawing.Point(0, 1);
            this.btnAddPart.Name = "btnAddPart";
            this.btnAddPart.Size = new System.Drawing.Size(80, 28);
            this.btnAddPart.TabIndex = 11;
            this.btnAddPart.Text = "Ny del";
            this.btnAddPart.UseVisualStyleBackColor = true;
            this.btnAddPart.Click += new System.EventHandler(this.btnAddPart_Click);
            // 
            // btnRemovePart
            // 
            this.btnRemovePart.Location = new System.Drawing.Point(86, 1);
            this.btnRemovePart.Name = "btnRemovePart";
            this.btnRemovePart.Size = new System.Drawing.Size(80, 28);
            this.btnRemovePart.TabIndex = 12;
            this.btnRemovePart.Text = "Ta bort del";
            this.btnRemovePart.UseVisualStyleBackColor = true;
            this.btnRemovePart.Click += new System.EventHandler(this.btnRemovePart_Click);
            // 
            // splitContainerParts
            // 
            this.splitContainerParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerParts.Location = new System.Drawing.Point(0, 30);
            this.splitContainerParts.Name = "splitContainerParts";
            this.splitContainerParts.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerParts.SplitterWidth = 6;
            this.splitContainerParts.BackColor = System.Drawing.Color.LightGray;
            // 
            // splitContainerParts.Panel1 (Parts list)
            // 
            this.splitContainerParts.Panel1.Controls.Add(this.dgvTaskParts);
            // 
            // splitContainerParts.Panel2 (Part description)
            // 
            this.splitContainerParts.Panel2.Controls.Add(this.rtbPartDescription);
            this.splitContainerParts.Size = new System.Drawing.Size(764, 266);
            this.splitContainerParts.SplitterDistance = 120;
            this.splitContainerParts.TabIndex = 0;
            // 
            // dgvTaskParts
            // 
            this.dgvTaskParts.AllowUserToAddRows = false;
            this.dgvTaskParts.AllowUserToDeleteRows = false;
            this.dgvTaskParts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaskParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskParts.Location = new System.Drawing.Point(0, 0);
            this.dgvTaskParts.Name = "dgvTaskParts";
            this.dgvTaskParts.ReadOnly = false;
            this.dgvTaskParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaskParts.Size = new System.Drawing.Size(764, 120);
            this.dgvTaskParts.TabIndex = 0;
            // 
            // rtbPartDescription
            // 
            this.rtbPartDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbPartDescription.Location = new System.Drawing.Point(0, 0);
            this.rtbPartDescription.Name = "rtbPartDescription";
            this.rtbPartDescription.Size = new System.Drawing.Size(764, 144);
            this.rtbPartDescription.TabIndex = 1;
            this.rtbPartDescription.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(610, 500);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Spara";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(710, 500);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Avbryt";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnDeleteProject
            // 
            this.btnDeleteProject.Location = new System.Drawing.Point(510, 500);
            this.btnDeleteProject.Name = "btnDeleteProject";
            this.btnDeleteProject.Size = new System.Drawing.Size(90, 28);
            this.btnDeleteProject.TabIndex = 8;
            this.btnDeleteProject.Text = "Ta bort projekt";
            this.btnDeleteProject.UseVisualStyleBackColor = true;
            this.btnDeleteProject.Click += new System.EventHandler(this.btnDeleteProject_Click);
            this.btnDeleteProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // ProjectDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.btnDeleteProject);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.lblFiles);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "ProjectDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Projekt‐detaljer";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);

            this.panelFileButtons.ResumeLayout(false);

            this.panelTaskButtons.ResumeLayout(false);

            this.splitContainerTasks.Panel1.ResumeLayout(false);
            this.splitContainerTasks.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTasks)).EndInit();
            this.splitContainerTasks.ResumeLayout(false);

            this.panelPartButtons.ResumeLayout(false);

            this.splitContainerParts.Panel1.ResumeLayout(false);
            this.splitContainerParts.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerParts)).EndInit();
            this.splitContainerParts.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskParts)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
