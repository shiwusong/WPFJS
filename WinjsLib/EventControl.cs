using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinjsLib
{

    public class EventControl
    {
        static EventControl instance = null; //单例对象
        public delegate void WinjsEvent(Message message); //委托声明
        public Dictionary<string,WinjsEvent> events; //委托字典
        ILogger logger = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        EventControl()
        {
            events = new Dictionary<string, WinjsEvent>();
        }

        #region 公共方法

        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        public static WinjsEvent GetEvent(string eventName)
        {
            return GetInstance()._GetEvent(eventName);
        }

        /// <summary>
        /// 判断是否有此事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        public static Boolean HasEvent(string eventName)
        {
            return GetInstance()._HasEvent(eventName);
        }

        /// <summary>
        /// 设置事件
        /// 使用方式： EventControl.SetEvent("XXX") = function
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static void SetEvent(string eventName,WinjsEvent func)
        {
            GetInstance()._SetEvent(eventName,func);
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        private static EventControl GetInstance()
        {
            if (instance != null) return instance;
            instance = new EventControl();
            return instance;
        }


        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        WinjsEvent _GetEvent(string eventName)
        {
            if (events.ContainsKey(eventName))
            {
                return events[eventName];
            }
            Log("未注册此事件:" + eventName);
            return null;
        }

        /// <summary>
        /// 判断是否有此事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        Boolean _HasEvent(string eventName)
        {
            return events.ContainsKey(eventName);
        }

        /// <summary>
        /// 设置事件
        /// SetEvent
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        void _SetEvent(string eventName,WinjsEvent func)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName] = func;
            }
            else
            {
                WinjsEvent winjsEvent = func;
                events.Add(eventName, winjsEvent);
            }
        }

        /// <summary>
        /// 日志注入
        /// </summary>
        /// <param name="message">信息</param>
        void Log(object message)
        {
            if (logger == null) return;
            logger.Log(message);
        }
        #endregion
    }
}
