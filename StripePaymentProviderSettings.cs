using Microsoft.Extensions.Configuration;
using System.IO;

namespace PaymentBotSample
{ 
    public class StripePaymentProviderSettings
    {
        private static IConfigurationBuilder builder = new ConfigurationBuilder();
        private static IConfigurationRoot GetConfigRoot()
        {
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            return root;
        }

        public static string GetPublishableApiKey
        {
            get { return GetConfigRoot().GetSection("stripePaymentProvider").GetSection("publishableApiKey").Value; }
        }

        public static string GetSecretKey
        {
            get { return GetConfigRoot().GetSection("stripePaymentProvider").GetSection("secretKey").Value; }
        }
    }
}
