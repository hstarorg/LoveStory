namespace ClumsyAssistant.Models
{
    public class MaterialEntity
    {
        /// <summary>
        /// 物料代码
        /// </summary>
        public  string MaterialCode { get; set; }

        /// <summary>
        /// 物料批次
        /// </summary>
        public string MaterialBatch { get; set; }

        /// <summary>
        /// 实存数量
        /// </summary>
        public double ActualNumber { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 是否是同兴源独有的
        /// </summary>
        public bool IsTxyOnly { get; set; }
    }
}
