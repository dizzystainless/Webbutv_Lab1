using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cats_api.Models
{
    public class Feline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        
        
    }
}