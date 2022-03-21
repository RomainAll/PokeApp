using PokeApp.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        Pokemon poke;
        // Constructeur de la page DetailsPage
        public DetailsPage(Pokemon pokemon)
        {
            poke = pokemon;
            InitializeComponent();
            BindingContext = pokemon;
        }

        // Méthode asynchrone qui nous renvoie sur la page de la liste lorsque l'on clique sur le bouton dans la vue détail du pokémon
        public async void goBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // Méthode asynchrone sur le Switch qui vérifie que si le booléen isOnPokeDeck est égale à faux, on l'initialise à vrai et 
        // on ajoute le pokémon dans la liste des favoris dans la page DeckPage et on actualise la liste de la page ListPage
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