using Microsoft.AspNetCore.SignalR;
using Vaccinatedapi.multiModels;

namespace Vaccinatedapi
{
    public class SignlrHub:Hub
    {
        public void  BroadcastVaccine(vaccine_kids vaccine_Kids){
            Clients.All.SendAsync("ReciveNotifcation",vaccine_Kids);
        }
        public void  Broadcastmessage(string message){
            Clients.All.SendAsync("ReciveNotifcation",message);
        }
    }
}