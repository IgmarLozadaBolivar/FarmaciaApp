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
    public class CiuRepo : GenericRepo<Ciudad>, ICiu
    {
        protected readonly DbAppContext _context;

        public CiuRepo(DbAppContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Ciudad>> GetAllAsync()
        {
            return await _context.Ciudades
                .Include(p => p.Personas)
                .ToListAsync();
        }

        public override async Task<Ciudad> GetByIdAsync(int id)
        {
            return await _context.Ciudades
            .Include(p => p.Personas)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}