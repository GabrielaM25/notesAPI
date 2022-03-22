using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace notesAPI.Models
{
    public class Note
    {
        [Required] public string Title { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        [Required] public  Guid Id { get; set; }

        [Required(ErrorMessage ="owner id is missing")] public  Guid OwnerId { get; set; }



    }
}
