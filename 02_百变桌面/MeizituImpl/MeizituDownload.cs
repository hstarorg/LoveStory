using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AngleSharp.Parser.Html;
using ImageDownloadInterface;

namespace MeizituImpl
{
    public class MeizituDownload:IImageDownload
    {
        public void DownloadImages(string folder, Action callback)
        {
            var categoryUrls = this.GetCategoryUrls();
            this.DownloadImages(categoryUrls.Select(x=>x.Key).ToList());
            callback();
        }

        private List<KeyValuePair<string,string>> GetCategoryUrls()
        {
            const string URL = "http://www.meizitu.com/";
            var webClient = new WebClient();
            var html = webClient.DownloadString(URL);
            var doc = new HtmlParser(html).Parse();
            return doc.QuerySelectorAll(".topmodel a").Select(a => new KeyValuePair<string, string>(a.GetAttribute("href"), a.NodeValue)).ToList();
        }

        private void DownloadImages(List<string> urlList)
        {
            
        }
    }
}
