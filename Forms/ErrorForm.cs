// ErrorForm.cs
using System;
using System.Windows.Forms;

namespace AdvancedToDoList.Forms
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(Exception ex)
        {
            InitializeComponent();
            rtbErrorDetails.Text = $"Tid: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n" +
                                   $"Meddelande: {ex.Message}\n\n" +
                                   $"Stackspårning:\n{ex.StackTrace}";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbErrorDetails.Text))
                Clipboard.SetText(rtbErrorDetails.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
