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
    public class GenRepo : GenericRepo<Genero>, IGen
    {
        protected readonly DbAppContext _context;

        public GenRepo(DbAppContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Genero>> GetAllAsync()
        {
            return await _context.Generos
                .Include(p => p.Personas)
                .ToListAsync();
        }

        public override async Task<Genero> GetByIdAsync(int id)
        {
            return await _context.Generos
            .Include(p => p.Personas)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}