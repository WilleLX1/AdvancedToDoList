// Program.cs (lägg till eller uppdatera så här i ditt WinForms-projekt)
using System;
using System.Windows.Forms;
using AdvancedToDoList.Forms;

namespace AdvancedToDoList
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Globala undantagshanterare
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowErrorDialog(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                ShowErrorDialog(ex);
        }

        private static void ShowErrorDialog(Exception ex)
        {
            try
            {
                using (var form = new ErrorForm(ex))
                {
                    form.ShowDialog();
                }
            }
            catch
            {
                // Om felhanteringsdialogen misslyckas, visa enkel MessageBox
                MessageBox.Show($"Ett allvarligt fel inträffade:\n{ex}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
