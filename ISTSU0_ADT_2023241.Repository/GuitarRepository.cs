using ISTSU0_ADT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Repository
{
    public class GuitarRepository : IGuitarRepository
    {
        private readonly GuitarDbContext guitarDbContext;

        public GuitarRepository(GuitarDbContext guitarDbContext)
        {
            this.guitarDbContext = guitarDbContext;
        }

        public async Task<Guitar> CreateAsync(Guitar guitar)
        {
            await guitarDbContext.Guitars.AddAsync(guitar);
            await guitarDbContext.SaveChangesAsync();

            return guitar;
        }

        public async Task<Guitar?> DeleteAsync(Guid id)
        {
            var guitarToDelete = await guitarDbContext.Guitars.FirstOrDefaultAsync(x => x.Id == id);
            if (guitarToDelete == null)
            {
                return null;
            }
            guitarDbContext.Guitars.Remove(guitarToDelete);
            await guitarDbContext.SaveChangesAsync();
            return guitarToDelete;
        }

        public IQueryable<Guitar> GetAll()
        {
            return guitarDbContext.Guitars.AsQueryable();
        }

        public async Task<Guitar?> GetOneAsync(Guid id)
        {
            var result = await guitarDbContext.Guitars.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Guitar?> UpdateAsync(Guid id, Guitar guitar)
        {
            var guitarToUpdate = await guitarDbContext.Guitars.FirstOrDefaultAsync(x => x.Id == id);
            if (guitarToUpdate == null)
            {
                return null;
            }
            guitarToUpdate.GuitaristId = guitar.GuitaristId;
            await guitarDbContext.SaveChangesAsync();
            return guitarToUpdate;
        }
    }
}
