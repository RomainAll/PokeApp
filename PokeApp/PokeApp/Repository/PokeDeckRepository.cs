using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace PokeApp.Repositories
{
    public class PokeDeckRepository
    {
        private SQLiteAsyncConnection connection;

        public string StatusMessage { get; set; }

        public PokeDeckRepository(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Pokemon>();
        }


        public async Task AddNewPokemonAsync(Pokemon pokemon)
        {
            int result = 0;
            try
            {
                result = await connection.InsertAsync(pokemon);
                StatusMessage = $"le pokemon : {pokemon.Name} a été ajouté ";

            }
            catch (Exception ex)
            {
                StatusMessage = $"Impossible d'ajouter le pokemon : {pokemon.Name}. \n Erreur : {ex.Message}";
            }

        }

        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            try
            {
                return await connection.Table<Pokemon>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Impossible de récupérer la liste des pokemons. \n Erreur : {ex.Message}";

            }
            return new List<Pokemon>();
        }
    }
}
