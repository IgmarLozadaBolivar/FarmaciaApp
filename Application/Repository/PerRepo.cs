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
            .Include(p => p.Ciudades)
                .Include(p => p.Generos)
                .Include(p => p.TipoPersonas)
                .ToListAsync();
        }

        public override async Task<Persona> GetByIdAsync(int id)
        {
            return await _context.Personas
            .Include(p => p.Ciudades)
            .Include(p => p.Generos)
            .Include(p => p.TipoPersonas)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        /* public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Personas as IQueryable<Persona>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombres.ToLower().Contains(search));
                query = query.Where(p => p.Apellidos.ToLower().Contains(search));
            }
            var totalRegistros = await query.CountAsync();
            var registros = await query
                                    .Include(u => u.Genero)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return (totalRegistros, registros);
        } */

        public async Task LoadCiudadesAsync(int personaId)
        {
            var persona = await _context.Personas
                .Include(p => p.Ciudades)
                .FirstOrDefaultAsync(p => p.Id == personaId);

            if (persona != null)
            {}
        }

        public async Task LoadGenerosAsync(Persona persona)
        {
            await _context.Entry(persona)
                .Reference(p => p.Generos)
                .LoadAsync();
        }

        public async Task LoadTipoPersonasAsync(Persona persona)
        {
            await _context.Entry(persona)
                .Reference(p => p.TipoPersonas)
                .LoadAsync();
        }

    }
}