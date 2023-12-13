using ISTSU0_ADT_2023241.Endpoint.Dtos;
using ISTSU0_ADT_2023241.Logic;
using ISTSU0_ADT_2023241.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTSU0_ADT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly IBandLogic bandLogic;
        private readonly ILogger<BandController> logger;

        public BandController(IBandLogic bandLogic, ILogger<BandController> logger)
        {
            this.bandLogic = bandLogic;
            this.logger = logger;
        }

        [HttpGet]
        [Route("DoesThisBandHaveMultipleGuitarists/{bandId:Guid}")]
        public async Task<IActionResult> DoesThisBandHaveMultipleGuitarists([FromRoute] Guid bandId)
        {
            var result = await bandLogic.DoesThisBandHaveMultipleGuitarists(bandId);
            return Ok(result);
        }

        [HttpGet]
        [Route("WhatGuitarsDoesThisBandHave/{id:Guid}")]
        public async Task<IActionResult> WhatGuitarsDoesThisBandHave([FromRoute] Guid id)
        {
            var result = await bandLogic.WhatGuitarsDoesThisBandHave(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await bandLogic.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOne/{id:Guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var result = await bandLogic.GetOneAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await bandLogic.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateBandDto createBandDto)
        {
            Band bandDomainModel = new()
            {
                Name = createBandDto.Name,
                Genre = createBandDto.Genre
            };

            bandDomainModel = await bandLogic.CreateAsync(bandDomainModel);
            return CreatedAtAction(nameof(GetOne), new { bandDomainModel.Id }, bandDomainModel);
        }

        [HttpPut]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBandDto updateBandDto)
        {
            Band band = new()
            {
                Name = updateBandDto.Name,
                Genre = updateBandDto.Genre
            };

            var result = await bandLogic.UpdateAsync(id, band);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
