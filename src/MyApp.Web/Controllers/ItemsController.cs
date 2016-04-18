using System.Linq;
using Microsoft.AspNet.Mvc;
using MyApp.Web.Models;
using Microsoft.Data.Entity;
using System.Threading.Tasks;

namespace MyApp.Web.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        public ApplicationDbContext Db { get; private set; }

        public ItemsController(ApplicationDbContext db)
        {
            Db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Json(Db.Items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Db.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            var entity = Db.Items.Add(item);
            await Db.SaveChangesAsync();
            return CreatedAtRoute(new { controller = "Items", id = entity.Entity.Id }, entity.Entity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = Db.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Db.Items.Remove(item);
                return Ok();
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}