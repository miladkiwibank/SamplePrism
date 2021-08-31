using Infrastructure.Network;
using SamplePrism.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;

namespace SamplePrism.Services
{
    public static class InfluxConfig
    {
        public static readonly Lazy<InfluxConnection> influxConnection
            = new Lazy<InfluxConnection>(() => InfluxConnection.Single);

        public class InfluxConnection
        {
            private static InfluxConnection m_single;

            private InfluxConnection(string uri, string dbName, string username, string password)
            {
                if (string.IsNullOrWhiteSpace(username))
                    Client = new InfluxClient(new Uri(uri));
                else
                    Client = new InfluxClient(new Uri(uri), username, password);
                DbName = DbName;
                var uri2 = new Uri(Uri.EscapeUriString(uri));
                if (uri2.Host.Telnet(uri2.Port))
                {
                    Task<InfluxResult> task = Client.CreateDatabaseAsync(dbName);
                    task.Wait();
                }
            }

            public string DbName { get; set; }

            public InfluxClient Client { get; set; }

            public static InfluxConnection Single
            {
                get
                {
                    if (m_single == null)
                    {
                        string uri = $"http://{Settings.Default.InfluxIP}:{Settings.Default.InfluxPort}";
                        m_single = new InfluxConnection(uri, Settings.Default.InfluxDbName,
                            Settings.Default.InfluxUser, Settings.Default.InfluxPwd);
                    }
                    return m_single;
                }
            }

            public bool HasTable(string tableName)
            {
                var task = Client.ShowMeasurementsAsync(DbName);
                task.Wait();
                var result = task.Result;
                if (!result.Succeeded) return false;
                if (result.Series.Count <= 0) return false;
                var series = result.Series[0];
                if (series.Rows.Count <= 0) return false;
                var row = series.Rows[0];
                return tableName == row.Name;
            }

            public void DropDb(string dbName)
            {
                Client.DropDatabaseAsync(dbName).Wait();
            }

            public void DropDb()
            {
                DropDb(DbName);
            }

            public void Write<T>(string dbName, string tableName, params T[] data) where T : new()
            {
                Client.WriteAsync(dbName, tableName, data).Wait();
            }

            public void Write<T>(string tableName, params T[] data) where T : new()
            {
                Write(DbName, tableName, data);
            }

            public void CreateRetentionPolicy(string dbName, string policyName, string duration, int level, bool isDefault)
            {
                Client.CreateRetentionPolicyAsync(dbName, policyName, duration, level, isDefault).Wait();
            }

            public void CreateRetentionPolicy(string policyName, string duration, int level, bool isDefault)
            {
                CreateRetentionPolicy(DbName, policyName, duration, level, isDefault);
            }

            public void CreateContinuousQuery(string dbName, string name, string queryString)
            {
                Client.CreateContinuousQuery(dbName, name, queryString).Wait();
            }

            public void CreateContinuousQuery(string name, string queryString)
            {
                CreateContinuousQuery(DbName, name, queryString);
            }
        }
    }
}
