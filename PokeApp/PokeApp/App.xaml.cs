using PokeApp.Repositories;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    public partial class App : Application
    {
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");

        public static PokeRepository PokeRepository { get; private set; }

        public App()
        {
            InitializeComponent();

            PokeRepository = new PokeRepository(dbPath);

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
