using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
namespace PokeApp.Repositories
{
    public class PokeRepository
    {
        // Déclaration de connection de type SQLiteAsyncConnection
        private SQLiteAsyncConnection connection;

        // Déclaration d'un StatusMessage de type string
        public string StatusMessage { get; set; }

        // Constructeur de PokeRepository
        public PokeRepository(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Pokemon>();
        }

        // Méthode qui permet d'essayer l'insertion d'un pokémon (message de confirmation) sinon on retourne une exception avec une explication du problème rencontrer 
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

        // Méthode qui permet d'essayer de récupérer la lite des pokemons sinon on retourne une exception avec une explication du problème
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