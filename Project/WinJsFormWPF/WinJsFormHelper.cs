using CefSharp;
using HttpServerLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WinjsLib;

namespace WinJsFormWPF
{
    public partial class WinJsForm
    {
        //注册事件
        public void RegisterEvent()
        {
            EventControl.SetEvent("WIN_SetHeight", SetHeight);
            EventControl.SetEvent("WIN_SetWidth", SetWidth);
            EventControl.SetEvent("WIN_ShowDevTools", ShowDevTools);
            EventControl.SetEvent("WIN_SetTitle", SetTitle);

            EventControl.SetEvent("WIN_Show", Show);
            EventControl.SetEvent("WIN_Hide", Hide);
            EventControl.SetEvent("WIN_Close", Close);
            EventControl.SetEvent("WIN_Minimize", Minimize);
            EventControl.SetEvent("WIN_Reload", Reload);
            EventControl.SetEvent("WIN_Back", Back);
            EventControl.SetEvent("WIN_Url", Url);
            EventControl.SetEvent("WIN_Hello", Hello);

        }
        //发送信息
        private void Send(HttpResponse response, object obj)
        {
            if (obj != null) response.FromText(obj.ToString());
            else response.FromText("YES");
            response.Send();
        }

        private void IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string js = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"assets/view/JsLib.js"));
            this.webBrowser.ExecuteScriptAsync(js);
        }

        //页面初始化(奇怪，没有生效)
        private void FrameEndFunc(object sender, FrameLoadEndEventArgs e)
        {
            string js = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"assets/JsLib.js"));
            this.webBrowser.ExecuteScriptAsync(js);
            //webBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
            //webBrowser.ShowDevTools();
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="message"></param>
        public void SetHeight(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(SetHeight), message);
                return;
            }
            WPFMs ms = JsonConvert.DeserializeObject<WPFMs>(message.Json);
            this.Height = ms.Height;
            Send(message.Response, null);


        }

        /// <summary>
        /// 开发者模式
        /// </summary>
        /// <param name="message"></param>
        public void ShowDevTools(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(ShowDevTools), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            webBrowser.WebBrowser.ShowDevTools();
            Send(message.Response, null);


        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="message"></param>
        public void SetWidth(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(SetWidth), message);
                return;
            }
            WPFMs ms = JsonConvert.DeserializeObject<WPFMs>(message.Json);
            this.Width = ms.Width;
            Send(message.Response, null);


        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="message"></param>
        public void SetTitle(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(SetTitle), message);
                return;
            }
            WPFMs ms = JsonConvert.DeserializeObject<WPFMs>(message.Json);
            this.Title = ms.Title;
            Send(message.Response, null);


        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message"></param>
        public void Show(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Show), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            this.Show();
            Send(message.Response, null);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="message"></param>
        public void Hide(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Hide), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            this.Hide();
            Send(message.Response, null);
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="message"></param>
        public void Minimize(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Minimize), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            this.WindowState = System.Windows.WindowState.Minimized;
            Send(message.Response, null);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="message"></param>
        public void Reload(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Reload), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            this.webBrowser.Reload();
            Send(message.Response, null);
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="message"></param>
        public void Url(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Url), message);
                return;
            }
            WPFMs ms = JsonConvert.DeserializeObject<WPFMs>(message.Json);
            this.webBrowser.Load(ms.Url);
            Send(message.Response, null);
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="message"></param>
        public void Back(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Back), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            this.webBrowser.Back();
            Send(message.Response, null);
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="message"></param>
        public void Close(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Close), message);
                return;
            }
            //WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
            this.Hide();
            this.Close();
            System.Environment.Exit(0);
            Send(message.Response, null);
        }

        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="message"></param>
        public void Hello(WinjsLib.Message message)
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send, new WinjsLib.EventControl.WinjsEvent(Hello), message);
                return;
            }
            Send(message.Response, "欢迎使用WinJs，如有建议或需求，请联系作者651010646@qq.com");
        }
    }
}
