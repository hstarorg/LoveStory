using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Aspose.Cells;
using ClumsyAssistant.Models;
using ClumsyAssistant.Utilities;

namespace ClumsyAssistant.Pages
{
    public partial class InventoryCountPage : UserControl
    {
        const int START_ROW_INDEX = 1; //为什么不是0？因为首行是列头
        private readonly StringBuilder logBuilder = new StringBuilder();
        public InventoryCountPage()
        {
            InitializeComponent();
        }

        #region 辅助方法

        private void AddLog(string log)
        {
            logBuilder.AppendFormat("{0} {1}{2}", DateTime.Now.ToString("HH:mm:ss"), log, Environment.NewLine);
            RtbLog.Text = logBuilder.ToString();
        }

        private IList<MaterialEntity> GetTxyMaterialData(string filePath)
        {
            var result = new List<MaterialEntity>();
            var workbook = new Workbook(filePath);
            var sheet = workbook.Worksheets[0];
            var materialCodeIdx = ExcelHelper.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = ExcelHelper.GetColIndexByColName(sheet, "批号");
            var materialNumberIndex = ExcelHelper.GetColIndexByColName(sheet, "数量");
            var materialNameIndex = ExcelHelper.GetColIndexByColName(sheet, "物料名称");
            if (materialCodeIdx < 0)
            {
                Common.Alert("没有找到[物料代码]列！");
            }
            else if (materialBatchIdx < 0)
            {
                Common.Alert("没有找到[批号]列！");
            }
            else if (materialNumberIndex < 0)
            {
                Common.Alert("没有找到[数量]列！");
            }
            else
            {
                int rowCount = sheet.Cells.MaxRow;
                //为什么用<= rowCount?因为LastRowNum是从0开始计算，所以真正的行数应该是LastRowNum+1
                for (int i = START_ROW_INDEX; i <= rowCount; i++)
                {
                    var cellForCode = sheet.Cells[i, materialCodeIdx];
                    var cellForBatch = sheet.Cells[i, materialBatchIdx];
                    var cellForNumber = sheet.Cells[i, materialNumberIndex];
                    var cellForName = sheet.Cells[i, materialNameIndex];
                    if (cellForCode == null)
                    {
                        break;
                    }
                    result.Add(new MaterialEntity
                    {
                        MaterialCode = cellForCode.StringValue.Trim(),
                        MaterialBatch = cellForBatch?.StringValue.Trim() ?? "",
                        ActualNumber = cellForNumber.StringValue.ToNumber(),
                        MaterialName = cellForName.StringValue
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// 获取试剂部数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private IList<MaterialEntity> GetSjbMaterialData(string filePath)
        {
            var result = new List<MaterialEntity>();
            var workbook = new Workbook(filePath);
            var sheet = workbook.Worksheets[0];
            var materialCodeIdx = ExcelHelper.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = ExcelHelper.GetColIndexByColName(sheet, "批号");
            var materialNumberIndex = ExcelHelper.GetColIndexByColName(sheet, "数量");
            var materialNameIndex = ExcelHelper.GetColIndexByColName(sheet, "物料名称");
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
                    var cellForCode = sheet.Cells[i, materialCodeIdx];
                    var cellForBatch = sheet.Cells[i, materialBatchIdx];
                    var cellForNumber = sheet.Cells[i, materialNumberIndex];
                    var cellForName = sheet.Cells[i, materialNameIndex];
                    if (cellForCode == null)
                    {
                        break;
                    }
                    result.Add(new MaterialEntity
                    {
                        MaterialCode = cellForCode.StringValue.Trim(),
                        MaterialBatch = cellForBatch?.StringValue.Trim() ?? "",
                        ActualNumber = cellForNumber.StringValue.ToNumber(),
                        MaterialName = cellForName.StringValue
                    });
                }
            }
            return result;
        }

        private void InitSheetFirstRow(Worksheet sheet)
        {
            sheet.Cells[0, 0].Value = "物料代码";
            sheet.Cells[0, 1].Value = "物料名称";
            sheet.Cells[0, 2].Value = "物料批次";
            sheet.Cells[0, 3].Value = "数量";
            sheet.Cells.SetColumnWidth(0, 50);
            sheet.Cells.SetColumnWidth(1, 50);
            sheet.Cells.SetColumnWidth(2, 30);
            sheet.Cells.SetColumnWidth(3, 20);
        }
        private void BuildExcelFile(List<MaterialEntity> remainingData, List<MaterialEntity> txyOnlyData)
        {
            Workbook workbook = new Workbook();
            workbook.Worksheets.Clear();
            workbook.Worksheets.Add("试剂部多的");
            workbook.Worksheets.Add("同兴源多的");
            var sheetSjb = workbook.Worksheets["试剂部多的"];
            var sheetTxy = workbook.Worksheets["同兴源多的"];
            this.InitSheetFirstRow(sheetSjb);
            this.InitSheetFirstRow(sheetTxy);
            //写入数据
            for (int i = 1; i <= remainingData.Count; i++)
            {
                sheetSjb.Cells[i, 0].Value = remainingData[i - 1].MaterialCode;
                sheetSjb.Cells[i, 1].Value = remainingData[i - 1].MaterialName;
                sheetSjb.Cells[i, 2].Value = remainingData[i - 1].MaterialBatch;
                sheetSjb.Cells[i, 3].Value = remainingData[i - 1].ActualNumber;
            }
            for (int i = 1; i <= txyOnlyData.Count; i++)
            {
                sheetTxy.Cells[i, 0].Value = txyOnlyData[i - 1].MaterialCode;
                sheetTxy.Cells[i, 1].Value = txyOnlyData[i - 1].MaterialName;
                sheetTxy.Cells[i, 2].Value = txyOnlyData[i - 1].MaterialBatch;
                sheetTxy.Cells[i, 3].Value = txyOnlyData[i - 1].ActualNumber;
            }
            var destPath = Path.GetDirectoryName(TbFile2.Text) + "/盘点结果数据表.xls";
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

        /// <summary>
        /// 数据校对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TbFile.Text == "" || !File.Exists(TbFile.Text))
            {
                Common.Alert("请选择同兴源库存文件。");
                return;
            }
            if (TbFile2.Text == "" || !File.Exists(TbFile2.Text))
            {
                Common.Alert("请选择试剂部盘点文件。");
                return;
            }
            logBuilder.Clear();
            BtnStart.Enabled = false;
            this.AddLog("开始盘点数据");

            var txyMaterialData = this.GetTxyMaterialData(TbFile.Text);
            var sjbMaterialData = this.GetSjbMaterialData(TbFile2.Text);
            //算法 = 试剂部 - 同兴源（物料代码 + 物料批次为主键）
            //数据预处理
            var txyData = txyMaterialData.GroupBy(x => $"{x.MaterialCode}|||{x.MaterialBatch}")
                .Select(x =>
                {
                    var temp = x.First();
                    return new MaterialEntity()
                    {
                        MaterialCode = temp.MaterialCode,
                        MaterialName = temp.MaterialName,
                        MaterialBatch = temp.MaterialBatch,
                        ActualNumber = x.Where(x2 => x2.ActualNumber > 0).Sum(x1 => x1.ActualNumber)
                    };
                }).ToList();
            var sjbData = sjbMaterialData.GroupBy(x => $"{x.MaterialCode}|||{x.MaterialBatch}")
                .Select(x =>
                {
                    var temp = x.First();
                    return new MaterialEntity()
                    {
                        MaterialCode = temp.MaterialCode,
                        MaterialName = temp.MaterialName,
                        MaterialBatch = temp.MaterialBatch,
                        ActualNumber = x.Where(x2 => x2.ActualNumber > 0).Sum(x1 => x1.ActualNumber)
                    };
                }).ToList();
            //数据校对
            var resultData = new List<MaterialEntity>();
            sjbData.ForEach(x =>
            {
                var txyHasModel = txyData.SingleOrDefault(x1 => x1.MaterialCode == x.MaterialCode && x1.MaterialBatch == x.MaterialBatch);
                if (txyHasModel != null)
                {
                    var remainingNumber = x.ActualNumber - txyHasModel.ActualNumber;//剩余数量
                    if (remainingNumber > 0)
                    {
                        resultData.Add(new MaterialEntity
                        {
                            MaterialCode = txyHasModel.MaterialCode,
                            MaterialName = txyHasModel.MaterialName,
                            MaterialBatch = txyHasModel.MaterialBatch,
                            ActualNumber = remainingNumber,

                        });
                    }
                }
                else
                {
                    if (x.ActualNumber > 0)
                    {
                        resultData.Add(x);
                    }
                }
            });
            var txyOnlyData = txyData.FindAll(x => !sjbData.Any(x1 => x1.MaterialBatch == x.MaterialBatch && x1.MaterialCode == x.MaterialCode));
            txyOnlyData.ForEach(x => x.IsTxyOnly = true);
            // resultData.AddRange(txyOnlyData);
            this.AddLog("盘点结束！");
            this.BuildExcelFile(resultData, txyOnlyData);
            BtnStart.Enabled = true;
        }

        /// <summary>
        /// 价格合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPriceMerge_Click(object sender, EventArgs e)
        {
            this.AddLog("开始将同兴源数量提取到试剂部！");
            var txyData = this.GetTxyMaterialData(TbFile.Text);
            this.AddLog("提取数量完成，正在生成Excel！");
            this.BuildPriceMergeExcelFile(TbFile2.Text, txyData);
            this.AddLog("生成成功！");

        }

        private void BuildPriceMergeExcelFile(string filePath, IList<MaterialEntity> txyData)
        {
            //数据变化
            var txyDic = new Dictionary<string, double>();
            txyData.ToList().ForEach((x) =>
            {
                var dicKey = $"{x.MaterialCode}$|${x.MaterialBatch}";
                if (!txyDic.ContainsKey(dicKey))
                {
                    txyDic.Add(dicKey, x.ActualNumber);
                }
            });

            var workbook = new Workbook(filePath);
            var sheet = workbook.Worksheets[0];
            var materialCodeIdx = ExcelHelper.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = ExcelHelper.GetColIndexByColName(sheet, "批号");
            //增加一列(同兴源数量列)
            var txyCountIndex = 11;//sheet.Cells.Columns.Count;
            sheet.Cells[0, txyCountIndex].Value = "同兴源数量";
            //写入数据
            int rowCount = sheet.Cells.MaxRow;
            //为什么用<= rowCount?因为LastRowNum是从0开始计算，所以真正的行数应该是LastRowNum+1
            for (int i = START_ROW_INDEX; i <= rowCount; i++)
            {
                var materialCode = sheet.Cells[i, materialCodeIdx].StringValue.Trim();
                var materialBatch = sheet.Cells[i, materialBatchIdx].StringValue.Trim() ?? "";
                var dicKey = $"{materialCode}$|${materialBatch}";
                double count;
                if (txyDic.TryGetValue(dicKey, out count))
                {
                    sheet.Cells[i, txyCountIndex].Value = count;
                }
            }
            var destPath = Path.GetDirectoryName(TbFile2.Text) + "/试剂部库存盘点表.xls";
            workbook.Save(destPath);
            Common.NotifyAndOpenFile(destPath);
        }
        #endregion

        #region 关闭按钮
        private void BtnClose_Click(object sender, EventArgs e)
        {
            FrmMain.RemoveTabPage("inventory_count");
        }
        
        #endregion

       
    }
}
