using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace HaciendaEndPoints.Config
{
    public class Configuration
    {
        public static IConfiguration CF { get;}

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                CF = builder.Build();
        }

        public static string TokenEndPoints => CF["EndPoints:TokenEndPoint"];
        public static string VouchersEndPoint => CF["EndPoints:VouchersEndPoint"];
        public static string Client_Id => CF["EndPoints:Client_Id"];
        public static string UserName => CF["EndPoints:UserName"];
        public static string Password => CF["EndPoints:Password"];
        public static string ContentType => CF["EndPoints:Content-Type"];
    }
}
