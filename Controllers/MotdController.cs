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
    public class MotdController : Controller
    {

        private readonly InfoTVContext m_context;

        public MotdController(InfoTVContext context)
        {
            m_context = context;
        }

        [HttpGet]
        public IEnumerable<MotdItem> GetAll()
        {
            return m_context.MotdItems.ToList();
        }   

        [HttpGet("{ID}", Name = "GetTodo")]
        public IActionResult GetByID (long ID)
        {
            var item = m_context.MotdItems.FirstOrDefault(t => t.ID == ID);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }  

        [HttpPost]
        public IActionResult Create ([FromBody] MotdItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            
            m_context.MotdItems.Add(item);
            m_context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { ID = item.ID }, item);
        }

        [HttpPut(template: "{ID}")]
        public IActionResult Update (long ID, [FromBody] MotdItem item)
        {
            if (item == null || item.ID != ID)
            {
                return BadRequest();
            }

            var motd = m_context.MotdItems.FirstOrDefault(t => t.ID == ID);
            if (motd == null)
            {
                return NotFound();
            }

            motd.Data = item.Data;
            motd.Type = item.Type;

            m_context.MotdItems.Update(motd);
            m_context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{ID}")]
        public IActionResult Delete (long ID)
        {
            var motd = m_context.MotdItems.FirstOrDefault(t => t.ID == ID);
            if (motd == null)
            {
                return NotFound();
            }

            m_context.MotdItems.Remove(motd);
            m_context.SaveChanges();
            return new NoContentResult();
        }
    }
}
