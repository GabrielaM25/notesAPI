using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Settings
{
   public interface IMongoDBSettings
    {
        string NoteCollectionName { get; set; }
        string OwnerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
