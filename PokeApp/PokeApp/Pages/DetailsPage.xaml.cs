using PokeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        Pokemon poke;
        public DetailsPage(Pokemon pokemon)
        {
            poke = pokemon;
            InitializeComponent();
            BindingContext = pokemon;
        }

        public async void goBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async void OnToggled(object sender, ToggledEventArgs e)
        {
            if (poke.isOnPokeDeck == false)
            {
                poke.isOnPokeDeck = true;
                await App.PokeDeckRepository.AddNewPokemonAsync(poke);
                List<Pokemon> pokemons_bd = await App.PokeDeckRepository.GetPokemonsAsync();
                ListViewModel.Instance.PokeDeck.Clear();
                foreach (var pokemoni in pokemons_bd)
                {
                    if (pokemoni.isOnPokeDeck == true)
                    {
                        ListViewModel.Instance.PokeDeck.Add(pokemoni);
                    }
                }

                List<Pokemon> pokemons_bdList = await App.PokeRepository.GetPokemonsAsync();
                ListViewModel.Instance.MyList.Clear();
                foreach (var pokemoni in pokemons_bdList)
                {
                    ListViewModel.Instance.MyList.Add(pokemoni);
                }
            }
        }

    }
}