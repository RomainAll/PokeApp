using System.Collections.ObjectModel;

namespace PokeApp.ViewModels
{
    internal class ListViewModel : BaseViewModel
    {
        private static ListViewModel _instance = new ListViewModel();
        public static ListViewModel Instance { get { return _instance; } }

        public ObservableCollection<Pokemon> MyList
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }

        public ListViewModel()
        {
            MyList = new ObservableCollection<Pokemon>();

            for (int i = 1; i < 10; i++)
            {
                MyList.Add(new Pokemon() { Name = "piplup" + i.ToString(), Number = i.ToString(), Type1 = "water" + i.ToString(), Type2 = "fire" + i.ToString(), UrlImg = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/393.png" });
            }
        }

    }
}
