using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ApiCaller
{
    class ApiCaller
    {
        static string nginxUrl;
        static int recordCount;
        static int threadCount;
        static string connectionString;
        static string dockerName;
        static int count = 1;
        static void Main(string[] args)
        {
            int tDelay = recordCount = Convert.ToInt32(ReadConfigurationValue("Thread_Delay"));
            nginxUrl = ReadConfigurationValue("NGINX_URL");
            recordCount = Convert.ToInt32(ReadConfigurationValue("RECORD_COUNT"));
            threadCount = Convert.ToInt32(ReadConfigurationValue("Thread_Count"));
            connectionString = ReadConfigurationValue("DBConnectionString");
            dockerName = Environment.MachineName;// ReadConfigurationValue("DockerName");
            DBUtility.ConnectionString = connectionString;
            bool isNeedSleep = recordCount*threadCount > 10000 ? true : false;

            if (string.IsNullOrEmpty(nginxUrl))
                return;

            for(int i = 1; i<=threadCount; i++)
            {
                Thread threadNode = new Thread(() => CallWebAPI(i));
                threadNode.Start();

                if (isNeedSleep)
                    Thread.Sleep(tDelay);
            }

            Console.ReadKey();
        }

        static void CallWebAPI(int threadNo)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/text";
            client.Encoding = Encoding.UTF8;

            for (int i = 1; i <= recordCount; i++)
            {
                string strUrl = nginxUrl;
                string apiResponseTime = "";
                string apiStartTime = "";

                string transID = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                transID = "T" + threadNo + "-R" + i + "-" + transID;

                string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                DBUtility.InsertTable(transID, dockerName, "Hello-World", requestTime);

                strUrl = strUrl + transID;
                strUrl = strUrl + "&clientRequestTime=" + requestTime;

                try
                {
                    string retVal = client.UploadString(strUrl, "POST", "");

                    Console.WriteLine(">> " + retVal);

                    if (retVal.IndexOf('|') > -1)
                    {
                        transID = retVal.Split('|')[0];
                        apiResponseTime = retVal.Split('|')[1];
                        apiStartTime = retVal.Split('|')[2];
                    }

                    requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                    DBUtility.UpdateTable(transID, dockerName, requestTime, apiResponseTime, apiStartTime);
                    count++;
                }
                catch (Exception)
                {
                    //File.AppendAllText("Log.txt", count + ". " + ex.Message + Environment.NewLine);
                }

            }
        }


        static IConfigurationRoot config;
        static string ReadConfigurationValue(string strKey)
        {
           
            var strVal = Environment.GetEnvironmentVariable(strKey);
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = config[strKey];
            }
            return strVal;
        }
    }
}
