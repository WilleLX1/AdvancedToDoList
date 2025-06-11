namespace AdvancedToDoList
{
    partial class CommentsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblExistingComments;
        private System.Windows.Forms.ListView lvComments;
        private System.Windows.Forms.ColumnHeader chAuthor;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chCommentText;

        private System.Windows.Forms.Label lblNewComment;
        private System.Windows.Forms.TextBox txtNewComment;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;

        private System.Windows.Forms.Button btnClose;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true om managed resources ska disposas; annars false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Metod som krävs för Designer-stöd – Ändra inte innehållet
        /// i denna metod med kodredigeraren.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblExistingComments = new System.Windows.Forms.Label();
            this.lvComments = new System.Windows.Forms.ListView();
            this.chAuthor = new System.Windows.Forms.ColumnHeader();
            this.chDate = new System.Windows.Forms.ColumnHeader();
            this.chCommentText = new System.Windows.Forms.ColumnHeader();

            this.lblNewComment = new System.Windows.Forms.Label();
            this.txtNewComment = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();

            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // lblExistingComments
            // 
            this.lblExistingComments.AutoSize = true;
            this.lblExistingComments.Location = new System.Drawing.Point(16, 15);
            this.lblExistingComments.Name = "lblExistingComments";
            this.lblExistingComments.Size = new System.Drawing.Size(83, 17);
            this.lblExistingComments.TabIndex = 0;
            this.lblExistingComments.Text = "Kommentarer:";
            // 
            // lvComments
            // 
            this.lvComments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAuthor,
            this.chDate,
            this.chCommentText});
            this.lvComments.FullRowSelect = true;
            this.lvComments.GridLines = true;
            this.lvComments.HideSelection = false;
            this.lvComments.Location = new System.Drawing.Point(19, 35);
            this.lvComments.MultiSelect = false;
            this.lvComments.Name = "lvComments";
            this.lvComments.Size = new System.Drawing.Size(520, 150);
            this.lvComments.TabIndex = 1;
            this.lvComments.UseCompatibleStateImageBehavior = false;
            this.lvComments.View = System.Windows.Forms.View.Details;
            // 
            // chAuthor
            // 
            this.chAuthor.Text = "Författare";
            this.chAuthor.Width = 120;
            // 
            // chDate
            // 
            this.chDate.Text = "Datum";
            this.chDate.Width = 120;
            // 
            // chCommentText
            // 
            this.chCommentText.Text = "Kommentar";
            this.chCommentText.Width = 280;
            // 
            // lblNewComment
            // 
            this.lblNewComment.AutoSize = true;
            this.lblNewComment.Location = new System.Drawing.Point(16, 200);
            this.lblNewComment.Name = "lblNewComment";
            this.lblNewComment.Size = new System.Drawing.Size(107, 17);
            this.lblNewComment.TabIndex = 2;
            this.lblNewComment.Text = "Ny kommentar:";
            // 
            // txtNewComment
            // 
            this.txtNewComment.Location = new System.Drawing.Point(19, 220);
            this.txtNewComment.Multiline = true;
            this.txtNewComment.Name = "txtNewComment";
            this.txtNewComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNewComment.Size = new System.Drawing.Size(380, 80);
            this.txtNewComment.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(410, 220);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 25);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Lägg till";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(410, 255);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(85, 25);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Ta bort";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(410, 295);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Stäng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // CommentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 340);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNewComment);
            this.Controls.Add(this.lblNewComment);
            this.Controls.Add(this.lvComments);
            this.Controls.Add(this.lblExistingComments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kommentarer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}