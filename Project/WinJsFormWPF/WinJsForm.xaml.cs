using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinjsLib;

namespace WinJsFormWPF
{
    /// <summary>
    /// WinJsForm.xaml 的交互逻辑
    /// </summary>
    public partial class WinJsForm : Window
    {
        private WPFJsSettings settings;
        public ChromiumWebBrowser webBrowser;
        public WinJsForm(WPFJsSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            Thread t = new Thread(new ParameterizedThreadStart(ServerStart));
            t.Start(settings.Port);
            Setting(settings);
            RegisterEvent();

        }

        //启动http程序
        private void ServerStart(object port)
        {
            WPFJsServer winjsServer = WPFJsServer.getInstance("127.0.0.1", (int)port, @"assets/view");
            winjsServer.Start();

        }
        public void Setting(WPFJsSettings settings) {

            #region 游览器设置
            //var Cefsettings = new CefSettings
            //{
            //    Locale = "zh-CN",
            //    AcceptLanguageList = "zh-CN",
            //    MultiThreadedMessageLoop = true
            //};
            //Cefsettings.CefCommandLineArgs.Add("disable-gpu", "1");
            //Cefsettings.CefCommandLineArgs.Add("force-device-scale-factor", "1");
            //Cef.Initialize(Cefsettings);
            //var webbrowser = new ChromiumWebBrowser(settings.Url);
            //webbrowser.MenuHandler = new MenuHandler();
            //var winform = new WindowsFormsHost()
            //{
            //    Child = webbrowser
            //};

            ////绑定，和js交互的关键，注册一个JsObj，这个可以自定义，然后对于js来说，第二个参数this就代表了JsObj
            ////this.Content = winform;
            //wrap.Children.Add(winform);

            var Cefsettings = new CefSettings
            {
                Locale = "zh-CN",
                AcceptLanguageList = "zh-CN",
                MultiThreadedMessageLoop = true
            };
            
            Cefsettings.CefCommandLineArgs.Add("disable-gpu", "1");
            Cefsettings.CefCommandLineArgs.Add("force-device-scale-factor", "1");
            CefSharp.Cef.Initialize(Cefsettings);
            webBrowser = new CefSharp.Wpf.ChromiumWebBrowser();
            webBrowser.BrowserSettings.WebSecurity = CefState.Disabled;
            webBrowser.Height = settings.Height;
            webBrowser.Width = settings.Width;
            webBrowser.Address = settings.Url;
            //webBrowser.IsBrowserInitializedChanged += IsBrowserInitializedChanged;
            webBrowser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(FrameEndFunc);
            webBrowser.MenuHandler = new MenuHandler();
            wrap.Children.Add(webBrowser);


            #endregion

            #region 窗体设置
            this.Title = settings.Title;//标题
            this.Height = settings.Height;//高度
            this.Width = settings.Width;//宽度
            if(settings.CanResize)
            this.ResizeMode = ResizeMode.CanResizeWithGrip;//是否可以拉伸
            //自定义头部
            for (int i = 0; i < settings.CustomBorders.Length; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Height = settings.CustomBorders[i].Height;
                rectangle.Width = settings.CustomBorders[i].Width;
                rectangle.Fill = new SolidColorBrush(Colors.Gray);
                rectangle.Opacity = 0.01;
                Canvas.SetLeft(rectangle, 0);
                Canvas.SetTop(rectangle, 0);
                rectangle.MouseLeftButtonDown += MouseLeftDown;
                wrap.Children.Add(rectangle);
            }
            
            
            #endregion
        }

        private void MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            webBrowser.Height = e.NewSize.Height;
            webBrowser.Width = e.NewSize.Width;
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
