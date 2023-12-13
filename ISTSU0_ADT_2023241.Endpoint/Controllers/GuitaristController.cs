using ISTSU0_ADT_2023241.Endpoint.Dtos;
using ISTSU0_ADT_2023241.Logic;
using ISTSU0_ADT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTSU0_ADT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuitaristController : ControllerBase
    {
        private readonly IGuitaristLogic guitaristLogic;
        private readonly ILogger<GuitaristController> logger;

        public GuitaristController(IGuitaristLogic guitaristLogic, ILogger<GuitaristController> logger)
        {
            this.guitaristLogic = guitaristLogic;
            this.logger = logger;
        }

        [HttpGet]
        [Route("WhereDoesThisGuitaristPlay/{id:Guid}")]
        public async Task<IActionResult> WhereDoesThisGuitaristPlay([FromRoute] Guid id)
        {
            var result = await guitaristLogic.WhereDoesThisGuitaristPlay(id);
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await guitaristLogic.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOne/{id:Guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var result = await guitaristLogic.GetOneAsync(id);

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
            var result = await guitaristLogic.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateGuitaristDto createGuitaristDto)
        {
            Guitarist guitaristDomainModel = new()
            {
                Name = createGuitaristDto.Name,
                Age = createGuitaristDto.Age,
                BandId = createGuitaristDto.BandId
            };

            guitaristDomainModel = await guitaristLogic.CreateAsync(guitaristDomainModel);
            return CreatedAtAction(nameof(GetOne), new { guitaristDomainModel.Id }, guitaristDomainModel);
        }

        [HttpPut]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGuitaristDto updateGuitaristDto)
        {
            Guitarist guitarist = new()
            {
                BandId = updateGuitaristDto.BandId,
                Age = updateGuitaristDto.Age
            };

            var result = await guitaristLogic.UpdateAsync(id, guitarist);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
