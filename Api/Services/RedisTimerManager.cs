using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace TransactionAppletaApi
{
    public class RedisTimerManager
    {
        private const int PERIOD = 300000;
        private static Timer Timer;

        #region X.成员方法[Run]
        /// <summary>
        /// 运行定时器
        /// </summary>
        public static void Run()
        {
            TimerCallback tc = new TimerCallback(TodoTask);
            Timer = new Timer(tc, "service", 0, PERIOD);
        }
        public static void Start()
        {
            Timer.Change(0, 10000);
        }
        public static void Stop()
        {
            Timer.Change(Timeout.Infinite, PERIOD);
        }
        #endregion

        #region Y.内部方法[TodoTask]
        /// <summary>
        /// 检查待处理任务
        /// </summary>
        static void TodoTask(object state)
        {
            //执行任务
            ShelvesProductRunner sp = new ShelvesProductRunner();
            sp.Do();
        }
        #endregion
    }
}