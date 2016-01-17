using System.Windows.Forms;

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
    }
}
