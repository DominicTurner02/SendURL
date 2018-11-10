using Rocket.API;

namespace SendURL
{
    public class ConfigurationSendURL : IRocketPluginConfiguration
    {
        public string DefaultDescription;

        public void LoadDefaults()
        {
            DefaultDescription = "You have been requested to open this URL.";
        }

    }
}
