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
        public DetailsPage(Pokemon pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
        }

        public async void goBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

 
    }
}