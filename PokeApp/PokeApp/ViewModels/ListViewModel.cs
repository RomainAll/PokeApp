using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp.ViewModels
{
    public class ListViewModel : BaseViewModel
    {
        private static ListViewModel _instance = new ListViewModel();
        public ListViewModel Instance { get { return _instance; } }


    }
}
