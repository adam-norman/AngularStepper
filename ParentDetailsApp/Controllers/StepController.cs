using App.DataAccess.Data.Repository.IRepository;
using App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParentDetailsApp.ViewModel;

namespace ParentDetailsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StepController : ControllerBase
    {
        private readonly ILogger<StepController> _logger;
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public StepController(ILogger<StepController> logger, IUnitOfWork db, IMapper mapper)
        {
            _logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var step = db.Steps.GetFirstOrDefault(s => s.Id == id);
            if (step != null)
            {
                return Ok(mapper.Map<StepVM>(step.Result));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var steps = db.Steps.GetAll();

            if (steps != null)
            {
                return Ok(mapper.Map<StepVM[]>(steps.Result));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult PostStep(StepVM stepVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (stepVM.Id == null)
            {
                db.Steps.Add(mapper.Map<Step>(stepVM));
            }
            else
            {
                db.Steps.Update(mapper.Map<Step>(stepVM));
            }
            db.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteStep(int id)
        {
            var stepFromDb = db.Steps.GetFirstOrDefault(s => s.Id == id);
            if (stepFromDb == null)
            {
                return NotFound();
            }
            db.Steps.Remove(stepFromDb.Result);
            db.Save();
            return NoContent();
        }
    }
}