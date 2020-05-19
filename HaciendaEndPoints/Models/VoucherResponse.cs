using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HaciendaEndPoints.Models
{
    public partial class VoucherResponse
    {
        public Guid Id { get; set; }
        [JsonProperty("clave")]
        public string Clave { get; set; }
        [JsonProperty("fecha")]
        public DateTime Fecha { get; set; }
        [JsonProperty("ind-estado")]
        public string IntEstado { get; set; }
        [JsonProperty("respuesta-xml")]
        public string RespuestaXml { get; set; }
    }
}
