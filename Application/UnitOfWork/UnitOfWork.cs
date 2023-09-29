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
