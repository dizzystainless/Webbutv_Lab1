using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cats_api.Data;
using cats_api.Interfaces;
using cats_api.Models;
using cats_api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cats_api.Controllers
{
    [ApiController]
    [Route("api/feline")]
    public class FelineController : ControllerBase
    {
        //constructor
        private readonly IFelineRepository repo;
        public FelineController(IFelineRepository repo)
        {
            this.repo = repo;    
        }

        //ENDPOINTS(metoder)
        //endpoint 1 LISTA
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            return Ok(await repo.ListAllFelinesAsync());
        }

        //endpoint 2 SÖKA PÅ ID
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<Feline>> FindById(int id)
        { 
            var feline = await repo.FindFelineAsync(id);
            if(feline != null) return Ok(feline);
            
            return NotFound($"Could not find any cat with id: {id}");
        }

        //endpoint 3 SÖKA PÅ NAMN
        [HttpGet("byName/{name}")]
        public async Task<ActionResult<Feline>> FindByName(string name)
        { 
            var feline = await repo.FindFelineByNameAsync(name);
            if(feline != null) return Ok(feline);
            
            return NotFound($"Could not find any cat with the name: {name}");
        }

        //endpoint 4 SÖKA PÅ RAS
        [HttpGet("byBreed/{breed}")]
        public async Task<IActionResult> FindByBreed(string breed)
        { 
            var felines= await repo.FindFelineByBreedAsync(breed);
            if(felines != null) return Ok(felines);

            return NotFound($"Could not find any cats for breed: {breed}");
        }

        //endpoint 5 LÄGGA TILL
        [HttpPost()]
        public async Task<IActionResult> AddFeline(PostViewModel feline)
        {
            var addedFeline = new Feline
            {
                Name = feline.Name,
                Breed = feline.Breed
            };

            if(await repo.AddNewFelineAsync(addedFeline))
            {
                if(!await repo.SaveFeline()) return StatusCode(500, "Ooops something went wrong!");
                return StatusCode(201, addedFeline);
            } 
            
            return StatusCode(500, "Oops something went wrong!");
        }

        //endpoint 6 UPPDATERA EN RESURS I SIN HELHET
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeline(int id, [FromBody] UpdateFelineViewModel feline)
        {
            //Hämta katt med hjälp av id
            var toUpdate = await repo.FindFelineAsync(id);
            if(toUpdate == null) return NotFound($"Could not find any cat with id: {id}");

            //Byta ut värdena på hämtad katt med det som kommer i "feline"
            toUpdate.Name = feline.Name;
            toUpdate.Breed = feline.Breed;

            //Spara
            if(repo.UpdateFeline(toUpdate)) 
              if(await repo.SaveFeline()) return NoContent();

            return StatusCode(500, "Something else went wrong!");
        }
        
        //endpoint 7 UPPDATERA UTVALDA DELAR AV EN RESURS 
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateFelineNameViewModel feline) 
        {
            //Hämta katt med hjälp av id
            var toUpdate = await repo.FindFelineAsync(id);
            if(toUpdate == null) return NotFound($"Could not find any cat with id: {id}");

           //Byta ut namnet på hämtad katt med namnet som kommer i "feline"
            toUpdate.Name = feline.Name;

            //Spara
            if(repo.UpdateFeline(toUpdate)) 
              if(await repo.SaveFeline()) return NoContent();

            return StatusCode(500, "Something else went wrong!");
        }

        //endpoint 8 TA BORT
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFeline(int id)
        {
            //Hämta katt med hjälp av id
            var toDelete = await repo.FindFelineAsync(id);
            if(toDelete == null) return NotFound($"Could not find any cat with id: {id}");

            //Ta bort katten ur databasen och spara
            if(repo.RemoveFeline(toDelete))
              if(await repo.SaveFeline()) return NoContent();
            
            return StatusCode(500, $"Could not find any cat with id: {id}");
        }     
    }
}