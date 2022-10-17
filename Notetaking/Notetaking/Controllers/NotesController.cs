using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notetaking.Data;
using Notetaking.Models.DomainModels;
using Notetaking.Models.DTO;
using System.Reflection;

namespace Notetaking.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext dbContext;

        public NotesController(NotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            //A method to get all existing notes created before and stored in database

            var notes = dbContext.Notes.ToList();

            var notesDTO = new List<Models.DTO.Note>(); 

            foreach (var note in notes)
            {
                notesDTO.Add(new Models.DTO.Note
                {
                    Id = note.Id,
                    Heading = note.Heading,
                    Text = note.Text,
                    DateCreated = note.DateCreated
                });
            }
            //If we find the notes we sent the message 200 success
            return Ok(notesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetNoteById(Guid id)
        {
            //A method to get notes based on their id
            var noteDomainObject = dbContext.Notes.Find(id);
            if (noteDomainObject != null)
            {
                var noteDTO = new Models.DTO.Note
                {
                    Id = noteDomainObject.Id,
                    Heading = noteDomainObject.Heading,
                    Text = noteDomainObject.Text,
                    DateCreated = noteDomainObject.DateCreated
                };
                //If we find the requested note we send the message 200 success and the requested note
                return Ok(noteDTO);
            }
            return BadRequest();
        }


        [HttpPost]
        public IActionResult AddNote(AddNoteRequest addNoteRequest)
        {
            //A method to add a new note
            var note = new Models.DomainModels.Note
            {
                Heading = addNoteRequest.Heading,
                Text = addNoteRequest.Text,
                DateCreated = DateTime.Now
            };

            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
            //If the note is added with success we return a 200 success
            return Ok(note);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateNote(Guid id, UpdateNoteRequest updateNoteRequest)
        {   //A method to update the existing notes.
            var existingNote = dbContext.Notes.Find(id);

            if (existingNote != null)
            {
                existingNote.Heading = updateNoteRequest.Heading;
                existingNote.Text = updateNoteRequest.Text;

                dbContext.SaveChanges();
                //If we found the note we update it and post it in database
                return Ok(existingNote);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteNote(Guid id)
        {   //A method to delete notes
            var existingNote = dbContext.Notes.Find(id);

            if(existingNote != null)
            {
                dbContext.Notes.Remove(existingNote);
                dbContext.SaveChanges();
                //if we find the note than me delete it and return a 200 success message
                return Ok();
            }

            return BadRequest();
        }
    }
}
