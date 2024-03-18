using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cats_api.Models;

namespace cats_api.Interfaces
{     public interface IFelineRepository
    {
        Task<bool> AddNewFelineAsync(Feline feline);
        bool UpdateFeline(Feline feline);
        bool RemoveFeline(Feline feline);
        Task<IList<Feline>> ListAllFelinesAsync();
        Task<Feline> FindFelineByNameAsync(string name);
        Task<Feline> FindFelineAsync(int id);
        Task<IList<Feline>> FindFelineByBreedAsync(string breed);
        Task<bool> SaveFeline();
    }
}