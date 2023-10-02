using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona : BaseEntity
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public int IdCiuFK { get; set; }
        public Ciudad Ciudades { get; set; }
        public DateOnly FechaNac { get; set; }
        public int IdGenFK { get; set; }
        public Genero Generos { get; set; }
        public int IdTipoPerFK { get; set; }
        public TipoPersona TipoPersonas { get; set; }
    }
}