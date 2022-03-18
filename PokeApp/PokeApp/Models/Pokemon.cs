using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PokeApp
{
    [Table("Pokemon")]
    public class Pokemon
    {
        public String Name { get; set; }

        public String Url { get; set; }

        public String Type1 { get; set; }

        public String Type2 { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Number { get; set; }

        public String TypeColor1 { get; set; }

        public String TypeColor2 { get; set; }

        public Boolean FrameType2 { get; set; }

        public Double Poids { get; set; }

        public Double Taille { get; set; }

        public String Description { get; set; }

        public String UrlShiny { get; set; }

        public int Hp { get; set; }

        public int Attaque { get; set; }

        public int Defense { get; set; }

        public int AttaqueSpeciale { get; set; }

        public int DefenseSpeciale { get; set; }

        public int Vitesse { get; set; }


    }
}
