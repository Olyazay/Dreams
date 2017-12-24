using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Admin
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherTimer timer = new DispatcherTimer();
            //Action<string> sd = (s) =>
            //{
            //};
            //Checker.CheckInternet(sd);
            timer.Interval = new TimeSpan(0,0,10);
            timer.Tick += Timer_Tick;
            //timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://yandex.ru");
                    request.Timeout = 5000;
                    request.Credentials = CredentialCache.DefaultNetworkCredentials;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    { }
                    else
                        System.Windows.MessageBox.Show("Проверьте подключение к интернету");
                }
                catch
                {
                    System.Windows.MessageBox.Show("Проверьте подключение к интернету");
                }
            });
        }

    }
}
