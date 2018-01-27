using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using infotv.Models;

namespace infotv.Controllers
{
    [Route("/[controller]")]
    public class LunchController : Controller
    {

        private readonly InfoTVContext m_context;

        public LunchController(InfoTVContext context)
        {
            m_context = context;
        }


        [HttpGet]
        public IEnumerable<LunchItem> GetAll()
        {
            return m_context.LunchItems.ToList();
        }   

        [HttpGet("{ID}", Name = "GetLunch")]
        public IActionResult GetByID (long ID)
        {
            var item = m_context.LunchItems.FirstOrDefault(t => t.ID == ID);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }  

        [HttpPost]
        public IActionResult Create ([FromBody] LunchItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            
            m_context.LunchItems.Add(item);
            m_context.SaveChanges();

            return CreatedAtRoute("GetLunch", new { ID = item.ID }, item);
        }

        [HttpPut(template: "{ID}")]
        public IActionResult Update (long ID, [FromBody] LunchItem item)
        {
            if (item == null || item.ID != ID)
            {
                return BadRequest();
            }

            var luch = m_context.LunchItems.FirstOrDefault(t => t.ID == ID);
            if (luch == null)
            {
                return NotFound();
            }

            luch.CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
            luch.Food = item.Food;

            m_context.LunchItems.Update(luch);
            m_context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{ID}")]
        public IActionResult Delete (long ID)
        {
            var luch = m_context.LunchItems.FirstOrDefault(t => t.ID == ID);
            if (luch == null)
            {
                return NotFound();
            }

            m_context.LunchItems.Remove(luch);
            m_context.SaveChanges();
            return new NoContentResult();
        }
    }
}
