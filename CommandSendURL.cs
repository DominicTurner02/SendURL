using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SendURL
{
    class CommandSendURL : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "SendURL";
        public string Help => "Sends a URL Prompt to User/s.";
        public string Syntax => "[Player || *] [URL] <Description>";
        public List<string> Aliases => new List<string>() { "SURL" };
        public List<string> Permissions => new List<string>() { "sendurl" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            string Description;
            try
            {
                Description = command[2];
            }
            catch(IndexOutOfRangeException)
            {
                Description = SendURL.Instance.Configuration.Instance.DefaultDescription;
            }

            string URL = SendURL.ValidateURL(command[1]);
            if (URL == "FALSE")
            {
                UnturnedChat.Say(caller, "That is an incorrect URL!", Color.red);
                return;
            }

            if (command[0] == "*")
            {
                foreach (SteamPlayer Victim in Provider.clients) { SendURL.Instance.URLSend(UnturnedPlayer.FromSteamPlayer(Victim), URL, Description); }
                Logger.LogWarning($"{caller.DisplayName} has sent a URL [{URL}] to Everyone.");
                UnturnedChat.Say(caller, $"Successfully sent a URL to Everyone.", Color.yellow);
            }
            else
            {
                UnturnedPlayer Victim;
                try
                {
                    Victim = UnturnedPlayer.FromName(command[0]);
                    string VictimName = Victim.DisplayName;
                }
                catch
                {
                    UnturnedChat.Say(caller, "User not found!", Color.red);
                    return;
                }
                SendURL.Instance.URLSend(Victim, URL, Description);
                Logger.LogWarning($"{caller.DisplayName} has sent a URL [{URL}] to {Victim.DisplayName}.");
                UnturnedChat.Say(caller, $"Successfully sent a URL to {Victim.DisplayName}.", Color.yellow);
            }
        }
    }
}
