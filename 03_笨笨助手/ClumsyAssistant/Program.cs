using System;
using System.Windows.Forms;

namespace ClumsyAssistant
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (sender, e) =>
            {
                var exception = e.Exception;
                MessageBox.Show(exception.Message);
            };

            new Aspose.Cells.License().SetLicense(License.LStream);
            Application.Run(new FrmMain());
        }
    }
}
