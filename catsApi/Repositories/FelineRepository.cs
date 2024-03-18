using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cats_api.Data;
using cats_api.Interfaces;
using cats_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cats_api.Repositories
{
    public class FelineRepository : IFelineRepository
    {
        //constructor
        private readonly FelineContext context;
        public FelineRepository(FelineContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddNewFelineAsync(Feline feline)
        {
            try
            {
               await context.Felines.AddAsync(feline); 
               return true;
            }
            catch 
            {
              return false;    
            }   
        }

        public async Task<Feline> FindFelineAsync(int id)
        {
            return await context.Felines.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IList<Feline>> FindFelineByBreedAsync(string breed)
        {
            return await context.Felines.Where(c => c.Breed.ToLower().Trim() == breed.ToLower().Trim()).ToListAsync();

        }

        public async Task<Feline> FindFelineByNameAsync(string name)
        {
             return await context.Felines.FirstOrDefaultAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
    
        }

        public async Task<IList<Feline>> ListAllFelinesAsync()
        {
            var felines = await context.Felines.ToListAsync();
            return felines;
            
        }

        public bool RemoveFeline(Feline feline)
        {
            try
            {
                 context.Felines.Remove(feline);
                 return true;
            }
            catch
            {  
                return false;
            }    
        }

        public async Task<bool> SaveFeline()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool UpdateFeline(Feline feline)
        {
            try
            {
                 context.Felines.Update(feline);
                 return true;
            }
            catch 
            {
                return false;
            }
        }    
    }
}