﻿using Microsoft.AspNetCore.Authorization;
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
            // check if user is logged in

            // check for the token in the reuest

            var notes = dbContext.Notes.ToList();

            var notesDTO = new List<Models.DTO.Note>(); 

            foreach (var note in notes)
            {
                notesDTO.Add(new Models.DTO.Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    DateCreated = note.DateCreated
                });
            }

            return Ok(notesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetNoteById(Guid id)
        {
            var noteDomainObject = dbContext.Notes.Find(id);
            if (noteDomainObject != null)
            {
                var noteDTO = new Models.DTO.Note
                {
                    Id = noteDomainObject.Id,
                    Title = noteDomainObject.Title,
                    Description = noteDomainObject.Description,
                    DateCreated = noteDomainObject.DateCreated
                };

                return Ok(noteDTO);
            }
            return BadRequest();
        }


        [HttpPost]
        public IActionResult AddNote(AddNoteRequest addNoteRequest)
        {
            //Convert the DTO to Domain Model
            var note = new Models.DomainModels.Note
            {
                Title = addNoteRequest.Title,
                Description = addNoteRequest.Description,
                DateCreated = DateTime.Now
            };

            dbContext.Notes.Add(note);
            dbContext.SaveChanges();

            return Ok(note);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateNote(Guid id, UpdateNoteRequest updateNoteRequest)
        {
            var existingNote = dbContext.Notes.Find(id);

            if (existingNote != null)
            {
                existingNote.Title = updateNoteRequest.Title;
                existingNote.Description = updateNoteRequest.Description;

                dbContext.SaveChanges();

                return Ok(existingNote);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteNote(Guid id)
        {
            var existingNote = dbContext.Notes.Find(id);

            if(existingNote != null)
            {
                dbContext.Notes.Remove(existingNote);
                dbContext.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
