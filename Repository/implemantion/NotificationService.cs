

using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Vaccinatedapi.data;

public class NotificationService : IHostedService
{
    dbdatacontexts _dbdatacontexts;
    private Timer? _timer;
    string firebasetoken = "AAAA2u2yNOk:APA91bEDJtb1U0Ze3WeetQW4irPuAamD7IcdvB6sFP29FKdfY5cIvsgRXl74l0O8mJjZkI0oS9quiFEZmIpxwpO3kq7F4auQPN17rNS6JCe-AonPOd7ShYpBYEDPF0fq57YdYOpts3ml";

    public NotificationService(dbdatacontexts context)
    {
        _dbdatacontexts = context;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        var kids = _dbdatacontexts.kids.ToList();
        var vaccine = _dbdatacontexts.vaccine.ToList();
        foreach (var item in kids)
        {
            foreach (var vaccines in vaccine)
            {
                double s = DateTime.Now.Subtract(Convert.ToDateTime(item.pirth_date)).TotalDays;
                int days = (int)Math.Floor(s);
                if (days == vaccines.days_to_take - 1)
                {
                    var parent = _dbdatacontexts.parents.Where(x => x.ID == item.father_id || x.ID == item.mother_id).ToList();
                    foreach (var paren in parent)
                    {
                        if (paren.firebase_token != null)
                        {
                            _timer = new Timer(async state => await SendPostAsync(paren.firebase_token, vaccines.ID, item.name), null, TimeSpan.Zero, TimeSpan.FromDays(1));
                        }
                    }
                }
            }
        }
        //     _timer = new Timer(async state => await SendPostAsync("diKXt0moSFGaKxorXE4aQf:APA91bGfOwMwhCXNmg_7hlZJle1aAGkgxnMgS-fSaTfz9cCmdVPErBNv989490wwq8duiiW7YxTcalqe475dH7ybQTSSdFDapIcw2_8pwDxmq-8vDFbHBwpurh20sOafVAP8oNvTN3An"), null, TimeSpan.Zero, TimeSpan.FromSeconds(70));

        // Start the service by creating a timer that sends POST requests every 5 seconds
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop the service by disposing the timer
        _timer?.Dispose();
        return Task.CompletedTask;
    }

    private async Task SendPostAsync(string firebase_token, int? _vaccines_Id, string _kids_name)
    {

        // Create a new HttpClient
        using (var httpClient = new HttpClient())
        {
            // Set the Authorization header

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", firebasetoken);

            // Create a new object with the data to send
            var data = new
            {
                message = "message",
            };
            var notification= new
            {
                title ="الطفل بحاجة إلى التلقيح"+ _kids_name,
                body = " غدا موعد تقليح طفلك  يرجى التوجه إلى اقرب مركز صحي"
            };
            
            var to = firebase_token;
            var cont = new
            {
                data = data,
                notification=notification,
                to = to
            };

            // Serialize the object to JSON
            string jsonPayload = JsonConvert.SerializeObject(cont);

            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the POST request to the FCM API endpoint
            string firebaseendpoint = "https://fcm.googleapis.com/fcm/send";
            try
            {
                var response = await httpClient.PostAsync(firebaseendpoint, content);
                var apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST request sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending POST request: {ex.Message}");
            }
        }
    }
}