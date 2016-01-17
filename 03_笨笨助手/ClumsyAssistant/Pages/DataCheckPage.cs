using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Aspose.Cells.Drawing;

namespace ClumsyAssistant.Pages
{
    public partial class DataCheckPage : UserControl
    {
        const int START_ROW_INDEX = 1; //为什么不是0？因为首行是列头
        private readonly StringBuilder logBuilder = new StringBuilder();
        public DataCheckPage()
        {
            InitializeComponent();
        }

        #region 辅助方法
        private string SelectFile()
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel文件|*.xls;*.xlsx", Multiselect = false };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        private void Alert(string msg)
        {
            MessageBox.Show(msg, "爱心提示");
        }

        private void AddLog(string log)
        {
            logBuilder.AppendFormat("{0} {1}{2}", DateTime.Now.ToString("HH:mm:ss"), log, Environment.NewLine);
            RtbLog.Text = logBuilder.ToString();
        }

        private int GetDocumentNumberColIndex(Worksheet sheet)
        {
            var cells = sheet.Cells;
            for (int i = 0; i <= cells.MaxColumn; i++)
            {
                if (cells[0, i].StringValue.StartsWith("单据编号"))
                {
                    return i;
                }
            }
            return -1;
        }


        /// <summary>
        /// 根据Excel文件路径，获取单据编号
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private List<string> GetDocumentNumber(string filepath)
        {
            var results = new List<string>();
            try
            {
                var workbook = new Workbook(filepath);
                var sheet = workbook.Worksheets[0];
                int colIndex = this.GetDocumentNumberColIndex(sheet);
                if (colIndex < 0)
                {
                    this.Alert("没有在文件“" + Path.GetFileName(filepath) + "”中找到“单据编号”列");
                    return null;
                }
                //     sheet.GetRow(1).RowStyle.FillBackgroundColor
                int rowCount = sheet.Cells.MaxRow;
                //为什么用<= rowCount?因为LastRowNum是从0开始计算，所以真正的行数应该是LastRowNum+1
                for (int i = START_ROW_INDEX; i <= rowCount; i++)
                {
                    var cell = sheet.Cells[i, colIndex];
                    if (cell == null)
                    {
                        break;
                    }
                    results.Add(cell.StringValue.Trim());
                }
                return results;
            }
            catch (IOException ioEx)
            {
                this.Alert("读取Excel出错 => " + ioEx.Message);
            }
            catch (Exception ex)
            {
                this.Alert("获取单据号失败 => " + ex.Message);
            }
            return null;
        }

        private void FillBackgroundColor(string filepath, IList<string> numberList)
        {
            this.AddLog("正在对差异文件进行着色");
            var workbook = new Workbook(filepath);
            var sheet = workbook.Worksheets[0];
            var rowCount = sheet.Cells.MaxRow;


            var cellStyle = workbook.CreateStyle();
            cellStyle.BackgroundColor = Color.LightGreen;
            cellStyle.Pattern = BackgroundType.Solid;
            cellStyle.Font.Size = 40;
            //new Style {BackgroundColor = Color.LightGreen};
            //                        cellStyle.FillForegroundColor = HSSFColor.LightGreen.Index;
            //                        cellStyle.FillPattern = FillPattern.SolidForeground;

            int colIndex = this.GetDocumentNumberColIndex(sheet);
            int colorRowCount = 0;
            for (int i = START_ROW_INDEX; i <= rowCount; i++)
            {
                Cell cell = sheet.Cells[i, colIndex];
                if (cell != null && numberList.Contains(cell.StringValue.Trim()))
                {
                    var style = cell.GetStyle();
                    style.Pattern = BackgroundType.Solid;
                    style.ForegroundColor = Color.Red;
                    cell.SetStyle(style);
                    colorRowCount++;
                }
            }
            var destPath = Path.GetDirectoryName(filepath) + "/" + Path.GetFileName(filepath).Replace(".xls", "_校对结果.xls");

            workbook.Save(destPath);
            this.AddLog("已将差异单据在试剂部文件中着色，共标记行数：" + colorRowCount);
            if (DialogResult.Yes == MessageBox.Show("已将差异单据在试剂部文件中着色，共标记行数：" + colorRowCount + "，是否打开结果文件？", "提示", MessageBoxButtons.YesNo))
            {
                System.Diagnostics.Process.Start(destPath);
            }
        }
        #endregion

        #region 事件处理

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            var filepath = this.SelectFile();
            if (!string.IsNullOrEmpty(filepath))
            {
                TbFile.Text = filepath;
            }
        }

        private void BtnSelectFile2_Click(object sender, EventArgs e)
        {
            var filepath = this.SelectFile();
            if (!string.IsNullOrEmpty(filepath))
            {
                TbFile2.Text = filepath;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TbFile.Text == "" || !File.Exists(TbFile.Text))
            {
                this.Alert("请选择同兴源出/入库文件。");
                return;
            }
            if (TbFile2.Text == "" || !File.Exists(TbFile2.Text))
            {
                this.Alert("请选择试剂部出/入库文件。");
                return;
            }
            logBuilder.Clear();
            BtnStart.Enabled = false;
            this.AddLog("开始校对数据");

            var txyDocNoList = this.GetDocumentNumber(TbFile.Text);
            var sjbDocNoList = this.GetDocumentNumber(TbFile2.Text);

            if (txyDocNoList != null && sjbDocNoList != null)
            {
                this.AddLog("同兴源出/入库文件单据总数：" + txyDocNoList.Count);
                this.AddLog("试剂部出/入库文件单据总数：" + sjbDocNoList.Count);
                txyDocNoList = txyDocNoList.Distinct().ToList();
                this.AddLog("同兴源出/入库文件单据去重后总数：" + txyDocNoList.Count);
                sjbDocNoList = sjbDocNoList.Distinct().ToList();
                this.AddLog("试剂部出/入库文件单据去重后总数：" + sjbDocNoList.Count);
                var noList = sjbDocNoList.Except(txyDocNoList).ToList();
                this.AddLog("试剂部出/入库文件与同兴源出/入库文件单据号差异数：" + noList.Count);
                this.Alert("试剂部出/入库文件与同兴源出/入库文件单据号差异数：" + noList.Count);
                this.FillBackgroundColor(TbFile2.Text, noList);
                this.AddLog("校对完成，(^.^)");
            }
            BtnStart.Enabled = true;
        }
        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            FrmMain.RemoveTabPage("data_check");
        }

        private void TbFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                TbFile.Cursor = Cursors.Arrow;  //指定鼠标形状（更好看）
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TbFile_DragDrop(object sender, DragEventArgs e)
        {
            var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            TbFile.Text = path;
            TbFile.Cursor = Cursors.IBeam; //还原鼠标形状
        }

        private void TbFile2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                TbFile2.Cursor = Cursors.Arrow;  //指定鼠标形状（更好看）
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TbFile2_DragDrop(object sender, DragEventArgs e)
        {
            var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            TbFile2.Text = path;
            TbFile2.Cursor = Cursors.IBeam; //还原鼠标形状
        }
    }
}
