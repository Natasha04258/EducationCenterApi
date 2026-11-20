using Microsoft.AspNetCore.Mvc;
using EducationCenterApi.Data;
using EducationCenterApi.Models;
using Microsoft.AspNetCore.Mvc;
using EducationCenterApi.Data;
using EducationCenterApi.Models;

namespace EducationCenterApi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class OplatasController : ControllerBase
    {
        private readonly IOplataRepository _repository;

        public OplatasController(IOplataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Oplata>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Oplata>>> GetAll()
        {
            await Task.Delay(3000);
            return Ok(await _repository.GetAllAsync());
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Oplata), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Oplata>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Oplata), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Oplata>> Create(CreateOplataRequest request)
        {
            var oplata = new Oplata
            {
                RoditelId = request.RoditelId,
                RebenokId = request.RebenokId,
                KolichestvoZanyatiy = request.KolichestvoZanyatiy,
                Summa = request.Summa
            };

            var created = await _repository.AddAsync(oplata);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            var oplata = await _repository.GetByIdAsync(id);
            if (oplata == null) return NotFound();

            if (oplata.Status == "Оплачено")
                return Conflict("Нельзя удалить уже оплаченный платёж");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
