using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class PerRepo : GenericRepo<Persona>, IPer
    {
        protected readonly DbAppContext _context;

        public PerRepo(DbAppContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas
            .Include(p => p.Ciudad)
                .Include(p => p.Genero)
                .Include(p => p.TipoPersona)
                .ToListAsync();
        }

        public override async Task<Persona> GetByIdAsync(int id)
        {
            return await _context.Personas
            .Include(p => p.Ciudad)
            .Include(p => p.Genero)
            .Include(p => p.TipoPersona)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}