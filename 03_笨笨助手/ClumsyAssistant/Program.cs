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
            new Aspose.Cells.License().SetLicense(License.LStream);
            Application.Run(new FrmMain());
        }
    }
}
