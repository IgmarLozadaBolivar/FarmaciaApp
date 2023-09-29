using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pais : BaseEntity
    {
        public string Nombre { get; set; }
        public string Capital { get; set; }
        public string CodISO { get; set; }
        public string Moneda { get; set; }
        public string Idioma { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}