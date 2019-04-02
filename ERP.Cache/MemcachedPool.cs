using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class MemcachedPool
    {
        /// <summary>
        /// 初始化Memcached连接池
        /// </summary>
        public void MemcachedSockIOPool()
        {
            char[] separator = { ',' };
            string[] serverlist = System.Configuration.ConfigurationManager.AppSettings["Memcached.ServerList"].Split(separator);
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
        }
    }
}
