using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private ObservableCollection<Pokemon> myList;

        private ObservableCollection<Pokemon> MyList
        {
            get { return myList; }
            set { myList = value; }
        }
        public ListPage()
        {
            InitializeComponent();
            BindingContext = this;

            MyList = new ObservableCollection<Pokemon>();

            for (int i = 1; i < 10; i++)
            {
                MyList.Add(new Pokemon() { Name = "piplup" + i.ToString(), Number = i.ToString(), Type1 = "water" + i.ToString(), Type2 = "fire" + i.ToString(), UrlImg = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/393.png" });
            }

            pokemonsList.ItemsSource = MyList;
        }

        //async void ShowDetails(object sender, EventArgs args)
        //{
        //    await Navigation.PushAsync(new DetailsPage());
        //}

    }
}