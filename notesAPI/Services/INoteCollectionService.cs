using notesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Services
{
    public interface INoteCollectionService : ICollectionService<Note>
    {
        public Task<List<Note>> GetNotesByOwnerId(Guid ownerId);
    }
}
