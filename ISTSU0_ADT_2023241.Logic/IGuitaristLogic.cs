using ISTSU0_ADT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Logic
{
    public interface IGuitaristLogic
    {
        public Task<Band?> WhereDoesThisGuitaristPlay(Guid id);
        public IQueryable<Guitarist> GetAll();
        public Task<Guitarist?> GetOneAsync(Guid id);
        public Task<Guitarist?> UpdateAsync(Guid id, Guitarist guitarist);
        public Task<Guitarist?> DeleteAsync(Guid id);
        public Task<Guitarist> CreateAsync(Guitarist guitarist);
    }
}
