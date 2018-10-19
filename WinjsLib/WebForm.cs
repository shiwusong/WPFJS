using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using HttpServerLib;
using System.IO;

namespace WinjsLib
{
    public partial class WebForm : Form { 

        private delegate void Callback(WinjsLib.Message message);//委托
        public ChromiumWebBrowser browser; //游览器对象
        string formName { get; set; } //窗口名称
        int port { get; set; }//端口
        string defaultUrl { get; set; }//默认路径
        public List<HeaderBar> bars = new List<HeaderBar>();//自定义边框
        public WinjsSettings winjssettings { get; set; }
        public WebForm(WinjsSettings settings)
        {
            InitializeComponent();
            //读取配置
            this.winjssettings = settings;
            //注册事件
            EventControl.SetEvent("WIN_SetFormHeight", SetHeight);
            EventControl.SetEvent("WIN_CloseHeaderBorder", CloseHeaderBorder);
            EventControl.SetEvent("WIN_OpenHeaderBorder", OpenHeaderBorder);
            EventControl.SetEvent("WIN_SetCustomBorder", SetCustomBorder);
            EventControl.SetEvent("WIN_SetHeaderTitle", SetHeaderTitle);
            EventControl.SetEvent("WIN_ClearCustomBorder", ClearCustomBorder);
            EventControl.SetEvent("WIN_SetFormWidth", SetWidth);
            EventControl.SetEvent("WIN_Show", Show);
            EventControl.SetEvent("WIN_Hide", Hide);
            EventControl.SetEvent("WIN_Close", Close);
            EventControl.SetEvent("WIN_Minimize", Minimize);
            EventControl.SetEvent("WIN_Reload", Reload);
            EventControl.SetEvent("WIN_Back", Back);
            EventControl.SetEvent("WIN_BrowserLoad", BrowserLoad);
            EventControl.SetEvent("WIN_ShowDevTools", ShowDevTools);
        }

        private void WebForm_Load(object sender, EventArgs e)
        {
            //设置游览器
            var settings = new CefSettings
            {
                Locale = "zh-CN",
                AcceptLanguageList = "zh-CN",
                MultiThreadedMessageLoop = true
            };
            Cef.Initialize(settings);

            browser = new ChromiumWebBrowser(winjssettings.Url);
            browser.MenuHandler = new MenuHandler();
            this.Controls.Add(browser);


            browser.Dock = DockStyle.Fill;
            //设置基本属性
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Height = winjssettings.Height;
            this.Width = winjssettings.Width;
            this.Text = winjssettings.Title;
            for (int i = 0; i < winjssettings.CustomBorders.Length; i++)
            {
                HeaderBar bar = new HeaderBar(this);
                bar.SetHeight(winjssettings.CustomBorders[i].Height);
                bar.SetLeft(winjssettings.CustomBorders[i].Left);
                bar.SetTop(winjssettings.CustomBorders[i].Top);
                bar.SetWidth(winjssettings.CustomBorders[i].Width);
                this.bars.Add(bar);
                bar.TopMost = true;
                bar.Show();
            }
            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;//添加事件
            browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(FrameEndFunc);
        }

