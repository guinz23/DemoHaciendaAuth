using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HaciendaEndPoints.Config;
using HaciendaEndPoints.Models;
using Microsoft.Extensions.Configuration;

namespace HaciendaEndPoints
{
    public class EndPoints
    {
        private string TokenEndPoint = Configuration.TokenEndPoints;
        private string ClientId = Configuration.Client_Id;
        private string VouchersEndPoint = Configuration.VouchersEndPoint;
        private string Username = Configuration.UserName;
        private string Password = Configuration.Password;
        private string ContentType = Configuration.ContentType;

        public void GetToken()
        {

            var http = new HttpClient();
            var body = new List<KeyValuePair<string, string>>();
            body.Add(new KeyValuePair<string, string>("Content-Type",ContentType));
            body.Add(new KeyValuePair<string, string>("client_id",ClientId));
            body.Add(new KeyValuePair<string, string>("username", Username));
            body.Add(new KeyValuePair<string, string>("password", Password));
            body.Add(new KeyValuePair<string, string>("grant_type", "password"));
            var content = new FormUrlEncodedContent(body);
            HttpResponseMessage response = http.PostAsync(TokenEndPoint,content).Result;
            string res = response.Content.ReadAsStringAsync().Result;
           
            if (res != null)
            {

              Serializer(res,"create");

            }

        }

        public void Serializer(string res,string type)
        {
            Token tk = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(res);
            using (var db = new HACIENDAContext())
            {
                switch (type)
                {
                    case "create":
                        db.Token.Add(new Token { TokenType = tk.TokenType, ExpiresIn = tk.ExpiresIn,  RefreshToken = tk.RefreshToken, AccessToken = tk.AccessToken, RefreshExpiresIn = tk.RefreshExpiresIn, SessionState = tk.SessionState });
                        db.SaveChanges();
                        Console.WriteLine("Se creo un nuevo token");
                        break;
                    case "update":
                        var token = db.Token.FirstOrDefault(item => item.Id == Guid.Parse("5766f8fc-1f3b-4eb3-9913-314f3df0c351"));
                        if (token != null)
                        {
                            token.TokenType = tk.TokenType;
                            token.ExpiresIn = tk.ExpiresIn;
                            token.RefreshToken = tk.RefreshToken;
                            token.AccessToken = tk.AccessToken;
                            token.RefreshExpiresIn = tk.RefreshExpiresIn;
                            token.SessionState = tk.SessionState;
                            db.Token.Update(token);
                            db.SaveChanges();
                            Console.WriteLine("Se Actualizo el token");
                        }
                        break;
                }
            }
        }

        public void RefreshToken()
        {
            var http = new HttpClient();
            using (var db = new HACIENDAContext())
            {
                var tk = db.Token.Where(b => b.Id == Guid.Parse("12adc686-2914-402b-87fc-09828b4ff442"))
                    .FirstOrDefault();
                var body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("Content-Type",ContentType));
                body.Add(new KeyValuePair<string, string>("client_id",ClientId));
                body.Add(new KeyValuePair<string, string>("refresh_token", tk.RefreshToken));
                body.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                var content = new FormUrlEncodedContent(body);
                HttpResponseMessage response = http.PostAsync(TokenEndPoint, content).Result;
                string res = response.Content.ReadAsStringAsync().Result;
                if (!TokenExpired(response.StatusCode.ToString()))
                {
                    db.Remove(db.Token.Single(a => a.Id == tk.Id));
                    db.SaveChanges();
                    GetToken();
                }
                else
                {
                    Serializer(res,"update");
                   
                }
                
            }
        }

        public bool TokenExpired(string Status)
        {
            bool tokenState = false;
            switch (Status)
            {
                case "OK":
                    tokenState = true;
                    break;

                case "Unauthorized":
                    tokenState = false;
                    break;

                  case "BadRequest":
                     tokenState=false;
                    break;
            }
            return tokenState;
        }
        
        public void SendElectronicBill()
        {
         
        }

        public async Task<string> GetElectronicBill()
        {
            string responseBody;
            using (var db = new HACIENDAContext())
            {
                var tk =  db.Token.Where(b => b.Id == Guid.Parse("12adc686-2914-402b-87fc-09828b4ff442"))
                   .FirstOrDefault();
                var http = new HttpClient();
                http.DefaultRequestHeaders.Add("Authorization", "bearer " + tk.AccessToken);
                  responseBody= await http.GetStringAsync(VouchersEndPoint + "recepcion/" + "50604111915580001061700100001010000000024100005555");
                SerializerXml(responseBody);

            }
            return responseBody;
        }

        public void SerializerXml(string res)
        {
            VoucherResponse v = Newtonsoft.Json.JsonConvert.DeserializeObject<VoucherResponse>(res);
            using (var db = new HACIENDAContext())
            {
                db.VoucherResponse.Add(new VoucherResponse {
                   Clave =v.Clave,
                   Fecha=v.Fecha,
                   IntEstado=v.IntEstado,
                   RespuestaXml=v.RespuestaXml
                });
                db.SaveChanges();
                Console.WriteLine(String.Format("Se recibio una respuesta de hacienda del documento{0} con un status {1} ",v.Clave,v.IntEstado));
            }
           
        }
    }
}
