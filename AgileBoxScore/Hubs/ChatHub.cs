﻿using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AgileBoxScore.RChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}