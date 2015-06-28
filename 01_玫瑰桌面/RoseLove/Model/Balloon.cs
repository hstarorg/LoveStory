using System.Drawing;

namespace RoseLove.Model
{
    public class Balloon
    {
        /// <summary>
        /// 图片源
        /// </summary>
        public Image Img { get; set; }

        /// <summary>
        /// 偏移角度
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// 当前位置X
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 当前位置Y
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 下降速度
        /// </summary>
        public int Speed { get; set; }

        public void CalcLocation()
        {
            Y += this.Speed;
        }
    }
}
