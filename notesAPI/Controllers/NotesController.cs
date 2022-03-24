using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notesAPI.Models;
using notesAPI.Services;
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

        INoteCollectionService _noteCollectionService;


        public NotesController(INoteCollectionService noteCollectionService) {
            _noteCollectionService = noteCollectionService ?? throw new ArgumentNullException(nameof(noteCollectionService));
        }

        /// <summary>
        ///  Get all notes
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public async Task<IActionResult> GetNotes()
        {
            List<Note> notes = await _noteCollectionService.GetAll();
            return Ok(notes);
        }

        /*public IActionResult GetNotes()
        {
            return Ok(_noteCollectionService.GetAll());
           
        }*/
        /// <summary>
        /// Creates a new note
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note should not be null");
            }
           await _noteCollectionService.Create(note);
            return Ok(await _noteCollectionService.GetAll());
        }

       /* public IActionResult CreateNote([FromBody]Note note)
        {
            
            if (note == null)
            {
                return BadRequest("Note should not be null");
            }
            _noteCollectionService.Create(note);
           *//* return Ok(_notes);*//*
            return CreatedAtRoute("GetNote", new { id = note.Id.ToString() }, note);
        }*/


        /// <summary>
        /// Updates the note with a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note can't be null");
            }
            if (!await _noteCollectionService.Update(id, note))
            {
                return NotFound("This note doesn't exist");
            }
            return Ok(await _noteCollectionService.Get(id));
        }

       /* public IActionResult UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note can't be null");
            }

            _noteCollectionService.Update(id, note);
            return Ok(_noteCollectionService.Get(id));

            */
        /*int index = _notes.FindIndex(n => n.Id == id);
        if (index == -1)
        {

            return CreateNote(note);

        }*/
        /* note.Id = id;
         _notes[index] = note;*/
        /*return Ok(_notes);*/


        /*}*/

        /// <summary>
        /// Deletes the note with that certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            if (await _noteCollectionService.Delete(id))
            {
                return Ok("note deleted");
            }
            else
            {

                return NotFound("this note doesn't exist");
            }
        }
        /*public IActionResult DeleteNote(Guid id)
        {
            if (_noteCollectionService.Delete(id)){
                return Ok();
            }
            else {
                
                return NotFound("this note doesn't exist");
            }
        }*/

        /// <summary>
        /// Get all notes of a certain owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("owner/{ownerId}")]
        public IActionResult GetNotesByOwner(Guid ownerId)
        {
            return Ok(_noteCollectionService.GetNotesByOwnerId(ownerId));
        }

        /// <summary>
        /// Get all notes with a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetNote")]
        public async Task<IActionResult> GetNotesID(Guid id)
        {
            return Ok(await _noteCollectionService.Get(id));
        }

        /*public IActionResult GetNotesID(Guid id)
        {
            return Ok(_noteCollectionService.Get(id));

        }*/

        /*
                /// <summary>
                ///  Update the note with a certain ID and with a certain OwnerID
                /// </summary>
                /// <param name="NoteId"></param>
                /// <param name="OwnerId"></param>
                /// <param name="note"></param>
                /// <returns></returns>
                [HttpPut("{OwnerId}/{NoteId}")]
                public IActionResult UpdateNoteByIdAndOwnerID(Guid NoteId, Guid OwnerId, [FromBody] Note note)
                {
                    if (note == null)
                    {
                        return BadRequest("Note can't be null");
                    }
                    int index = _notes.FindIndex(n => n.Id == NoteId & n.OwnerId == OwnerId);
                    if (index == -1)
                    {
                        return NotFound("Note not found");

                    }
                    note.Id = NoteId;
                    note.OwnerId = OwnerId;
                    _notes[index] = note;
                    return Ok(_notes);
                }

                /// <summary>
                ///  Delete the note with a certain ID and with a certain OwnerID
                /// </summary>
                /// <param name="NoteId"></param>
                /// <param name="OwnerId"></param>
                /// <returns></returns>
                [HttpDelete("{OwnerId}/{NoteId}")]
                public IActionResult DeleteNoteByIdAndOwnerID(Guid NoteId, Guid OwnerId)
                {
                    int index = _notes.FindIndex(n => n.Id == NoteId && n.OwnerId == OwnerId);
                    if (index == -1)
                    {
                        return NotFound("Note not found");
                    }
                    _notes.RemoveAt(index);
                    return Ok(_notes);
                }

                /// <summary>
                ///  Delete all notes with a certain OwnerID
                /// </summary>
                /// <param name="OwnerId"></param>
                /// <returns></returns>
                [HttpDelete("{OwnerId}")]
                public IActionResult DeleteAllNotesByOwnerID(Guid OwnerId)
                {
                    if (_notes.RemoveAll(n => n.OwnerId == OwnerId) == 0)
                    {
                        return NotFound("This Owner doesn't have any note");
                    }
                    return Ok(_notes);
                }*/

    }
}
