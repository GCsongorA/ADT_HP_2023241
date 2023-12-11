using ISTSU0_ADT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Repository
{
    public interface IBandRepository
    {
        public IQueryable<Band> GetAll();
        public Task<Band?> GetOne(Guid id);
        public Task<Band?> Update(Guid id, Band band);
        public Task<Band?> Delete(Guid id);
        public Task<Band> Create(Band band);
    }
}
