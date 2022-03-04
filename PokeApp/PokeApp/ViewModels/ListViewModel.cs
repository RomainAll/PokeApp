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
                Pokemon monPokemon = new Pokemon();
                monPokemon.Name = pokemon.Name.ToUpper();
                monPokemon.Number = "#" + pokemon.Id;
                monPokemon.Url = pokemon.Sprites.FrontDefault;
                monPokemon.Type1 = pokemon.Types[0].Type.Name.ToUpper();
                if (pokemon.Types.Count == 2)
                {
                    monPokemon.FrameType2 = true;
                    monPokemon.Type2 = pokemon.Types[1].Type.Name.ToUpper();
                }

                monPokemon.TypeColor = Constantes.ColorDictionary[monPokemon.Type1.ToLower()];
                MyList.Add(monPokemon);
            }
        }

    }
}
