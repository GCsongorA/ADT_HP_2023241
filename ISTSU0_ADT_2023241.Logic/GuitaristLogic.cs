using ISTSU0_ADT_2023241.Models;
using ISTSU0_ADT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Logic
{
    public class GuitaristLogic : IGuitaristLogic
    {
        private readonly IGuitaristRepository guitaristRepository;

        public GuitaristLogic(IGuitaristRepository guitaristRepository)
        {
            this.guitaristRepository = guitaristRepository;
        }


        #region CRUD
        public async Task<Guitarist> CreateAsync(Guitarist guitarist)
        {
            if (guitarist.Name == null)
            {
                throw new ArgumentException();
            }
            return await guitaristRepository.CreateAsync(guitarist);
        }

        public async Task<Guitarist?> DeleteAsync(Guid id)
        {
            return await guitaristRepository.DeleteAsync(id);
        }

        public IQueryable<Guitarist> GetAll()
        {
            return guitaristRepository.GetAll();
        }

        public async Task<Guitarist?> GetOneAsync(Guid id)
        {
            return await guitaristRepository.GetOneAsync(id);
        }

        public async Task<Guitarist?> UpdateAsync(Guid id, Guitarist guitarist)
        {
            return await guitaristRepository.UpdateAsync(id, guitarist);
        }

        public async Task<Band?> WhereDoesThisGuitaristPlay(Guid id)
        {
            var guitarist = await guitaristRepository.GetOneAsync(id);
            return guitarist?.Band;
        }



        #endregion
    }
}
