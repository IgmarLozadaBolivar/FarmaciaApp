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

        /* public override async Task<(int totalRegistros, IEnumerable<Ciudad> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Ciudades as IQueryable<Ciudad>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }
            var totalRegistros = await query.CountAsync();
            var registros = await query
                                    .Include(u => u.Personas)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return (totalRegistros, registros);
        } */

        public async Task LoadPersonasAsync(int ciudadId)
        {
            var ciudad = await _context.Ciudades
                .Include(g => g.Personas)
                .FirstOrDefaultAsync(g => g.Id == ciudadId);

            if (ciudad != null)
            {
                // No es necesario hacer nada, ya la lista o coleccion esta cargada o es nula
            }
        }
    }
}