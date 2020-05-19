using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HaciendaEndPoints.Models
{
    public partial class Token
    {
        public Guid Id { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("refresh_expires_in")]
        public string RefreshExpiresIn { get; set; }
        [JsonProperty("session_state")]
        public string SessionState { get; set; }
    }
}
