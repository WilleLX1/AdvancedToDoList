// ErrorForm.Designer.cs
namespace AdvancedToDoList.Forms
{
    partial class ErrorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.RichTextBox rtbErrorDetails;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblError = new System.Windows.Forms.Label();
            this.rtbErrorDetails = new System.Windows.Forms.RichTextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(12, 9);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(113, 17);
            this.lblError.TabIndex = 0;
            this.lblError.Text = "Ett fel har uppstått:";
            // 
            // rtbErrorDetails
            // 
            this.rtbErrorDetails.Location = new System.Drawing.Point(15, 30);
            this.rtbErrorDetails.Name = "rtbErrorDetails";
            this.rtbErrorDetails.ReadOnly = true;
            this.rtbErrorDetails.Size = new System.Drawing.Size(600, 300);
            this.rtbErrorDetails.TabIndex = 1;
            this.rtbErrorDetails.Text = "";
            this.rtbErrorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(420, 325);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(90, 30);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Kopiera";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(525, 325);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Stäng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 370);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.rtbErrorDetails);
            this.Controls.Add(this.lblError);
            this.MinimumSize = new System.Drawing.Size(650, 420);
            this.Name = "ErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Felhantering";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
