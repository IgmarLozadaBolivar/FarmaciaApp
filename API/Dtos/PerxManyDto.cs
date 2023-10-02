using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PerxManyDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public int IdCiuFK { get; set; }
        public CiuDto ciudades { get; set; }
        public DateOnly FechaNac { get; set; }
        public int IdGenFK { get; set; }
        public GenDto generos { get; set; }
        public int IdTipoPerFK { get; set; }
        public TipoPerDto tipoPersonas { get; set; }
    }
}