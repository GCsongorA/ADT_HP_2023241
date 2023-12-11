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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await bandLogic.GetAll().ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOne/{id:Guid}")]
        public async Task<IActionResult> GetOneAsync([FromRoute] Guid id)
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
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = bandLogic.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] Band band)
        {
            var result = await bandLogic.CreateAsync(band);
            return CreatedAtAction(nameof(GetOneAsync), band.Id, band);
        }

        [HttpPut]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] Band band)
        {
            var result = await bandLogic.UpdateAsync(id, band);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
