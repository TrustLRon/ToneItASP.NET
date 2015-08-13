using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using TestLog.Models;
using TestLog.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestLog
{
    public class ChatHub : Hub
    {

        public static List<string> UsersOnline = new List<string>();
        public static List<string> UsersFullName = new List<string>();
        

        public override Task OnConnected()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            string userName = Context.User.Identity.Name;
            var currentUser = manager.FindByName(userName);
            string firstName = currentUser.FirstName;
            string lastName = currentUser.LastName;
            string fullName = firstName + " " + lastName;

            string name = Context.User.Identity.Name;
            string message = " (" + fullName + ") is getting Toned.";
                        
            if (UsersOnline.IndexOf(name) == -1)
            {
                UsersOnline.Add(name);
                Groups.Add(Context.ConnectionId, name);
                Clients.All.Joined(name, message);                
            }
            
            GetUsers(UsersOnline);

            return base.OnConnected();
        }


        public void Send(string name, string message, string tone)
        {
            string privateTag = "";
            Clients.All.AddNewMessageToPage(name, message, tone, privateTag);
        }


        public void SendPrivate(string name, string message, string privateUserName, string tone)
        {
            string privateTag = "Private";

            Clients.Group(privateUserName).AddNewMessageToPage(name, message, tone, privateTag);
            Clients.Caller.AddNewMessageToPage(name, message, tone, privateTag);
        }


        public void Join(string name, string message)
        {
            Clients.All.Joined(name, message);
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            string message = " left :'(";

            if (UsersOnline.IndexOf(name) > -1)
            {
                UsersOnline.Remove(name);
                Clients.All.Leaving(name, message);                
            }
            
            GetUsers(UsersOnline);            
            
            return base.OnDisconnected(stopCalled);
        }


        public void GetUsers(List<string> UsersOnline)
        {
            Clients.All.updateOnline(UsersOnline);
        }
    }
}