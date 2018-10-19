using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteLib
{
    public class Sqlite
    {
        private string dbName;
        private string password;
        private SQLiteConnection conn;

        /// <summary>
        /// 创建sqlite数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="password"></param>
        static public void Create(string dbName,string password)
        {
            SQLiteConnection.CreateFile(dbName);
            SQLiteConnection conn = new SQLiteConnection("Data Source="+dbName+";Version=3;");
            conn.SetPassword(password);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="password"></param>
        Sqlite(string dbName, string password)
        {
            this.dbName = dbName;
            this.password = password;
            conn = new SQLiteConnection("Data Source=" + dbName + ";Version=3;password=" + password);
        }


    }
}
