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

        public override async Task<(int totalRegistros, IEnumerable<Genero> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Generos as IQueryable<Genero>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
                query = query.Where(p => p.Abreviatura.ToLower().Contains(search));
                query = query.Where(p => p.Descripcion.ToLower().Contains(search));
            }
            var totalRegistros = await query.CountAsync();
            var registros = await query
                                    .Include(u => u.Personas)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}