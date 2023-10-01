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
    public class TipoPerRepo : GenericRepo<TipoPersona>, ITipoPer
    {
        protected readonly DbAppContext _context;

        public TipoPerRepo(DbAppContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<TipoPersona>> GetAllAsync()
        {
            return await _context.TipoPersonas
                .Include(p => p.Personas)
                .ToListAsync();
        }

        public override async Task<TipoPersona> GetByIdAsync(int id)
        {
            return await _context.TipoPersonas
            .Include(p => p.Personas)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}