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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ClumsyAssistant.Pages
{
    public partial class DataCheckPage : UserControl
    {
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

        /// <summary>
        /// 根据Excel文件路径，获取单据编号
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private List<string> GetDocumentNumber(string filepath)
        {
            var results = new List<string>();
            IWorkbook workbook = null;
            try
            {
                var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                if (filepath.IndexOf(".xlsx", StringComparison.Ordinal) > 0) // 2007版本
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (filepath.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
                {
                    workbook = new HSSFWorkbook(fs);
                }
            }
            catch (Exception ex)
            {
                this.Alert("读取Excel出错=>" + ex.Message);
            }
            var sheet = workbook.GetSheetAt(0);
            int startRowIndex = 1; //为什么不是0？因为首行是列头
            int colIndex = 1;//默认第二列是单据编号
            int rowCount = sheet.LastRowNum;
            for (int i = startRowIndex; i < rowCount; i++)
            {
                results.Add(sheet.GetRow(i).GetCell(colIndex).StringCellValue);
            }
            return results;
        }
//        function expect(arr1, arr2) {
//    var results = [];
//    for (var i = 0, len = arr1.length; i < len; i++) {
//        if (arr2.indexOf(arr1[i]) < 0) {
//            results.push(arr1[i]);
//        }
//    }
//    return results;
//}

        private List<string> GetNoInListOneAndNotInListTwo(List<string> list1, List<string> list2)
        {
            var results = new List<string>();
            foreach (var str in list1)
            {
                if (list2.IndexOf(str) < 0)
                {
                    results.Add(str);
                }
            }
            return results;
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

        #endregion

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TbFile.Text == "" || !File.Exists(TbFile.Text))
            {
                this.Alert("请选择同兴源出入库文件。");
                return;
            }
            if (TbFile2.Text == "" || !File.Exists(TbFile2.Text))
            {
                this.Alert("请选择试剂部出入库文件。");
                return;
            }

            var documentNumberList = this.GetDocumentNumber(TbFile.Text).Distinct().ToList();
            var documentNumber2List = this.GetDocumentNumber(TbFile2.Text).Distinct().ToList();

            var numberList = documentNumber2List.Except(documentNumberList).ToList();
            Alert(numberList.Count.ToString());
        }
    }
}
