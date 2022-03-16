using PokeApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokeApp.ViewModels;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPokemonPage : ContentPage
    {
       
        public NewPokemonPage()
        {
            InitializeComponent();
                     
        }

        private async void OnTakePicture(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Impossible", "Votre appareil ne prend pas en charge", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            imagePoke.Source = ImageSource.FromStream(() => file.GetStream());
        }


        private async void OnTakePictureShiny(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Impossible", "Votre appareil ne prend pas en charge", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            imagePokeShiny.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void OnNewButtonClicked(object sender, EventArgs e)
        {
           Pokemon pokemon = new Pokemon();
            pokemon.Name = nomPoke.Text;
            //url
            pokemon.Type1 = (string)pickerType.SelectedItem;
            pokemon.Type2 = (string)pickerType2.SelectedItem;
            //number
            //typecolor1
            //typecolor2
            //FrameType 2
            pokemon.Poids = Convert.ToDouble(poids.Text);        
            pokemon.Taille = Convert.ToDouble(taille.Text);
            pokemon.Description = description.Text;
            //urlShiny
            pokemon.Hp = SlideHp.Value;
            pokemon.Attaque = SlideAttaque.Value;
            pokemon.Defense = SlideDefense.Value;
            pokemon.AttaqueSpeciale = SlideAttaqueSpe.Value;
            pokemon.DefenseSpeciale = SlideDefenceSpe.Value;
            pokemon.Vitesse = SlideVitesse.Value;

            //ListPage.Instance.MyList = await App.PokeRepository.GetUsersAsync();            
            //List<Pokemon> pokemons = await App.PokeRepository.GetUsersAsync();
            

            await App.PokeRepository.AddNewUserAsync(pokemon);
            List<Pokemon> pokemons_bd = await App.PokeRepository.GetUsersAsync();
            ListViewModel.Instance.MyList.Clear();
            foreach (var pokemoni in pokemons_bd)
            {
                ListViewModel.Instance.MyList.Add(pokemoni);
            } 
            statusMessage.Text = App.PokeRepository.StatusMessage;         

        }

        public void onSubmit(Object sender, EventArgs e)
        {
            string type = pickerType.SelectedItem.ToString();
            string type2 = pickerType2.SelectedItem.ToString();
            Console.WriteLine(type);
            Console.WriteLine(type2);
        }

        private void Slide_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelHp.Text = SlideHp.Value.ToString(".");
            LabelAttaque.Text = SlideAttaque.Value.ToString(".");
            LabelDefense.Text = SlideDefense.Value.ToString(".");
            LabelAttaqueSpe.Text = SlideAttaqueSpe.Value.ToString(".");
            LabelDefenceSpe.Text = SlideDefenceSpe.Value.ToString(".");
            LabelVitesse.Text = SlideVitesse.Value.ToString(".");
            
        }

    }

}