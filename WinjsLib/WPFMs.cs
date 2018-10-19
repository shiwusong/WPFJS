using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinjsLib
{
    public class WPFMs
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string FormName { get; set; }
        public int Port { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public CustomBorder[] CustomBorders { get; set; }
    }
    public class CustomBorder
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

}
