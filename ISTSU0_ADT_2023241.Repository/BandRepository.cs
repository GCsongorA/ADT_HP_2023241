using ISTSU0_ADT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Repository
{
    public class BandRepository : IBandRepository
    {
        private readonly GuitarDbContext guitarDbContext;

        public BandRepository(GuitarDbContext guitarDbContext)
        {
            this.guitarDbContext = guitarDbContext;
        }

        public async Task<Band> CreateAsync(Band band)
        {
            await guitarDbContext.Bands.AddAsync(band);
            await guitarDbContext.SaveChangesAsync();

            return band;
        }

        public async Task<Band?> DeleteAsync(Guid id)
        {
            var bandToDelete = await guitarDbContext.Bands.FirstOrDefaultAsync(x=>x.Id== id);
            if (bandToDelete==null)
            {
                return null;
            }
            guitarDbContext.Bands.Remove(bandToDelete);
            await guitarDbContext.SaveChangesAsync();
            return bandToDelete;
        }

        public IQueryable<Band> GetAll()
        {
            return guitarDbContext.Bands.AsQueryable().Include(x => x.Guitarists).ThenInclude(x=>x.Guitars);
        }

        public async Task<Band?> GetOneAsync(Guid id)
        {
            var result = await guitarDbContext.Bands.Include(x=>x.Guitarists).ThenInclude(x=>x.Guitars).FirstOrDefaultAsync(x => x.Id == id);
            if (result==null)
            {
                return null;
            }
            return result;
        }

        public async Task<Band?> UpdateAsync(Guid id, Band band)
        {
            var bandToUpdate = await guitarDbContext.Bands.FirstOrDefaultAsync(x => x.Id == id);
            if (bandToUpdate==null)
            {
                return null;
            }
            bandToUpdate.Name = band.Name;
            bandToUpdate.Genre = band.Genre;
            await guitarDbContext.SaveChangesAsync();
            return bandToUpdate;
        }
    }
}
