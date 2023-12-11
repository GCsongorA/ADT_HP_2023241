using ISTSU0_ADT_2023241.Models;
using ISTSU0_ADT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Logic
{
    public class BandLogic : IBandLogic
    {
        private readonly BandRepository bandRepository;

        public BandLogic(BandRepository bandRepository)
        {
            this.bandRepository = bandRepository;
        }


        #region CRUD
        public async Task<Band> CreateAsync(Band band)
        {
            return await bandRepository.CreateAsync(band);
        }

        public async Task<Band?> DeleteAsync(Guid id)
        {
            return await bandRepository.DeleteAsync(id);
        }

        public IQueryable<Band> GetAll()
        {
            return bandRepository.GetAll();
        }

        public async Task<Band?> GetOneAsync(Guid id)
        {
            return await bandRepository.GetOneAsync(id);
        }

        public async Task<Band?> UpdateAsync(Guid id, Band band)
        {
            return await bandRepository.UpdateAsync(id, band);
        }



        #endregion
    }
}
