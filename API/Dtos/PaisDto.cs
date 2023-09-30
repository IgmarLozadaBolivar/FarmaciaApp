using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class PaisDto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Capital { get; set; }
        public string CodIso { get; set; }
        public string Moneda { get; set; }
        public string Idioma { get; set; }
        public List<DepDto> departamentos { get; set; }
    }
}