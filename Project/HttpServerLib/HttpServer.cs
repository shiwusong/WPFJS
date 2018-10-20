using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServerLib
{
    public class HttpServer
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP { get; private set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; private set; }

        /// <summary>
        /// 服务器目录
        /// </summary>
        public string ServerRoot { get; private set; }

        /// <summary>
        /// 是否运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 服务端Socet
        /// </summary>
        private TcpListener serverListener;

        /// <summary>
        /// 日志接口
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iPAddress">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="root">根目录</param>
        private HttpServer(IPAddress iPAddress, int port, string root)
        {
            this.ServerIP = iPAddress.ToString();
            this.ServerPort = port;
            //获取程序目录下的指定文件夹
            root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, root);
            //如果指定目录不存在则采用默认目录
            if (!Directory.Exists(root))
            {
                this.ServerRoot = AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                this.ServerRoot = root;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="root">根目录</param>
        public HttpServer(string ipAddress, int port, string root) :
            this(IPAddress.Parse(ipAddress), port, root)
        { }

        #region 公开方法

        /// <summary>
        /// 开启服务器
        /// </summary>
        public void Start()
        {
            if (IsRunning) return;

            //创建服务端Socket
            this.serverListener = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
            this.IsRunning = true;
            this.serverListener.Start();
            this.Log(string.Format("Sever is running at http://{0}:{1}", ServerIP, ServerPort));

            try
            {
                while (IsRunning)
                {
                    TcpClient client = serverListener.AcceptTcpClient();
                    Thread requestThread = new Thread(() => { ProcessRequest(client); });
                    requestThread.Start();
                    //ProcessRequest(client);
                }
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 处理客户端请求
        /// </summary>
        /// <param name="handler">客户端Socket</param>
        private void ProcessRequest(TcpClient handler)
        {
            //处理请求
            Stream clientStream = handler.GetStream();

            if (clientStream == null) return;

            //构造HTTP请求
            HttpRequest request = new HttpRequest(clientStream);
            request.Logger = Logger;

            //构造HTTP响应
            HttpResponse response = new HttpResponse(clientStream);
            response.Logger = Logger;
            Log(request.URL);
            //处理请求类型
            switch (request.Method)
            {
                case "GET":
                    OnGet(request, response);
                    break;
                case "WIN":
                    OnWIN(request, response);
                    break;
                default:
                    OnDefault(request, response);
                    break;
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">日志消息</param>
        protected void Log(object message)
        {
            if (Logger != null) Logger.Log(message);
        }

        #region 虚方法

        /// <summary>
        /// 响应Get请求
        /// </summary>
        /// <param name="request">请求报文</param>
        public virtual void OnGet(HttpRequest request, HttpResponse response)
        {

        }

        /// <summary>
        /// 响应WIN请求
        /// </summary>
        /// <param name="request"></param>
        public virtual void OnWIN(HttpRequest request, HttpResponse response)
        {

        }

        /// <summary>
        /// 响应默认请求
        /// </summary>

        public virtual void OnDefault(HttpRequest request, HttpResponse response)
        {

        }

        #endregion

        #endregion
    }
}
