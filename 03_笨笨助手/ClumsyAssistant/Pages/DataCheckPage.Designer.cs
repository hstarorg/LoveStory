﻿namespace ClumsyAssistant.Pages
{
    partial class DataCheckPage
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RtbLog = new System.Windows.Forms.RichTextBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnSelectFile2 = new System.Windows.Forms.Button();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.TbFile2 = new System.Windows.Forms.TextBox();
            this.TbFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择同兴源：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RtbLog);
            this.groupBox1.Controls.Add(this.BtnStart);
            this.groupBox1.Controls.Add(this.BtnSelectFile2);
            this.groupBox1.Controls.Add(this.BtnSelectFile);
            this.groupBox1.Controls.Add(this.TbFile2);
            this.groupBox1.Controls.Add(this.TbFile);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 432);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出/入库数据校对";
            // 
            // RtbLog
            // 
            this.RtbLog.BackColor = System.Drawing.Color.Black;
            this.RtbLog.ForeColor = System.Drawing.Color.Red;
            this.RtbLog.Location = new System.Drawing.Point(9, 134);
            this.RtbLog.Name = "RtbLog";
            this.RtbLog.Size = new System.Drawing.Size(345, 292);
            this.RtbLog.TabIndex = 2;
            this.RtbLog.Text = "日志信息：";
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(242, 105);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 3;
            this.BtnStart.Text = "开始校对";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnSelectFile2
            // 
            this.BtnSelectFile2.Location = new System.Drawing.Point(285, 60);
            this.BtnSelectFile2.Name = "BtnSelectFile2";
            this.BtnSelectFile2.Size = new System.Drawing.Size(32, 23);
            this.BtnSelectFile2.TabIndex = 2;
            this.BtnSelectFile2.Text = "...";
            this.BtnSelectFile2.UseVisualStyleBackColor = true;
            this.BtnSelectFile2.Click += new System.EventHandler(this.BtnSelectFile2_Click);
            // 
            // BtnSelectFile
            // 
            this.BtnSelectFile.Location = new System.Drawing.Point(285, 29);
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.Size = new System.Drawing.Size(32, 23);
            this.BtnSelectFile.TabIndex = 2;
            this.BtnSelectFile.Text = "...";
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // TbFile2
            // 
            this.TbFile2.AllowDrop = true;
            this.TbFile2.Location = new System.Drawing.Point(115, 61);
            this.TbFile2.Name = "TbFile2";
            this.TbFile2.Size = new System.Drawing.Size(169, 21);
            this.TbFile2.TabIndex = 2;
            this.TbFile2.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbFile2_DragDrop);
            this.TbFile2.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbFile2_DragEnter);
            // 
            // TbFile
            // 
            this.TbFile.AllowDrop = true;
            this.TbFile.Location = new System.Drawing.Point(115, 30);
            this.TbFile.Name = "TbFile";
            this.TbFile.Size = new System.Drawing.Size(169, 21);
            this.TbFile.TabIndex = 1;
            this.TbFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.TbFile_DragDrop);
            this.TbFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.TbFile_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "请选择试剂部：";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(400, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 146);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox1";
            this.groupBox2.Visible = false;
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(696, 442);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "关闭";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // DataCheckPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DataCheckPage";
            this.Size = new System.Drawing.Size(774, 468);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbFile2;
        private System.Windows.Forms.TextBox TbFile;
        private System.Windows.Forms.Button BtnSelectFile2;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.RichTextBox RtbLog;
        private System.Windows.Forms.Button BtnClose;
    }
}
