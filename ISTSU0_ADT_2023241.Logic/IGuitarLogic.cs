using ISTSU0_ADT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Logic
{
    public interface IGuitarLogic
    {
        public IQueryable<Guitar> GetAll();
        public Task<Guitar?> GetOneAsync(Guid id);
        public Task<Guitar?> UpdateAsync(Guid id, Guitar guitar);
        public Task<Guitar?> DeleteAsync(Guid id);
        public Task<Guitar> CreateAsync(Guitar guitar);
    }
}
