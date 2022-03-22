using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notesAPI.Models;
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
        private static List<Owner> _owners = new List<Owner>{
            new Owner{Name = "Clive Owen" , Id = Guid.NewGuid()},
            new Owner{Name = "Natalie Portman" , Id = Guid.NewGuid()},
            new Owner{Name = "Mila Kunis" , Id = Guid.NewGuid()},
       };
        /// <summary>
        /// Get owners
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOwners()
        {
            return Ok(_owners);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOwners([FromBody] Owner owner)
        {
            _owners.Add(owner);
            return Ok(_owners);
        }

        /// <summary>
        /// Edit owner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateOwner(Guid id, [FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Owner can't be null");
            }
            int index = _owners.FindIndex(n => n.Id == id);
            if (index == -1)
            {

                return NotFound("Owner not found");

            }
            owner.Id = id;
            _owners[index] = owner;

            return Ok(_owners);
        }
        /// <summary>
        /// Delete the owner with that certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteOwner(Guid id) {
            int index = _owners.FindIndex(n => n.Id == id);
            if (index == -1)
            {

                return NotFound("Owner not found");

            }
            /*suprascrie lista cu o lista fara ownerul sters*/
            _owners = _owners.Where(n => n.Id == id).ToList();
            return Ok(_owners);
        }
    }
}
