using PokeApp.Repositories;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PokeApp
{
    public partial class App : Application
    {
        // Déclaration de deux variable de type string permetant d'accéder au chemin de la base de donnée
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");
        private string dbPathPokeDeck = Path.Combine(FileSystem.AppDataDirectory, "database.db1");

        // Déclatartion  static de PokeRepository et de PokeDeckRepository
        public static PokeRepository PokeRepository { get; private set; }
        public static PokeDeckRepository PokeDeckRepository { get; private set; }


        // Constructeur de APP
        public App()
        {
            InitializeComponent();

            PokeRepository = new PokeRepository(dbPath);
            PokeDeckRepository = new PokeDeckRepository(dbPathPokeDeck);
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
