using MongoDB.Driver;
using notesAPI.Models;
using notesAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Services
{
    public class NoteCollectionService : INoteCollectionService
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteCollectionService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _notes = database.GetCollection<Note>(settings.NoteCollectionName);
        }

        /* nu mai avem nevoie cand folosim MongoDB*/
        /* private static List<Note> _notes = new List<Note> { new Note { Id = new Guid("00000000-0000-0000-0000-000000000001"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "First Note", Description = "First Note Description" },
         new Note { Id = new Guid("00000000-0000-0000-0000-000000000002"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Second Note", Description = "Second Note Description" },
         new Note { Id = new Guid("00000000-0000-0000-0000-000000000003"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Third Note", Description = "Third Note Description" },
         new Note { Id = new Guid("00000000-0000-0000-0000-000000000004"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fourth Note", Description = "Fourth Note Description" },
         new Note { Id = new Guid("00000000-0000-0000-0000-000000000005"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000002"), Title = "Fifth Note", Description = "Fifth Note Description" }
         };*/
        public NoteCollectionService()
        {
        }
        /*public bool Create(Note note)
        {
            _notes.Add(note);
            return true;
        }*/

        public async Task<bool> Create(Note note)
        {
            if (note.Id == Guid.Empty)
            {
                note.Id = Guid.NewGuid();
            }

            await _notes.InsertOneAsync(note);
            return true;
        }


        /*   public bool Delete(Guid id)
           {
               int index = _notes.FindIndex(n => n.Id == id);
               if (index == -1)
               {
                   return false;
               }
               else
               {
                   //_notes = _notes.Where(note => note.Id != id).ToList();  asta functioneaza fara List
                   // sau
                   _notes.RemoveAt(index);
                   return true;
               }
           }*/
        public async Task<bool> Delete(Guid id)
        {
            var result = await _notes.DeleteOneAsync(note => note.Id == id);
            if (result.IsAcknowledged && result.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }


        //public note get(guid id)
        //{

        //    note note = _notes.find(n => n.id == id);
        //    return note;
        //}

        public async Task<Note> Get(Guid id)
        {
            return (await _notes.FindAsync(note => note.Id == id)).FirstOrDefault();
        }


        /* public List<Note> GetAll()
         {
             return _notes;
         }*/

        public async Task<List<Note>> GetAll()
        {
            var result = await _notes.FindAsync(note => true);
            return result.ToList();
        }

        /*   public List<Note> GetNotesByOwnerId(Guid ownerId)
       {
           int index = _notes.FindIndex((note => note.OwnerId == ownerId));
           _notes.Find(n => n.OwnerId == ownerId);
           int index = _notes.FindIndex((note => note.Id == ownerId));
           _notes.Where(n => n.OwnerId == ownerId);
           return _notes.FindAll(note => note.OwnerId == ownerId);
       }*/

        public async Task<List<Note>> GetNotesByOwnerId(Guid ownerId)
        {
            return (await _notes.FindAsync(note => note.OwnerId == ownerId)).ToList();
        }


        /* public bool Update(Guid id, Note note)
         {
             if (note == null)
             {
                 return false;
             }
             int index = _notes.FindIndex(n => n.Id == id);
             if (index == -1)
             {
                 Create(note);
                 return false;

             }
             note.Id = id;
             _notes[index] = note;
             GetAll();
             return true;


         }*/
        public async Task<bool> Update(Guid id, Note noteNew)
        {
            noteNew.Id = id;
            var result = await _notes.ReplaceOneAsync(note => note.Id == id, noteNew);
            if (!result.IsAcknowledged && result.ModifiedCount == 0)
            {
                await _notes.InsertOneAsync(noteNew);
                return false;
            }

            return true;
        }

       //async Task<List<Note>> INoteCollectionService.GetNotesByOwnerId(Guid ownerId)
       // {
       //     throw new NotImplementedException();
       // }
    }
}
