
using System;

namespace ImageDownloadInterface
{
    public interface IImageDownload
    {
        void DownloadImages(string folder,Action callback);
    }
}
