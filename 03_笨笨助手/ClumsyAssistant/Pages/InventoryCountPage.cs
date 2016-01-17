using System;
using System.Collections.Generic;
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

        private int GetColIndexByColName(Worksheet sheet, string name)
        {
            var cells = sheet.Cells;
            for (int i = 0; i <= cells.MaxColumn; i++)
            {
                if (cells[0, i].StringValue.StartsWith(name))
                {
                    return i;
                }
            }
            return -1;
        }


        private IList<MaterialEntity> GetTxyMaterialData(string filePath)
        {
            var result = new List<MaterialEntity>();
            var workbook = new Workbook(filePath);
            var sheet = workbook.Worksheets[0];
            var materialCodeIdx = this.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = this.GetColIndexByColName(sheet, "批号");
            var materialNumberIndex = this.GetColIndexByColName(sheet, "常用单位数量");
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
                    if (cellForCode == null)
                    {
                        break;
                    }
                    result.Add(new MaterialEntity
                    {
                        MaterialCode = cellForCode.StringValue.Trim(),
                        MaterialBatch = cellForBatch?.StringValue.Trim() ?? "",
                        ActualNumber = cellForNumber.StringValue.ToNumber()
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
            var materialCodeIdx = this.GetColIndexByColName(sheet, "物料代码");
            var materialBatchIdx = this.GetColIndexByColName(sheet, "物料批次");
            var materialNumberIndex = this.GetColIndexByColName(sheet, "实存数量");
            if (materialCodeIdx < 0)
            {
                Common.Alert("没有找到[物料代码]列！");
            }
            else if (materialBatchIdx < 0)
            {
                Common.Alert("没有找到[物料批次]列！");
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
                    if (cellForCode == null)
                    {
                        break;
                    }
                    result.Add(new MaterialEntity
                    {
                        MaterialCode = cellForCode.StringValue.Trim(),
                        MaterialBatch = cellForBatch?.StringValue.Trim() ?? "",
                        ActualNumber = cellForNumber.StringValue.ToNumber()
                    });
                }
            }
            return result;
        }

        private void InitSheetFirstRow(Worksheet sheet)
        {
            sheet.Cells[0, 0].Value = "物料代码";
            sheet.Cells[0, 1].Value = "物料批次";
            sheet.Cells[0, 2].Value = "数量";
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
                sheetSjb.Cells[i, 1].Value = remainingData[i - 1].MaterialBatch;
                sheetSjb.Cells[i, 2].Value = remainingData[i - 1].ActualNumber;
            }
            for (int i = 1; i <= txyOnlyData.Count; i++)
            {
                sheetTxy.Cells[i, 0].Value = txyOnlyData[i - 1].MaterialCode;
                sheetTxy.Cells[i, 1].Value = txyOnlyData[i - 1].MaterialBatch;
                sheetTxy.Cells[i, 2].Value = txyOnlyData[i - 1].ActualNumber;
            }
            var cell = sheetSjb.Cells[0, 0].Value;
            var destPath = Path.GetDirectoryName(TbFile2.Text) + "/盘点结果数据表.xls";
            workbook.Save(destPath);
        }

        //        private void FillBackgroundColor(string filepath, IList<string> numberList)
        //        {
        //            this.AddLog("正在对差异文件进行着色");
        //            var workbook = new Workbook(filepath);
        //            var sheet = workbook.Worksheets[0];
        //            var rowCount = sheet.Cells.MaxRow;
        //
        //
        //            var cellStyle = workbook.CreateStyle();
        //            cellStyle.BackgroundColor = Color.LightGreen;
        //            cellStyle.Pattern = BackgroundType.Solid;
        //            cellStyle.Font.Size = 40;
        //            //new Style {BackgroundColor = Color.LightGreen};
        //            //                        cellStyle.FillForegroundColor = HSSFColor.LightGreen.Index;
        //            //                        cellStyle.FillPattern = FillPattern.SolidForeground;
        //
        //            int colIndex = this.GetDocumentNumberColIndex(sheet);
        //            int colorRowCount = 0;
        //            for (int i = START_ROW_INDEX; i <= rowCount; i++)
        //            {
        //                Cell cell = sheet.Cells[i, colIndex];
        //                if (cell != null && numberList.Contains(cell.StringValue.Trim()))
        //                {
        //                    var style = cell.GetStyle();
        //                    style.Pattern = BackgroundType.Solid;
        //                    style.ForegroundColor = Color.Red;
        //                    cell.SetStyle(style);
        //                    colorRowCount++;
        //                }
        //            }
        //            var destPath = Path.GetDirectoryName(filepath) + "/" + Path.GetFileName(filepath).Replace(".xls", "_校对结果.xls");
        //
        //            workbook.Save(destPath);
        //            this.AddLog("已将差异单据在试剂部文件中着色，共标记行数：" + colorRowCount);
        //            if (DialogResult.Yes == MessageBox.Show("已将差异单据在试剂部文件中着色，共标记行数：" + colorRowCount + "，是否打开结果文件？", "提示", MessageBoxButtons.YesNo))
        //            {
        //                System.Diagnostics.Process.Start(destPath);
        //            }
        //        }
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
                            MaterialBatch = txyHasModel.MaterialBatch,
                            ActualNumber = remainingNumber
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
        #endregion

        #region 拖拽控制和关闭按钮
        private void BtnClose_Click(object sender, EventArgs e)
        {
            FrmMain.RemoveTabPage("inventory_count");
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
        #endregion
    }
}
