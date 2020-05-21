using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace HaciendaEndPoints
{
    [XmlRoot(ElementName = "FacturaElectronica",Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronica")]
    public class Company
    {
        public Company()
        {
            Employees = new List<Employee>();
        }

        [XmlElement(ElementName = "Employee")]
        public List<Employee> Employees { get; set; }

        [XmlElement(ElementName = "CodigoActividad")]
        public string CodigoActividad { get; set; }

        [XmlElement(ElementName = "Clave")]
        public string Clave
        {
            get; set;
        }
        [XmlElement(ElementName = "FechaEmision")]
        public DateTime FechaEmision
        {
            get; set;
        }

        public Employee this[string name]
        {
            get { return Employees.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)); }
        }
    }
}
