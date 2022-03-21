using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static List<Category> categories = new List<Category>
        {
            new Category
            {
                Name="To do",
                Id=1
            },
            new Category
            {
                  Name="Done",
                 Id=2
            },
            new Category
            {
                  Name="Doing",
                 Id=3
            }

        };
        
        /* [HttpGet]
         public IActionResult GetCategory()
         {
             return Ok(categories);
         }*/

        /// <summary>
        /// Get one random category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOne()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, 3);
            return Ok(categories[index]);
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// 
        /// <param name="categ"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateCategory(Category categ)
        {
            categories.Add(categ);
            if (categ == null)
            {
                return BadRequest("Note cannot be null");
            }
            return Ok(categ);
        }
        [HttpDelete("id")]
        public IActionResult DeleteCategory(int id)
         {
            categories = categories.Where(category => category.Id != id).ToList();
            return Ok(categories);
        }



    }
}
