// TaskDetailForm.Designer.cs
namespace AdvancedToDoList
{
    partial class TaskDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTaskTitle;
        private System.Windows.Forms.TextBox txtTaskTitle;

        private System.Windows.Forms.Label lblPersons;
        private System.Windows.Forms.ComboBox cmbPersons;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.ListBox lbAssigned;
        private System.Windows.Forms.Button btnRemovePerson;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();

            this.lblPersons = new System.Windows.Forms.Label();
            this.cmbPersons = new System.Windows.Forms.ComboBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.lbAssigned = new System.Windows.Forms.ListBox();
            this.btnRemovePerson = new System.Windows.Forms.Button();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.AutoSize = true;
            this.lblTaskTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new System.Drawing.Size(88, 17);
            this.lblTaskTitle.TabIndex = 0;
            this.lblTaskTitle.Text = "Uppgiftsnamn:";
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.Location = new System.Drawing.Point(120, 12);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(640, 22);
            this.txtTaskTitle.TabIndex = 1;
            this.txtTaskTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lblPersons
            // 
            this.lblPersons.AutoSize = true;
            this.lblPersons.Location = new System.Drawing.Point(16, 50);
            this.lblPersons.Name = "lblPersons";
            this.lblPersons.Size = new System.Drawing.Size(122, 17);
            this.lblPersons.TabIndex = 2;
            this.lblPersons.Text = "Tilldela personer:";
            // 
            // cmbPersons
            // 
            this.cmbPersons.FormattingEnabled = true;
            this.cmbPersons.Location = new System.Drawing.Point(144, 47);
            this.cmbPersons.Name = "cmbPersons";
            this.cmbPersons.Size = new System.Drawing.Size(450, 24);
            this.cmbPersons.TabIndex = 3;
            this.cmbPersons.Anchor = ((System.Windows.Forms.AnchorStyles)(
                ((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(600, 46);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(75, 25);
            this.btnAddPerson.TabIndex = 4;
            this.btnAddPerson.Text = "Lägg till";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            this.btnAddPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right));
            // 
            // lbAssigned
            // 
            this.lbAssigned.FormattingEnabled = true;
            this.lbAssigned.ItemHeight = 16;
            this.lbAssigned.Location = new System.Drawing.Point(144, 80);
            this.lbAssigned.Name = "lbAssigned";
            this.lbAssigned.Size = new System.Drawing.Size(450, 68);
            this.lbAssigned.TabIndex = 5;
            this.lbAssigned.Anchor = ((System.Windows.Forms.AnchorStyles)(
                ((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnRemovePerson
            // 
            this.btnRemovePerson.Location = new System.Drawing.Point(600, 80);
            this.btnRemovePerson.Name = "btnRemovePerson";
            this.btnRemovePerson.Size = new System.Drawing.Size(75, 25);
            this.btnRemovePerson.TabIndex = 6;
            this.btnRemovePerson.Text = "Ta bort";
            this.btnRemovePerson.UseVisualStyleBackColor = true;
            this.btnRemovePerson.Click += new System.EventHandler(this.btnRemovePerson_Click);
            this.btnRemovePerson.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(600, 170);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Spara";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right));
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(700, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Avbryt";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right));
            // 
            // TaskDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 220);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemovePerson);
            this.Controls.Add(this.lbAssigned);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.cmbPersons);
            this.Controls.Add(this.lblPersons);
            this.Controls.Add(this.txtTaskTitle);
            this.Controls.Add(this.lblTaskTitle);
            this.MinimumSize = new System.Drawing.Size(600, 260);
            this.Name = "TaskDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Uppgiftsdetaljer";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
