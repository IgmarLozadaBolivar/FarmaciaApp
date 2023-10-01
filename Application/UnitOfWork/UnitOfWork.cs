using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbAppContext context;
        private PaisRepo _paises;
        private DepRepo _departamentos;
        private CiuRepo _ciudades;
        private PerRepo _personas;
        private GenRepo _generos;
        private TipoPerRepo _tipoPersonas;

        public UnitOfWork(DbAppContext _context)
        {
            context = _context;
        }
        
        public IPais Paises
        {
            get
            {
                if (_paises == null)
                {
                    _paises = new PaisRepo(context);
                }
                return _paises;
            }
        }

        public IDep Departamentos
        {
            get
            {
                if (_departamentos == null)
                {
                    _departamentos = new DepRepo(context);
                }
                return _departamentos;
            }
        }

        public ICiu Ciudades
        {
            get
            {
                if (_ciudades == null)
                {
                    _ciudades = new CiuRepo(context);
                }
                return _ciudades;
            }
        }

        public IPer Personas
        {
            get
            {
                if (_personas == null)
                {
                    _personas = new PerRepo(context);
                }
                return _personas;
            }
        }

        public IGen Generos
        {
            get
            {
                if (_generos == null)
                {
                    _generos = new GenRepo(context);
                }
                return _generos;
            }
        }

        public ITipoPer TipoPersonas
        {
            get
            {
                if (_tipoPersonas == null)
                {
                    _tipoPersonas = new TipoPerRepo(context);
                }
                return _tipoPersonas;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
