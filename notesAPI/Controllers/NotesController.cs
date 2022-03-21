using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Controllers
{
    [Route("[controller]")]
     [ApiController]
    public class NotesController : ControllerBase
    {

        private static List<Note> _notes = new List<Note> { new Note { Id = new Guid("00000000-0000-0000-0000-000000000001"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "First Note", Description = "First Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000002"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Second Note", Description = "Second Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000003"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Third Note", Description = "Third Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000004"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000005"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fifth Note", Description = "Fifth Note Description" }
        };


        public NotesController() { }

        /// <summary>
        ///  Get all notes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }
        [HttpPost]
        public IActionResult CreateNote([FromBody]Note note)
        {
            _notes.Add(note);
            if (note == null)
            {
                return BadRequest("Note cannot be null");
            }
            return Ok(_notes);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id ,[FromBody] Note note)
        {
            if(note== null)
            {
                return BadRequest("Note can't be null");
            }
            int index = _notes.FindIndex(n => n.Id == id);
            if (index == -1)
            {///daca u=nu exista notita pt editat o creeaza 
                return CreateNote(note);
               
            }
            note.Id = id;
            _notes[index] = note;
            return Ok(_notes);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            int index = _notes.FindIndex(n => n.Id == id);
            if(index == -1)
            {
                return NotFound("this note doesn't exist");
            }
            else
            {
                //_notes = _notes.Where(note => note.Id != id).ToList();  asta functioneaza fara List
               // sau
                _notes.RemoveAt(index);
                return Ok(_notes);
            }
        }



    }
}
