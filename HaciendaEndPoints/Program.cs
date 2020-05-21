using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HaciendaEndPoints
{
    class Program
    {
        static async Task Main(string[] args)
        {
         //   Console.WriteLine("ATV");
            EndPoints endPoints = new EndPoints();
            XmlSigned xml = new XmlSigned();
         //   xml.xml();
         //   // endPoints.GetToken();
          // endPoints.RefreshToken();
           // await endPoints.GetElectronicBill();
          Company data= xml.DeserializeToObject<Company>("Test.xml");
            Console.WriteLine(data.Clave);
        }
    }
}
