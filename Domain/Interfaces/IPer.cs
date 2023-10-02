using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPer : IGenericRepo<Persona>
    {
        Task LoadCiudadesAsync(int personaId);
        Task LoadGenerosAsync(Persona persona);
        Task LoadTipoPersonasAsync(Persona persona);
    }
}