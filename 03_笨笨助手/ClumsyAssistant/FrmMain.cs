using System;
using System.Windows.Forms;
using ClumsyAssistant.Pages;

namespace ClumsyAssistant
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnDataCheck_Click(object sender, EventArgs e)
        {
            this.AddNewTabPage<DataCheckPage>("data_check", "数据校对");
        }

        private void AddNewTabPage<T>(string name, string title) where T : UserControl, new()
        {
            if (!MainTabControl.TabPages.ContainsKey(name))
            {
                var tabPage = new TabPage(title) {Name = name};
                var t = new T { Dock = DockStyle.Fill };
                tabPage.Controls.Add(t);
                MainTabControl.TabPages.Add(tabPage);
            }
            MainTabControl.SelectTab(name);
        }
    }
}
