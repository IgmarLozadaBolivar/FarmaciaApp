using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class TipoPerDto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}