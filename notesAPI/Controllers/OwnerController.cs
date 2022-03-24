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
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerCollectionService _ownerCollectionService;
        public OwnerController(IOwnerCollectionService ownerCollectionsService)
        {
            _ownerCollectionService = ownerCollectionsService ?? throw new ArgumentNullException(nameof(ownerCollectionsService));
        }   
        
        /// <summary>
        /// Get owners
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            List<Owner> owners = await _ownerCollectionService.GetAll();
            return Ok(owners);
        }
       /* public IActionResult GetOwners()
        {
            return Ok(_ownerCollectionService.GetAll());
        }*/


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnersById(Guid id)
        {
            return Ok(await _ownerCollectionService.Get(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOwners([FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Owner should not be null");
            }
            await _ownerCollectionService.Create(owner);
            return Ok(await _ownerCollectionService.GetAll());
        }

        /// <summary>
        /// Edit owner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public  async Task<IActionResult> UpdateOwner(Guid id, [FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Owner can't be null");
            }
            if (!await _ownerCollectionService.Update(id, owner))
            {

                return NotFound("This owner doesn't exist");
            }
                return Ok(await _ownerCollectionService.Get(id));

        }
        /// <summary>
        /// Delete the owner with that certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {

            if ( await _ownerCollectionService.Delete(id))
            {

                return Ok("Owner was deleted");

            }
            else
            {
                return NotFound("this owner doesn't exist");
            }
        }
    }
}
