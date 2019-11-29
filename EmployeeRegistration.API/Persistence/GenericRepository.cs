using EmployeeRegistration.API.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRegistration.API.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RegistrationDbContext _context;
        private DbSet<T> table = null;

        public GenericRepository(RegistrationDbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public async void Add(T entity)
        { 
            table.Add(entity); 
        }

        public async void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async void Remove(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public async void Save()
        {
            _context.SaveChanges();
        } 
    }
}
