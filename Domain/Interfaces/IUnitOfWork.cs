using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPais Paises { get; }
        IDep Departamentos { get; }
        ICiu Ciudades { get; }
        IPer Personas { get; }
        IGen Generos { get; }
        ITipoPer TipoPersonas { get; }
        Task<int> SaveAsync();
    }
}