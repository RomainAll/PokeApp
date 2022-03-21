using PokeApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokeApp.ViewModels;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPokemonPage : ContentPage
    {
        // Création de deux images de type MediaFile
        private MediaFile Image1 { get; set;}
        private MediaFile Image2 { get; set;}

        // Constructeur de la page NewPokemonPage
        public NewPokemonPage()
        {
            InitializeComponent();
                     
        }

        // Méthode asynchrone permettant d'accéder à la galerie de l'appareil photo afin de selectioner une photo d'un pokémon dans sa galerie 
        private async void OnTakePicture(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Impossible", "Votre appareil ne prend pas en charge", "OK");
                return;
            }

            Image1 = await CrossMedia.Current.PickPhotoAsync();
            if (Image1 == null)
            {
                return;
            }
            imagePoke.Source = ImageSource.FromStream(() => Image1.GetStream());
        }

        // Méthode asynchrone permettant d'accéder à la galerie de l'appareil photo afin de selectioner une photo d'un pokémon shiny dans sa galerie 
        private async void OnTakePictureShiny(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Impossible", "Votre appareil ne prend pas en charge", "OK");
                return;
            }

            Image2 = await CrossMedia.Current.PickPhotoAsync();
            if (Image2 == null)
            {
                return;
            }
            imagePokeShiny.Source = ImageSource.FromStream(() => Image2.GetStream());
        }

        // Méthode asynchrone qui permet l'ajout d'un pokémon que l'on crée dans la base de données et permet de l'ajouter ensuite dans la liste
        // Elle permet également de vérifier la saisie des différents champs par l'utilisateur (sauf le type n°2 qui est facultatif)
        // Une fois le pokémon ajouté, une alerte de confirmation, nous indique son ajout et on réinitialise les saisies 
        private async void OnNewButtonClicked(object sender, EventArgs e)
        {
            if ((nomPoke.Text == String.Empty) || 
                (description.Text == String.Empty) ||
                (taille.Text == String.Empty) ||
            (poids.Text == String.Empty) ||
            (pickerType.SelectedItem == null) ||
            (SlideHp.Value == 0) ||
            (SlideAttaque.Value == 0) ||
            (SlideAttaqueSpe.Value == 0) ||
            (SlideDefense.Value == 0) ||
            (SlideDefenseSpe.Value == 0) ||
            (SlideVitesse.Value == 0) ||
            (imagePoke.Source ==  null) ||
            (imagePokeShiny.Source == null) ||
            (LabelHp.Text == String.Empty) ||
            (LabelAttaque.Text == String.Empty) ||
            (LabelAttaqueSpe.Text == String.Empty) ||
            (LabelDefense.Text == String.Empty) ||
            (LabelDefenseSpe.Text == String.Empty) ||
            (LabelVitesse.Text == String.Empty))
            {
                await DisplayAlert("Ajout impossible !", "Merci de remplir tous les champs, sauf le type 2 (facultatif)", "OK");
                return;
            }
            else { 


            Pokemon pokemon = new Pokemon();
            pokemon.Name = nomPoke.Text.ToUpper();
            pokemon.Url = Image1.Path;
            pokemon.UrlShiny = Image2.Path;
            string monType1EnFr = (string)pickerType.SelectedItem;

            foreach (var typeinfo in Constantes.ColorDictionary)
            {
                if (typeinfo.Value.Item1 == monType1EnFr)
                {
                    pokemon.Type1 = Constantes.ColorDictionary[typeinfo.Key].Item1.ToUpper();
                    pokemon.TypeColor1 = Constantes.ColorDictionary[typeinfo.Key].Item2;
                }
            }
            if (pickerType2.SelectedItem != null)
            {
                pokemon.FrameType2 = true;
                string monType2EnFr = (string)pickerType2.SelectedItem;
                foreach (var typeinfo in Constantes.ColorDictionary)
                {
                    if (typeinfo.Value.Item1 == monType2EnFr)
                    {
                        pokemon.Type2 = Constantes.ColorDictionary[typeinfo.Key].Item1.ToUpper();
                        pokemon.TypeColor2 = Constantes.ColorDictionary[typeinfo.Key].Item2;
                    }
                }
            }
            pokemon.Poids = Convert.ToDouble(poids.Text);        
            pokemon.Taille = Convert.ToDouble(taille.Text);
            pokemon.Description = description.Text;
            pokemon.Hp = (int)SlideHp.Value;
            pokemon.Attaque = (int)SlideAttaque.Value;
            pokemon.Defense = (int)SlideDefense.Value;
            pokemon.AttaqueSpeciale = (int)SlideAttaqueSpe.Value;
            pokemon.DefenseSpeciale = (int)SlideDefenseSpe.Value;
            pokemon.Vitesse = (int)SlideVitesse.Value;

            await App.PokeRepository.AddNewPokemonAsync(pokemon);
            List<Pokemon> pokemons_bd = await App.PokeRepository.GetPokemonsAsync();
            ListViewModel.Instance.MyList.Clear();
            ListViewModel.Instance.PokemonsList.Clear();
            foreach (var pokemoni in pokemons_bd)
            {
                ListViewModel.Instance.MyList.Add(pokemoni);
                ListViewModel.Instance.PokemonsList.Add(pokemoni);
            }
                await DisplayAlert("Ajout réussi !", "Le pokemon : " + pokemon.Name + " a été ajouté ", "OK");

                nomPoke.Text = String.Empty;
                description.Text = String.Empty;
                taille.Text = String.Empty;
                poids.Text = String.Empty;
                pickerType.SelectedItem = null;
                pickerType2.SelectedItem = null;
                SlideHp.Value = 0;
                SlideAttaque.Value = 0;
                SlideAttaqueSpe.Value = 0;
                SlideDefense.Value = 0;
                SlideDefenseSpe.Value = 0;
                SlideVitesse.Value = 0;
                imagePoke.Source = "";
                imagePokeShiny.Source = "";
                LabelHp.Text = String.Empty;
                LabelAttaque.Text = String.Empty;
                LabelAttaqueSpe.Text = String.Empty;
                LabelDefense.Text = String.Empty;
                LabelDefenseSpe.Text = String.Empty;
                LabelVitesse.Text = String.Empty;
            }
        }

        // Méthode permettant de récupérer la valeur de chaque slider avec des nombres entiers
        private void Slide_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelHp.Text = SlideHp.Value.ToString(".");
            LabelAttaque.Text = SlideAttaque.Value.ToString(".");
            LabelDefense.Text = SlideDefense.Value.ToString(".");
            LabelAttaqueSpe.Text = SlideAttaqueSpe.Value.ToString(".");
            LabelDefenseSpe.Text = SlideDefenseSpe.Value.ToString(".");
            LabelVitesse.Text = SlideVitesse.Value.ToString(".");
        }

    }
}