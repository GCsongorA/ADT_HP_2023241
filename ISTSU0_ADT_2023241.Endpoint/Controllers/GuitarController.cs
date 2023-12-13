using ISTSU0_ADT_2023241.Endpoint.Dtos;
using ISTSU0_ADT_2023241.Logic;
using ISTSU0_ADT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTSU0_ADT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuitarController : ControllerBase
    {
        private readonly IGuitarLogic guitarLogic;
        private readonly ILogger<GuitarController> logger;

        public GuitarController(IGuitarLogic guitarLogic, ILogger<GuitarController> logger)
        {
            this.guitarLogic = guitarLogic;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await guitarLogic.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOne/{id:Guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var result = await guitarLogic.GetOneAsync(id);

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
            var result = await guitarLogic.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateGuitarDto createGuitarDto)
        {
            Guitar guitarDomainModel = new()
            {
                Model = createGuitarDto.Model,
                Brand = createGuitarDto.Brand,
                GuitaristId = createGuitarDto.GuitaristId,
                BodyType = createGuitarDto.BodyType
            };

            guitarDomainModel = await guitarLogic.CreateAsync(guitarDomainModel);
            return CreatedAtAction(nameof(GetOne), new { guitarDomainModel.Id }, guitarDomainModel);
        }

        [HttpPut]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGuitarDto updateGuitarDto)
        {
            Guitar guitar = new()
            {
                GuitaristId = updateGuitarDto.GuitaristId
            };

            var result = await guitarLogic.UpdateAsync(id, guitar);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
