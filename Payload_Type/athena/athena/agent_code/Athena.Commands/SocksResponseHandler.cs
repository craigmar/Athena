﻿using Athena.Models.Mythic.Response;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Athena.Commands
{
    public class SocksResponseHandler
    {
        //private static ConcurrentDictionary<int, SocksMessage> messagesOut = new ConcurrentDictionary<int, SocksMessage>();
        private static ConcurrentBag<SocksMessage> messagesOut = new ConcurrentBag<SocksMessage>();
        public static async Task AddSocksMessageAsync(SocksMessage sm)
        {
            messagesOut.Add(sm);
        }
        public static async Task<List<SocksMessage>> GetSocksMessagesAsync()
        {
            if(messagesOut.IsEmpty)
            {
                return new List<SocksMessage>();
            }

            List<SocksMessage> messages = new List<SocksMessage>(messagesOut);
            messagesOut.Clear();
            foreach (var message in messages)
            {
                message.PrepareMessage();
            }
            return messages;
        }
    }
}
