using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse
{
    public class Checker
    {
        public static async Task<bool> CheckConnection()
        {
            bool isExist = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://yandex.ru");
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                    isExist = true;
                else
                    isExist = false;
            }
            catch
            {
                isExist = false;
            }
            return isExist;
        }
        //private static Action<string> InterruptedAction;
        public static async void CheckInternet(Action<string> action)
        {
            bool r = await CheckConnection();
            //int count = 0;
            if (r)
            {
                action("https://yandex.ru");
            }
        }
        }
}
