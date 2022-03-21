using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PokeApiNet;
using PokeApp.Models;

namespace PokeApp.ViewModels
{
    internal class ListViewModel : BaseViewModel
    {
        //  Déclaration d'une instance de la ListeViewModel
        private static ListViewModel _instance = new ListViewModel();

        // Récupère l'instance de la ListeViewModel
        public static ListViewModel Instance { get { return _instance; } }

        // Déclaration d'une liste PokemonsList de type List<Pokemon>
        public List<Pokemon> PokemonsList = new List<Pokemon>();

        // Création d'un ObservableCollection<Pokemon> MyList
        public ObservableCollection<Pokemon> MyList
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }

        // Constructeur de la ListViewModel
        public ListViewModel()
        {
            MyList = new ObservableCollection<Pokemon>();
            PokeDeck = new ObservableCollection<Pokemon>();
            InitList();

        }

        // Méthode qui permet d'ajouter un pokémon dans la liste MyList
        public void PokeList(List<Pokemon> listPokemons)
        {
            MyList.Clear();
            foreach (Pokemon pokemon in listPokemons)
            {
                MyList.Add(pokemon);
            }
            
        }

        //  Création d'un ObservableCollection<Pokemon> PokeDeck
        public ObservableCollection<Pokemon> PokeDeck
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }

        // Méthode qui permet d'ajouter un pokémon dans la liste PokeDeck
        public void PokeListPokeDeck(List<Pokemon> listPokemons)
        {
            PokeDeck.Clear();
            foreach (Pokemon pokemon in listPokemons)
            {
                PokeDeck.Add(pokemon);
            }

        }

        // Méthode asynchrone qui permet de récuperer et d'ajouter les 50 premiers pokémons dans PokemonsList et MyList
        public async void InitList()
        {
            List<Pokemon> listPokemons = await App.PokeRepository.GetPokemonsAsync();
            List<Pokemon> pokeDeckDB = await App.PokeDeckRepository.GetPokemonsAsync();
            PokeApiClient pokeApiClient = new PokeApiClient();
            if (listPokemons.Count != 0)
            {
                foreach (Pokemon pokemon in listPokemons)
                {
                    MyList.Add(pokemon);
                    PokemonsList.Add(pokemon);
                }
            }
            if (pokeDeckDB.Count != 0)
            {
                foreach (Pokemon pokemon in pokeDeckDB)
                {
                    if (pokemon.isOnPokeDeck == true) { PokeDeck.Add(pokemon); }
                }
            }
            else
            {
                for (int i = 1; i <= 50; i++)
                {
                    PokeApiNet.Pokemon pokemon = await Task.Run(() => pokeApiClient.GetResourceAsync<PokeApiNet.Pokemon>(i));
                    PokeApiNet.PokemonSpecies pokemonSpecies = await Task.Run(() => pokeApiClient.GetResourceAsync<PokeApiNet.PokemonSpecies>(pokemon.Species));
                    Pokemon monPokemon = new Pokemon();
                    monPokemon.Name = pokemonSpecies.Names.Find(name => name.Language.Name.Equals("fr")).Name.ToString().ToUpper();
                    monPokemon.Number = pokemon.Id;
                    monPokemon.Url = pokemon.Sprites.FrontDefault;
                    monPokemon.Type1 = Constantes.ColorDictionary[pokemon.Types[0].Type.Name.ToLower()].Item1.ToUpper();
                    if (pokemon.Types.Count == 2)
                    {
                        monPokemon.FrameType2 = true;
                        monPokemon.Type2 = Constantes.ColorDictionary[pokemon.Types[1].Type.Name.ToLower()].Item1.ToUpper();
                        monPokemon.TypeColor2 = Constantes.ColorDictionary[pokemon.Types[1].Type.Name.ToLower()].Item2;
                    }

                    monPokemon.TypeColor1 = Constantes.ColorDictionary[pokemon.Types[0].Type.Name.ToLower()].Item2;
                    monPokemon.Taille = pokemon.Height / 10.0;
                    monPokemon.Poids = pokemon.Weight / 10.0;
                    monPokemon.Description = pokemonSpecies.FlavorTextEntries.FindLast(flavor => flavor.Language.Name.Equals("fr")).FlavorText.Replace("\n"," ");
                    monPokemon.UrlShiny = pokemon.Sprites.FrontShiny;
                    monPokemon.Hp = pokemon.Stats[0].BaseStat;
                    monPokemon.Attaque = pokemon.Stats[1].BaseStat;
                    monPokemon.Defense = pokemon.Stats[2].BaseStat;
                    monPokemon.AttaqueSpeciale = pokemon.Stats[3].BaseStat;
                    monPokemon.DefenseSpeciale = pokemon.Stats[4].BaseStat;
                    monPokemon.Vitesse = pokemon.Stats[5].BaseStat;
                    await App.PokeRepository.AddNewPokemonAsync(monPokemon);
                    MyList.Add(monPokemon);
                    PokemonsList.Add(monPokemon);
              
                }
            }
        }
    }
}
