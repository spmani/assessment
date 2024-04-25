using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace EmailTrigger
{
    public class MyService
    {
        private Timer _timer;
        private const int TimerInterval = 12000;
        public void Start()
        {

            Console.WriteLine("My Service Started");
            _timer = new Timer(TimerInterval);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();

        }
        public void Stop()
        {
            Console.WriteLine("My Service Stoped");
            _timer?.Stop();
            _timer?.Dispose();

        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timer?.Stop();

            string url = "https://localhost:7213/api/Email/SyncEmailTrigger";
            HttpMessageHandler handler = new HttpClientHandler();

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = new TimeSpan(0, 2, 0)
            };

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.Content != null)
            {
                Console.WriteLine(response.Content);
            }



            Console.WriteLine("Triggered at: " + DateTime.Now);

            _timer?.Start();





        }
    }
}
