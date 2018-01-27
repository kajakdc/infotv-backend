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
    public class NoteController : Controller
    {

        private readonly InfoTVContext m_context;

        public NoteController(InfoTVContext context)
        {
            m_context = context;
        }

        [HttpGet]
        public IEnumerable<NoteItem> GetAll()
        {
            return m_context.NoteItems.ToList();
        }   

        [HttpGet("{ID}", Name = "GetNote")]
        public IActionResult GetByID (long ID)
        {
            var item = m_context.NoteItems.FirstOrDefault(t => t.ID == ID);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }  

        [HttpPost]
        public IActionResult Create ([FromBody] NoteItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            
            m_context.NoteItems.Add(item);
            m_context.SaveChanges();

            return CreatedAtRoute("GetNote", new { ID = item.ID }, item);
        }

        [HttpPut(template: "{ID}")]
        public IActionResult Update (long ID, [FromBody] NoteItem item)
        {
            if (item == null || item.ID != ID)
            {
                return BadRequest();
            }

            var note = m_context.NoteItems.FirstOrDefault(t => t.ID == ID);
            if (note == null)
            {
                return NotFound();
            }

            note.Data = item.Data;
            note.Priority = item.Priority;
            note.Active = item.Active;

            m_context.NoteItems.Update(note);
            m_context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{ID}")]
        public IActionResult Delete (long ID)
        {
            var note = m_context.NoteItems.FirstOrDefault(t => t.ID == ID);
            if (note == null)
            {
                return NotFound();
            }

            m_context.NoteItems.Remove(note);
            m_context.SaveChanges();
            return new NoContentResult();
        }
    
        [Route("/all")]
        [HttpDelete]
        public IActionResult Delete ()
        {
            int count = (int)m_context.NoteItems.Count();

            if (count == 0)
            {
                return new NoContentResult();
            }

            m_context.NoteItems.RemoveRange(m_context.NoteItems);
            m_context.SaveChanges();
            return new NoContentResult();
        }

        [Route("/all")]
        [HttpPut]
        public IActionResult UpdateAll ([FromBody] int Active)
        {
            if (Active != 0 || Active != 1)
            {
                return BadRequest();
            }

            // Query fo all notes with active different than given
            var notes = from n in m_context.NoteItems where n.Active != Active select n;

            foreach (var n in notes)
            {
                n.Active = Active;
                m_context.NoteItems.Update(n);
            }
            
            m_context.SaveChanges();
            return new NoContentResult();
        }
    }
}
