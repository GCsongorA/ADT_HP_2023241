using ISTSU0_ADT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTSU0_ADT_2023241.Logic
{
    public interface IBandLogic
    {
        public Task<bool> DoesThisBandHaveMultipleGuitarists(Guid bandId);
        public Task<List<string>> WhatGuitarsDoesThisBandHave(Guid id);
        public IQueryable<Band> GetAll();
        public Task<Band?> GetOneAsync(Guid id);
        public Task<Band?> UpdateAsync(Guid id, Band band);
        public Task<Band?> DeleteAsync(Guid id);
        public Task<Band> CreateAsync(Band band);
    }
}
