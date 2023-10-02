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
    public class DepRepo : GenericRepo<Departamento>, IDep
    {
        protected readonly DbAppContext _context;

        public DepRepo(DbAppContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Departamento>> GetAllAsync()
        {
            return await _context.Departamentos
                .Include(p => p.Ciudades)
                .ToListAsync();
        }

        public override async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Departamentos
            .Include(p => p.Ciudades)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRegistros, IEnumerable<Departamento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Departamentos as IQueryable<Departamento>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
                query = query.Where(p => p.CodISO.ToLower().Contains(search));
            }
            var totalRegistros = await query.CountAsync();
            var registros = await query
                                    .Include(u => u.Ciudades)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}