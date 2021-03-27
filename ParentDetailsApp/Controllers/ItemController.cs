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
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public ItemController(ILogger<ItemController> logger, IUnitOfWork db, IMapper mapper)
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
            var item = db.Items.GetFirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                return Ok(item);
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
            var items = db.Items.GetAll();
            if (items != null)
            {
                return Ok(items.Result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult PostItem(ItemVM itemVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (itemVM.Id == null)
            {
                db.Items.Add(mapper.Map<Item>(itemVM));
            }
            else
            {
                db.Items.Update(mapper.Map<Item>(itemVM));
            }
            db.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteItem(int id)
        {
            var itemFromDb = db.Items.GetFirstOrDefault(s => s.Id == id);
            if (itemFromDb == null)
            {
                return NotFound();
            }
            db.Items.Remove(itemFromDb.Result);
            db.Save();
            return NoContent();
        }
    }
}