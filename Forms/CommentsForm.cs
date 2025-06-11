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
    public partial class CommentsForm : Form
    {
        private List<Comment> _comments;
        private ProjectDataStore _dataStore;

        public CommentsForm(List<Comment> comments, ProjectDataStore dataStore)
        {
            InitializeComponent();
            _comments = comments;
            _dataStore = dataStore;
            RefreshComments();
        }

        private void RefreshComments()
        {
            lvComments.Items.Clear();
            foreach (var comment in _comments.OrderBy(c => c.CreatedAt))
            {
                var personName = _dataStore.AllPersons.FirstOrDefault(p => p.Id == comment.AuthorId)?.Name ?? "Okänd";
                var item = new ListViewItem(new string[] { personName, comment.CreatedAt.ToString(), comment.Text });
                lvComments.Items.Add(item);
            }
        }

        // “Ny kommentar”-knapp
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var text = txtNewComment.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.Empty,
                CreatedAt = DateTime.Now,
                Text = text
            };
            _comments.Add(comment);
            txtNewComment.Clear();
            RefreshComments();
        }

        // “Ta bort kommentar”-knapp (om du vill)
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvComments.SelectedItems.Count == 0) return;
            var idx = lvComments.SelectedIndices[0];
            _comments.RemoveAt(idx);
            RefreshComments();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
