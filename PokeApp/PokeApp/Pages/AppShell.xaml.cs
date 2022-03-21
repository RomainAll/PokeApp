using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        // Constructeur de l'AppShell
        public AppShell()
        {
            InitializeComponent();
        }
    }
}