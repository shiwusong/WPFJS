using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpServerLib;
using Newtonsoft.Json;

namespace WinjsLib
{
    public class WPFJsServer : HttpServer
    {
        //单例
        static private WPFJsServer winjsServer = null;

        //get instance
        static public WPFJsServer getInstance(string ipAddress, int port, string root) {
            if (winjsServer == null)
            {
                winjsServer = new WPFJsServer(ipAddress, port, root);
            }
            return winjsServer;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口号</param>
        private WPFJsServer(string ipAddress, int port, string root)
            : base(ipAddress, port, root)
        {

        }
        public new void Start()
        {
            //开启事件分发线程
            //EventDispatch dispatch = new EventDispatch();
            //dispatch.Start();
            //开启http服务器线程
            Thread t = new Thread(base.Start);
            t.Start();
        }

        public override void OnWIN(HttpRequest request, HttpResponse response)
        {
            string jsonStr = request.Body;
            WinjsLib.Message ms = JsonConvert.DeserializeObject<WinjsLib.Message>(jsonStr);
            ms.Json = jsonStr;
            ms.Request = request;
            ms.Response = response;
            //MsgQueue.getInstance().handle(MsgQueue.PUSH, ms);
            if(EventControl.HasEvent(ms.EventName))
            EventControl.GetEvent(ms.EventName)(ms);
        }

        public override void OnGet(HttpRequest request, HttpResponse response)
        {


            //当文件不存在时应返回404状态码
            string requestURL = request.URL;
            requestURL = requestURL.Replace("/", @"\").Replace("\\..", "").TrimStart('\\');
            string requestFile = Path.Combine(ServerRoot, requestURL);

            //判断地址中是否存在扩展名
            string extension = Path.GetExtension(requestFile);

            //根据有无扩展名按照两种不同链接进行处
            if (extension != "")
            {
                //从文件中返回HTTP响应
                if (extension == ".json")
                {
                    response = response.FromJsonFile(requestFile);
                }
                else
                {
                    response = response.FromFile(requestFile);
                }
            }
            //发送HTTP响应
            response.Send();
        }

        public override void OnDefault(HttpRequest request, HttpResponse response)
        {

        }
    }
}
