using Aspose.Cells;

namespace ClumsyAssistant.Utilities
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 获取sheet中命名列的索引
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="colName">列头名称（如果重名，会获取第一次出现的索引）</param>
        /// <param name="headerRowIndex">列头所在的行索引</param>
        /// <returns></returns>
        public static int GetColIndexByColName(Worksheet sheet, string colName, int headerRowIndex = 0)
        {
            var cells = sheet.Cells;
            for (int i = 0; i <= cells.MaxColumn; i++)
            {
                if (cells[headerRowIndex, i].StringValue.StartsWith(colName))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
