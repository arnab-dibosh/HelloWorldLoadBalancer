using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Text;

namespace ApiCaller
{
    class ApiCaller
    {
        static void Main(string[] args)
        {
            string nginxUrl = ReadConfigurationValue("NGINX_URL");
            int count = Convert.ToInt32(ReadConfigurationValue("RECORD_COUNT"));

            if (string.IsNullOrEmpty(nginxUrl))
                return;

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/text";
            client.Encoding = Encoding.UTF8;

            for (int i = 0; i < count; i++)
            {
                string str = client.UploadString(nginxUrl, "");
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }

        static IConfigurationRoot config;
        static string ReadConfigurationValue(string strKey)
        {
            if (config == null)
            {
                config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
            }


            var strVal = Environment.GetEnvironmentVariable(strKey);
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = config[strKey];
            }
            return strVal;
        }
    }
}
