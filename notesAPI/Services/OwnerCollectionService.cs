using MongoDB.Driver;
using notesAPI.Models;
using notesAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Services
{
    public class OwnerCollectionService : IOwnerCollectionService
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerCollectionService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _owners = database.GetCollection<Owner>(settings.OwnerCollectionName);
        }
        public OwnerCollectionService()
        {
        }

        /* private static List<Owner> _owners = new List<Owner>{
             new Owner{Name = "Clive Owen" , Id = Guid.NewGuid()},
             new Owner{Name = "Natalie Portman" , Id = Guid.NewGuid()},
             new Owner{Name = "Mila Kunis" , Id = Guid.NewGuid()},
        };*/
        /*  
          public bool Create(Owner owner)
          {
              _owners.Add(owner);

              return true;
          }

          public bool Delete(Guid id)
          {
              int index = _owners.FindIndex(n => n.Id == id);
              if (index == -1)
              {

                  return false;

              }
              _owners.RemoveAt(index);
              return true; 
          }

          public Owner Get(Guid id)
          {
              Owner owner = _owners.Find(o => o.Id == id);

              return owner;
          }

          public List<Owner> GetAll()
          {

              return _owners;
          }

          public bool Update(Guid id, Owner owner
              )
          {
              int index = _owners.FindIndex(o=> o.Id == id);
              if (index == -1)
              {
                  Create(owner);
                  return false;

              }
              owner.Id = id;
              _owners[index] = owner;
              GetAll();
              return true;
          }*/

        public  async Task<bool> Create(Owner owner)
        {
            if (owner.Id == Guid.Empty)
            {
                owner.Id = Guid.NewGuid();
            }

            await _owners.InsertOneAsync(owner);
            return true;
        }

        public async Task<List<Owner>> GetAll()
        {
            var result = await _owners.FindAsync(owner => true);
            return result.ToList();

        }

        public async Task<Owner> Get(Guid id)
        {
            return (await _owners.FindAsync(owner => owner.Id == id)).FirstOrDefault();

        }

        public async Task<bool> Update(Guid id, Owner owner)
        {
            owner.Id = id;
            var result = await _owners.ReplaceOneAsync(note => note.Id == id, owner);
            if (!result.IsAcknowledged && result.ModifiedCount == 0)
            {
                await _owners.InsertOneAsync(owner);
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _owners.DeleteOneAsync(owner => owner.Id == id);
            if (!result.IsAcknowledged && result.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }
    }

        
        
    
}
