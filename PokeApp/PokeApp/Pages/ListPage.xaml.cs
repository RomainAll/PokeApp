using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using PokeApp.ViewModels;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        // Constructeur de la page ListPage
        public ListPage()
        {
            InitializeComponent();
            BindingContext = ListViewModel.Instance;
        }

        // Déclaration d'une méthode asynchrone qui vérifie que si le pokémon est sélectionné, il nous revoit sa page de détail 
        async void Selection(Object sender, SelectionChangedEventArgs eventArgs)
        {
            Pokemon selectedPokemon = (eventArgs.CurrentSelection.FirstOrDefault() as Pokemon);
            if (selectedPokemon == null)
            {
                return;
            }
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new DetailsPage(selectedPokemon));
        }

        // Méthode qui permet de faire une recherche par nom dans la liste de pokémon, permettant ainsi d'y accéder plus simplement
        void SearchBarPoke(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            ListViewModel.Instance.PokeList(
                ListViewModel.Instance.PokemonsList.ToList().Where(
                    pokemon => pokemon.Name.ToUpper().Contains(
                        e.NewTextValue.ToUpper()
                        )).ToList());
        }
    }
}