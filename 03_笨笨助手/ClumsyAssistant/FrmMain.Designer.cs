﻿namespace ClumsyAssistant
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.btnDataCheck = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.HomeTabPage = new System.Windows.Forms.TabPage();
            this.btnTest = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.MenuPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统SToolStripMenuItem,
            this.关于AToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统SToolStripMenuItem
            // 
            this.系统SToolStripMenuItem.Name = "系统SToolStripMenuItem";
            this.系统SToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.系统SToolStripMenuItem.Text = "系统(&S)";
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.关于AToolStripMenuItem.Text = "关于(&A)";
            // 
            // MenuPanel
            // 
            this.MenuPanel.Controls.Add(this.btnTest);
            this.MenuPanel.Controls.Add(this.btnDataCheck);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenuPanel.Location = new System.Drawing.Point(0, 25);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(784, 40);
            this.MenuPanel.TabIndex = 1;
            // 
            // btnDataCheck
            // 
            this.btnDataCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDataCheck.Location = new System.Drawing.Point(0, 0);
            this.btnDataCheck.Name = "btnDataCheck";
            this.btnDataCheck.Size = new System.Drawing.Size(40, 40);
            this.btnDataCheck.TabIndex = 0;
            this.btnDataCheck.Text = "数据校对";
            this.btnDataCheck.UseVisualStyleBackColor = true;
            this.btnDataCheck.Click += new System.EventHandler(this.btnDataCheck_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.MainTabControl);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 65);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(784, 496);
            this.MainPanel.TabIndex = 2;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.HomeTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(784, 496);
            this.MainTabControl.TabIndex = 0;
            // 
            // HomeTabPage
            // 
            this.HomeTabPage.Location = new System.Drawing.Point(4, 22);
            this.HomeTabPage.Name = "HomeTabPage";
            this.HomeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.HomeTabPage.Size = new System.Drawing.Size(776, 470);
            this.HomeTabPage.TabIndex = 0;
            this.HomeTabPage.Text = "主页";
            this.HomeTabPage.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTest.Location = new System.Drawing.Point(40, 0);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(40, 40);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MenuPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "笨笨助手 V0.1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MenuPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage HomeTabPage;
        private System.Windows.Forms.Button btnDataCheck;
        private System.Windows.Forms.Button btnTest;
    }
}

