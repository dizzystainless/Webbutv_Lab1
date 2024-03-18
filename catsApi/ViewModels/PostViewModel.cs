using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cats_api.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage ="Breed must be included")]
        public string Breed { get; set; }
    }
}