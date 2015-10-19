using System;
using System.Windows.Forms;

namespace ClumsyAssistant.Pages
{
    public partial class TestPage : UserControl
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请不要点我！");
            return;
            var workbook = new Aspose.Cells.Workbook();
            workbook.Worksheets.Add("TEST");
            var sheet = workbook.Worksheets[0];
            for (int i = 0; i < 100000; i++)
            {
                sheet.Cells[i, 0].Value = "fdasfadsfa";
            }
            workbook.Save("ABC.xlsx");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            FrmMain.RemoveTabPage("test");
        }
    }
}
