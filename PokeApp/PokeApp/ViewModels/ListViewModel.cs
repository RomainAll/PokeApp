using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PokeApiNet;
using PokeApp.Models;

namespace PokeApp.ViewModels
{
    internal class ListViewModel : BaseViewModel
    {
        private static ListViewModel _instance = new ListViewModel();
        public static ListViewModel Instance { get { return _instance; } }

        public ObservableCollection<Pokemon> MyList
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }

        public ListViewModel()
        {
            MyList = new ObservableCollection<Pokemon>();

            InitList();
        }

        public async void InitList()
        {
            PokeApiClient pokeApiClient = new PokeApiClient();
            for(int i = 1; i <= 50; i++)
            {
                PokeApiNet.Pokemon pokemon = await Task.Run(() => pokeApiClient.GetResourceAsync<PokeApiNet.Pokemon>(i));
                PokeApiNet.PokemonSpecies pokemonSpecies = await Task.Run(() => pokeApiClient.GetResourceAsync<PokeApiNet.PokemonSpecies>(pokemon.Species));
                Pokemon monPokemon = new Pokemon();
                monPokemon.Name = pokemonSpecies.Names.Find(name => name.Language.Name.Equals("fr")).Name.ToString().ToUpper();
                monPokemon.Number = "#" + pokemon.Id;
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

                MyList.Add(monPokemon);
            }
        }

    }
}
