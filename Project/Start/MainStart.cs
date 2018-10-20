using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinJsFormWPF;
using WinjsLib;

namespace Start
{
    class MainStart
    {
        [STAThread]
        static void Main(string[] args) {

            //初始化程序
            Start.App app = new Start.App();
            app.InitializeComponent();

            //读取配置文件
            WPFJsSettings winjsSettings = WPFJsSettings.ParseFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"assets\settings.json"));

            WinJsForm form = new WinJsForm(winjsSettings);
            app.MainWindow = form;
            app.Run(form);
        }
    }
}
