using PokeApp.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckPage : ContentPage
    {
        // Constructeur de la page DeckPage
        public DeckPage()
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

    }
}