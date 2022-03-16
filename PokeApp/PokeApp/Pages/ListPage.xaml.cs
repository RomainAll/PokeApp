using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Linq;
using PokeApp.ViewModels;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
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