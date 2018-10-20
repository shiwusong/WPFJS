var WPFJs = (function () {
  let defaultMs = {
    EventName: 'Hello',
  }

  function sendMessage(ms, func) {
    let sendMs = Object.assign({}, defaultMs, ms);
    let jsonStr = JSON.stringify(sendMs);
    fetch('/', {
      method: 'WIN',
      headers: {
        credentials: 'include',
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: jsonStr
    }).
    then((response) => {
      return response.text()
    }).
    then((data) => {
      func(data)
    });
  }

  WIN_SUCCESS = function (data) {
    //alert("data:",data)
    console.log(data)
  }




  //设置标题
  function SetTitle(name) {
    let ms = {
      EventName: 'WIN_SetTitle',
      Title: name
    }
    sendMessage(ms, WIN_SUCCESS);
  }

  //设置窗口高度
  function SetHeight(Height) {
    let ms = {
      EventName: 'WIN_SetHeight',
      Height: Height
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //设置窗口宽度
  function SetWidth(Width) {
    let ms = {
      EventName: 'WIN_SetWidth',
      Width: Width
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //显现
  function Show() {
    let ms = {
      EventName: 'WIN_Show'
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //隐藏
  function Hide() {
    let ms = {
      EventName: 'WIN_Hide'
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //关闭
  function Close() {
    let ms = {
      EventName: 'WIN_Close'
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //最小化
  function Minimize() {
    let ms = {
      EventName: 'WIN_Minimize'
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //刷新
  function Reload() {
    let ms = {
      EventName: 'WIN_Reload'
    };
    sendMessage(ms, WIN_SUCCESS);
  }
  //后退
  function Back() {
    let ms = {
      EventName: 'WIN_Back'
    };
    sendMessage(ms, WIN_SUCCESS);
  }

  //重定向
  function Url(url) {
    let ms = {
      EventName: 'WIN_Url',
      Url: url
    };
    sendMessage(ms, WIN_SUCCESS);
  }

  //显示开发者工具
  function ShowDevTools() {
    let ms = {
      EventName: 'WIN_ShowDevTools',
    };
    sendMessage(ms, WIN_SUCCESS);
  }

  function Help() {
    console.log('*****************************')
    console.log('帮助：')
    console.log('WPFJs.SetTitle(title):  设置标题');
    console.log('WPFJs.SetHeight(height):  设置高度');
    console.log('WPFJs.SetWidth(width):  设置宽度');
    console.log('WPFJs.Show():  显示');
    console.log('WPFJs.Hide():  隐藏');
    console.log('WPFJs.Close():  关闭');
    console.log('WPFJs.Minimize():  最小化');
    console.log('WPFJs.Reload():  刷新');
    console.log('WPFJs.Back():  后退');
    console.log('WPFJs.Url(url):  跳转');
    console.log('WPFJs.ShowDevTools():  显示开发者工具');
    console.log('WPFJs.Help():  显示帮助');
    console.log('*****************************')
  }

  return {
    SetTitle: SetTitle,
    SetHeight: SetHeight,
    SetWidth: SetWidth,
    Show: Show,
    Hide: Hide,
    Close: Close,
    Minimize: Minimize,
    Reload: Reload,
    Back: Back,
    Url: Url,
    ShowDevTools: ShowDevTools,
    Help: Help,
  }
})();
document.onkeydown = function (e) {
  if (e.keyCode == 74 && e.ctrlKey) {
    WPFJs.ShowDevTools();
  }
}