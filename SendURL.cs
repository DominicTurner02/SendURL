using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using System;
using Logger = Rocket.Core.Logging.Logger;

namespace SendURL
{
    public class SendURL : RocketPlugin<ConfigurationSendURL>
    {
        public static SendURL Instance { get; private set; }

        protected override void Load()
        {
            base.Load();
            Instance = this;
            Logger.LogWarning("\n Loading SendURL, made by Mr.Kwabs...");
            Logger.LogWarning($"\n Default URL Description: {Instance.Configuration.Instance.DefaultDescription}");
            Logger.LogWarning("\n Successfully loaded SendURL, made by Mr.Kwabs!");
        }

        protected override void Unload()
        {
            Instance = null;
            base.Unload();
        }

        public void URLSend(UnturnedPlayer Player, string URL, string Description)
        {
            Player.Player.sendBrowserRequest(Description, URL);
        }

        public static string ValidateURL(string URL)
        {
            bool ValidURL = Uri.TryCreate(URL, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (ValidURL)
            {
                return URL;
            }
            else
            {
                return "FALSE";
            }
        }
    }
}
