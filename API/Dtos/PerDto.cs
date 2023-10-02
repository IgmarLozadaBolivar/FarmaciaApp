using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class PerDto : BaseEntity
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public int IdCiuFK { get; set; }
        public DateOnly FechaNac { get; set; }
        public int IdGenFK { get; set; }
        public int IdTipoPerFK { get; set; }
    }
}