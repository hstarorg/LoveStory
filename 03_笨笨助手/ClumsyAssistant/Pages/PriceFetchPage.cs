using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Aspose.Cells;
using ClumsyAssistant.Utilities;

namespace ClumsyAssistant.Pages
{
    public partial class PriceFetchPage : UserControl
    {
        const int START_ROW_INDEX = 1; //为什么不是0？因为首行是列头
        private readonly StringBuilder logBuilder = new StringBuilder();
        public PriceFetchPage()
        {
            InitializeComponent();
        }

        #region 辅助方法

        private void AddLog(string log)
        {
            logBuilder.AppendFormat("{0} {1}{2}", DateTime.Now.ToString("HH:mm:ss"), log, Environment.NewLine);
            RtbLog.Text = logBuilder.ToString();
        }

        private Dictionary<string, double> GetPriceData(string filePath)
        {
            var result = new Dictionary<string, double>();
            var workbook = new Workbook(filePath);
            var sheet = workbook.Worksheets[0];
            var materialCodeIdx = ExcelHelper.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = ExcelHelper.GetColIndexByColName(sheet, "批号");
            var materialPriceIndex = ExcelHelper.GetColIndexByColName(sheet, "单价");
            if (materialCodeIdx < 0)
            {
                Common.Alert("没有找到[物料代码]列！");
            }
            else if (materialBatchIdx < 0)
            {
                Common.Alert("没有找到[批号]列！");
            }
            else
            {
                int rowCount = sheet.Cells.MaxRow;
                //为什么用<= rowCount?因为LastRowNum是从0开始计算，所以真正的行数应该是LastRowNum+1
                for (int i = START_ROW_INDEX; i <= rowCount; i++)
                {
                    var materialCode = sheet.Cells[i, materialCodeIdx].StringValue.Trim();
                    var materialBatch = sheet.Cells[i, materialBatchIdx].StringValue.Trim() ?? "";
                    var cellForPrice = sheet.Cells[i, materialPriceIndex];
                    var key = $"{materialCode}$|${materialBatch}";
                    if (!result.ContainsKey(key))
                    {
                        result.Add(key, cellForPrice.DoubleValue);
                    }
                }
            }
            return result;
        }

        private void BuildExcelFile(string filePath, Dictionary<string, double> priceDic)
        {
            var workbook = new Workbook(filePath);
            var sheet = workbook.Worksheets[0];
            var materialCodeIdx = ExcelHelper.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = ExcelHelper.GetColIndexByColName(sheet, "批号");
            //增加一列
            var priceCellIndex = sheet.Cells.Columns.Count;
            sheet.Cells[0, priceCellIndex].Value = "单价";
            //写入数据
            int rowCount = sheet.Cells.MaxRow;
            //为什么用<= rowCount?因为LastRowNum是从0开始计算，所以真正的行数应该是LastRowNum+1
            for (int i = START_ROW_INDEX; i <= rowCount; i++)
            {
                var materialCode = sheet.Cells[i, materialCodeIdx].StringValue.Trim();
                var materialBatch = sheet.Cells[i, materialBatchIdx].StringValue.Trim() ?? "";
                var dicKey = $"{materialCode}$|${materialBatch}";
                double unitPrice;
                if (priceDic.TryGetValue(dicKey, out unitPrice))
                {
                    sheet.Cells[i, priceCellIndex].Value = unitPrice;
                }
            }
            var destPath = Path.GetDirectoryName(TbFile2.Text) + "/库存带价格数据表.xls";
            workbook.Save(destPath);
            Common.NotifyAndOpenFile(destPath);
        }
        #endregion

        #region 事件处理

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            var filepath = Common.SelectFile();
            if (!string.IsNullOrEmpty(filepath))
            {
                TbFile.Text = filepath;
            }
        }

        private void BtnSelectFile2_Click(object sender, EventArgs e)
        {
            var filepath = Common.SelectFile();
            if (!string.IsNullOrEmpty(filepath))
            {
                TbFile2.Text = filepath;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TbFile.Text == "" || !File.Exists(TbFile.Text))
            {
                Common.Alert("请选择库存文件。");
                return;
            }
            if (TbFile2.Text == "" || !File.Exists(TbFile2.Text))
            {
                Common.Alert("请选择物料价格文件。");
                return;
            }
            logBuilder.Clear();
            BtnStart.Enabled = false;
            this.AddLog("开始抓取价格");
            var priceData = this.GetPriceData(TbFile2.Text);
            this.AddLog("价格抓取完毕，正在写入");

            this.BuildExcelFile(TbFile.Text, priceData);
            this.AddLog("盘价格抓取已完成！");
            BtnStart.Enabled = true;
        }
        #endregion

        #region 拖拽控制和关闭按钮
        private void BtnClose_Click(object sender, EventArgs e)
        {
            FrmMain.RemoveTabPage("price_fetch");
        }
        #endregion
    }
}
