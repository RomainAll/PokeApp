using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp
{
    internal class Pokemon
    {
        private String name;
        public String Name { get { return name; } set { name = value; } }

        private String description;
        public String Description { get { return description; } set { description = value; } }

        private String type1;
        public String Type1 { get { return type1; } set { type1 = value; } }

        private String type2;
        public String Type2 { get { return type2; } set { type2 = value; } }

        private String number;
        public String Number { get { return "#" + number; } set { number = value; } }

        private String ability1;
        public String Ability1 { get { return ability1; } set { ability1 = value; } }
       
        private String ability2;
        public String Ability2 { get { return ability2; } set { ability2 = value; } }

        private String urlImg;
        public String UrlImg { get { return urlImg; } set { urlImg = value; } }

        private String evolution;
        public String Evolution { get { return evolution; } set { evolution = value; } }
    }
}
