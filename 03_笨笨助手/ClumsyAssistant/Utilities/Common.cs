using System.Diagnostics;
using System.Windows.Forms;
using Aspose.Cells.Charts;

namespace ClumsyAssistant.Utilities
{
    public static class Common
    {
        public static void Alert(string msg)
        {
            MessageBox.Show(msg, "爱心提示");
        }

        public static string SelectFile()
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel文件|*.xls;*.xlsx", Multiselect = false };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        public static void NotifyAndOpenFile(string filePath, string msg = null)
        {
            const string title = "爱心提示";
            if (MessageBox.Show(msg ?? "导出文件成功，是否要打开文件？", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                Process.Start(filePath);
            }
        }
    }
}
