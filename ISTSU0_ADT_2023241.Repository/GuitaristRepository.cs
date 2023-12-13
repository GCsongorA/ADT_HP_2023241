using ISTSU0_ADT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Repository
{
    public class GuitaristRepository: IGuitaristRepository
    {
        private readonly GuitarDbContext guitarDbContext;

        public GuitaristRepository(GuitarDbContext guitarDbContext)
        {
            this.guitarDbContext = guitarDbContext;
        }

        public async Task<Guitarist> CreateAsync(Guitarist guitarist)
        {
            await guitarDbContext.Guitarists.AddAsync(guitarist);
            await guitarDbContext.SaveChangesAsync();

            return guitarist;
        }

        public async Task<Guitarist?> DeleteAsync(Guid id)
        {
            var guitaristToDelete = await guitarDbContext.Guitarists.FirstOrDefaultAsync(x => x.Id == id);
            if (guitaristToDelete == null)
            {
                return null;
            }
            guitarDbContext.Guitarists.Remove(guitaristToDelete);
            await guitarDbContext.SaveChangesAsync();
            return guitaristToDelete;
        }

        public IQueryable<Guitarist> GetAll()
        {
            return guitarDbContext.Guitarists.AsQueryable().Include(x=>x.Guitars).Include(x => x.Band);
        }

        public async Task<Guitarist?> GetOneAsync(Guid id)
        {
            var result = await guitarDbContext.Guitarists.Include(x=>x.Guitars).Include(x=>x.Band).FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Guitarist?> UpdateAsync(Guid id, Guitarist guitarist)
        {
            var guitaristToUpdate = await guitarDbContext.Guitarists.FirstOrDefaultAsync(x => x.Id == id);
            if (guitaristToUpdate == null)
            {
                return null;
            }
            guitaristToUpdate.Name = guitarist.Name;
            guitaristToUpdate.Age = guitarist.Age;
            guitaristToUpdate.BandId = guitarist.BandId;
            await guitarDbContext.SaveChangesAsync();
            return guitaristToUpdate;
        }
    }
}
