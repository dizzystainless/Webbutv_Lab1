using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using cats_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cats_api.Data
{
    public class LoadData
    //Genererar en lista från jsonfil och gör om det till en lista av objekt
    {
        public static async Task LoadFelines(FelineContext context)
        {
            //Säkerhetsgrej för att kunna ha kvar koden nedan, 
            //annars läses datan in igen vid varje "run" och det blir error
            if(await context.Felines.AnyAsync()) return;

            var data = await File.ReadAllTextAsync("Data/felines.json");
            var felines = JsonSerializer.Deserialize<List<Feline>>(data);

            await context.AddRangeAsync(felines);
            await context.SaveChangesAsync();
            
        }
    }
}