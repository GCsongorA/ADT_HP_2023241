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
        private readonly IBandRepository bandRepository;
        private readonly IGuitaristRepository guitaristRepository;

        public BandLogic(IBandRepository bandRepository, IGuitaristRepository guitaristRepository)
        {
            this.bandRepository = bandRepository;
            this.guitaristRepository = guitaristRepository;
        }

        public async Task<bool> DoesThisBandHaveMultipleGuitarists(Guid bandId)
        {
            var guitarists = guitaristRepository.GetAll();
            var band = await bandRepository.GetOneAsync(bandId);
            return guitarists.Where(x => x.BandId == band.Id).Count() > 1;
        }
        public async Task<List<string>> WhatGuitarsDoesThisBandHave(Guid id)
        {
            var band = await bandRepository.GetOneAsync(id);
            var guitarTypes = band.Guitarists.SelectMany(x => x.Guitars.Select(x => x.BodyType.ToString()));
            return guitarTypes.ToList();
        }

        #region CRUD
        public async Task<Band> CreateAsync(Band band)
        {
            if (band.Name == null)
            {
                throw new ArgumentException();
            }
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
