using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinjsLib
{
    public class WPFJsSettings
    {
        // 入口文件
        public string Url { get; set; }
        // 标题
        public string Title { get; set; }
        // 
        public string FormName { get; set; }
        // 端口
        public int Port { get; set; }
        // 高度
        public int Height { get; set; }
        // 宽度
        public int Width { get; set; }
        // 是否可调整高度宽度
        public bool CanResize { get; set;}
        // 拖拽边框
        public CustomBorder[] CustomBorders { get; set; }

        public static WPFJsSettings ParseFile(string filePath)
        {
            WPFJsSettings ws = null ;
            //文件路径
            try
            {
                if (File.Exists(filePath))
                {
                    ws = JsonConvert.DeserializeObject<WPFJsSettings>(File.ReadAllText(filePath));
                }
                else
                {
                    Console.WriteLine("文件:" + filePath + " 不存在");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ws;
        }
    }
}
