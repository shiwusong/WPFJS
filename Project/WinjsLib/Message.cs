using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServerLib;
using Newtonsoft.Json;

namespace WinjsLib
{
    public class Message
    {
        /// <summary>
        /// 同步
        /// </summary>
        // public const int SYNCHRONOUS = 0;
        /// <summary>
        /// 异步
        /// </summary>
        // public const int ASYNCHRONOUS = 1;

        /// <summary>
        /// 消息的名称
        /// </summary>
        // public string Name { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        // public int Type { get; set; }

        /// <summary>
        /// 消息源
        /// </summary>
        //public string Source { get; set; }

        public string Json { get; set; }

        /// <summary>
        /// 依附数据
        /// </summary>
        //public object Data { get; set; }

        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }
        
    }
}