        private void FrameEndFunc(object sender, FrameLoadEndEventArgs e)
        {
            //MessageBox.Show("加载完毕");
            string js = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"base\1.js"));
            browser.ExecuteScriptAsync(js);

        }

        private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs args)
        {
            if (args.IsBrowserInitialized)
            {
                
            }
        }

        #region 基本事件
        private void Send(HttpResponse response,object obj)
        {
            if (obj != null) response.FromText(obj.ToString());
            else response.FromText("YES");
            response.Send();
        }
        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="message"></param>
        public void SetHeight(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(SetHeight);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                this.Height = ms.Height;
                Send(message.Response,null);
            }
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="message"></param>
        public void SetWidth(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(SetWidth);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                this.Width = ms.Width;
                Send(message.Response,null);
            }
        }

        /// <summary>
        /// 获取窗口名称
        /// </summary>
        /// <param name="message"></param>
        public void GetFormName(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(GetFormName);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                Send(message.Response,this.formName);
            }
        }

        /// <summary>
        /// 获取端口
        /// </summary>
        /// <param name="message"></param>
        public void GetPort(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(GetPort);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                Send(message.Response, this.port);
            }
        }

        /// <summary>
        /// 设置窗口名称
        /// </summary>
        /// <param name="message"></param>
        public void SetFormName(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(SetFormName);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                this.formName = ms.FormName;
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 设置端口
        /// </summary>
        /// <param name="message"></param>
        public void SetPort(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(SetPort);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                this.port = ms.Port;
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 关闭默认边框
        /// </summary>
        /// <param name="message"></param>
        public void CloseHeaderBorder(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(CloseHeaderBorder);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 开启默认边框
        /// </summary>
        /// <param name="message"></param>
        public void OpenHeaderBorder(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(OpenHeaderBorder);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                Send(message.Response, null);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;

            }
        }

        /// <summary>
        /// 设置程序标题
        /// </summary>
        /// <param name="message"></param>
        public void SetHeaderTitle(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(SetHeaderTitle);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                this.Text = ms.Title;
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 设置自定义边框
        /// </summary>
        /// <param name="message"></param>
        public void SetCustomBorder(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(SetCustomBorder);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                for (int i = 0; i < ms.CustomBorders.Length; i++)
                {
                    HeaderBar bar = new HeaderBar(this);
                    bar.SetHeight(ms.CustomBorders[i].Height);
                    bar.SetLeft(ms.CustomBorders[i].Left);
                    bar.SetTop(ms.CustomBorders[i].Top);
                    bar.SetWidth(ms.CustomBorders[i].Width);
                    this.bars.Add(bar);
                    bar.Show();
                }
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 清除自定义边框
        /// </summary>
        /// <param name="message"></param>
        public void ClearCustomBorder(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(ClearCustomBorder);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                for (int i = 0; i < this.bars.Count; i++)
                {
                    HeaderBar bar = this.bars[0];
                    this.bars.RemoveAt(0);
                    bar.Close();
                }
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message"></param>
        public void Show(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(Show);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.Show();
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="message"></param>
        public void Hide(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(Hide);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.Hide();
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="message"></param>
        public void Close(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(Close);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.Hide();
                this.Close();
                System.Environment.Exit(0);
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="message"></param>
        public void Minimize(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(Minimize);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 游览器刷新
        /// </summary>
        /// <param name="message"></param>
        public void Reload(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(Reload);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.browser.Reload();
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 游览器后退
        /// </summary>
        /// <param name="message"></param>
        public void Back(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(Back);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.browser.Back();
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 游览器重定向
        /// </summary>
        /// <param name="message"></param>
        public void BrowserLoad(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(BrowserLoad);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                WinMs ms = JsonConvert.DeserializeObject<WinMs>(message.Json);
                this.browser.Load(ms.Url);
                Send(message.Response, null);
            }
        }

        /// <summary>
        /// 游览器显示开发者工具
        /// </summary>
        /// <param name="message"></param>
        public void ShowDevTools(WinjsLib.Message message)
        {
            if (this.InvokeRequired)
            {
                Callback d = new Callback(ShowDevTools);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.browser.ShowDevTools();
                Send(message.Response, null);
            }
        }


        #endregion

        

        private void WebForm_Activated(object sender, EventArgs e)
        {
            //Console.WriteLine("active");
            //for (int i = 0; i < this.bars.Count; i++)
            //{
            //    if(bars[i].TopMost == false)
            //    bars[i].SetTopMost(true);
            //}
            //this.Focus();
            //this.Activate();
            //button1.Focus();


        }

        private void WebForm_Deactivate(object sender, EventArgs e)
        {
            //Console.WriteLine("deactive");
            //for (int i = 0; i < this.bars.Count; i++)
            //{
            //    bars[i].SetTopMost(false);
            //}
        }

        /// <summary>
        /// 开发者模式快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.J) && e.Control)
            {
                browser.ShowDevTools();
            }
        }
    }


    /// <summary>
    /// 取消游览器右键菜单事件
    /// </summary>
    internal class MenuHandler : IContextMenuHandler
    {
        public bool OnBeforeContextMenu(IWebBrowser browser, IBrowser ibrower, IFrame iframe, IContextMenuParams icontextmenuparams, IMenuModel imenumodel)
        {
            return false;
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
            //throw new NotImplementedException();
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            // throw new NotImplementedException();
        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            throw new NotImplementedException();
        }

        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear();
        }
    }
}
