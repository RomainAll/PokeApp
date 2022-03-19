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
        public DeckPage()
        {
            InitializeComponent();
            BindingContext = ListViewModel.Instance;

        }

        public static object Instance { get; internal set; }

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