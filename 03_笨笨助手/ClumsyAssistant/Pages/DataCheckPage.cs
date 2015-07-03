using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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
            MessageBox.Show(msg);
        }

        private void AddLog(string log)
        {
            logBuilder.AppendFormat("{0} {1}{2}", DateTime.Now.ToString("HH:mm:ss"), log, Environment.NewLine);
            RtbLog.Text = logBuilder.ToString();
        }

        private int GetDocumentNumberColIndex(ISheet sheet)
        {
            var cells = sheet.GetRow(0).Cells;
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].StringCellValue.StartsWith("单据编号"))
                {
                    return i;
                }
            }
            return -1;
        }

        private IWorkbook GetWorkbook(string filepath)
        {
            var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            if (filepath.IndexOf(".xlsx", StringComparison.Ordinal) > 0) // 2007版本
            {
                return new XSSFWorkbook(fs);
            }
            else if (filepath.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
            {
                return new HSSFWorkbook(fs);
            }
            return null;
        }

        /// <summary>
        /// 根据Excel文件路径，获取单据编号
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="isSjbWorkbook"></param>
        /// <returns></returns>
        private List<string> GetDocumentNumber(string filepath)
        {
            var results = new List<string>();
            IWorkbook workbook = null;
            FileStream fs = null;
            try
            {
                workbook = this.GetWorkbook(filepath);
                var sheet = workbook.GetSheetAt(0);
                int colIndex = this.GetDocumentNumberColIndex(sheet);
                if (colIndex < 0)
                {
                    this.Alert("没有在文件“" + Path.GetFileName(filepath) + "”中找到“单据编号”列");
                    return null;
                }
                //     sheet.GetRow(1).RowStyle.FillBackgroundColor
                int rowCount = sheet.LastRowNum;
                //为什么用<= rowCount?因为LastRowNum是从0开始计算，所以真正的行数应该是LastRowNum+1
                for (int i = START_ROW_INDEX; i <= rowCount; i++)
                {

                    var cell = sheet.GetRow(i).GetCell(colIndex);
                    if (cell == null)
                    {
                        break;
                    }
                    results.Add(cell.StringCellValue);
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
            finally
            {
                if (fs != null) fs.Dispose();
            }
            return null;
        }

        private void FillBackgroundColor(string filepath, IList<string> numberList)
        {
            this.AddLog("正在对差异文件进行着色");
            var workbook = this.GetWorkbook(filepath);
            var sheet = workbook.GetSheetAt(0);
            var rowCount = sheet.LastRowNum;

            var cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = HSSFColor.LightGreen.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;

            int colIndex = this.GetDocumentNumberColIndex(sheet);
            int colorRowCount = 0;
            ICell cell;
            for (int i = START_ROW_INDEX; i <= rowCount; i++)
            {
                cell = sheet.GetRow(i).GetCell(colIndex);
                if (numberList.Contains(cell.StringCellValue.Trim()))
                {
                    cell.CellStyle = cellStyle;
                    colorRowCount++;
                }
            }
            var destPath = Path.GetDirectoryName(filepath) + "/" + Path.GetFileName(filepath).Replace(".xls", "_校对结果.xls");
            var fsDist = new FileStream(destPath, FileMode.Create);
            workbook.Write(fsDist);
            fsDist.Flush();
            fsDist.Close();
            if(DialogResult.Yes == MessageBox.Show("已将差异单据在试剂部文件中着色，共标记行数："+ colorRowCount +"，是否打开结果文件？","提示",MessageBoxButtons.YesNo))
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
                this.Alert("试剂部出/入库文件与同兴源出/入库文件单据号差异数：" + noList.Count);
                this.FillBackgroundColor(TbFile2.Text, noList);
                this.AddLog("校对完成，(^.^)");
            }
            BtnStart.Enabled = true;
        }
        #endregion
    }
}
