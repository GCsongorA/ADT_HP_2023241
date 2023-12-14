using ISTSU0_ADT_2023241.Models;
using ISTSU0_ADT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Logic
{
    public class GuitarLogic: IGuitarLogic
    {
        private readonly IGuitarRepository guitarRepository;

        public GuitarLogic(IGuitarRepository guitarRepository)
        {
            this.guitarRepository = guitarRepository;
        }


        #region CRUD
        public async Task<Guitar> CreateAsync(Guitar guitar)
        {
            if (guitar.Brand==null||guitar.Model==null)
            {
                throw new ArgumentException();
            }
            return await guitarRepository.CreateAsync(guitar);
        }

        public async Task<Guitar?> DeleteAsync(Guid id)
        {
            return await guitarRepository.DeleteAsync(id);
        }

        public IQueryable<Guitar> GetAll()
        {
            return guitarRepository.GetAll();
        }

        public async Task<Guitar?> GetOneAsync(Guid id)
        {
            return await guitarRepository.GetOneAsync(id);
        }

        public async Task<Guitar?> UpdateAsync(Guid id, Guitar guitar)
        {
            return await guitarRepository.UpdateAsync(id, guitar);
        }



        #endregion
    }
}
