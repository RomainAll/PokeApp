using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp.Models
{
    internal class Constantes
    {
        public static readonly Dictionary<string,(string,string)> ColorDictionary = new Dictionary<string,(string, string)>
        {
            {"rock", ("🏔 Roche", "#B6A136") },
            {"ghost",("👻 Spectre","#735797")},
            {"steel", ("⛓ Acier","#B7B7CE")},
            {"water", ("💧 Eau","#6390F0")},
            {"grass", ("🍀 Plante","#7AC74C")},
            {"psychic", ("🧠 Psy","#F95587")},
            {"ice", ("❄️ Glace","#96D9D6")},
            {"dark", ("🌘 Tenebre","#705746")},
            {"fairy", ("✨ Fée","#D685AD")},
            {"normal", ("👀 Normal","#A8A77A")},
            {"fighting", ("💣 Combat","#C22E28")},
            {"flying", ("🕊 Vol","#A98FF3")},
            {"poison", ("💀 Poison","#A33EA1")},
            {"ground", ("🏝 Sol","#E2BF65")},
            {"bug", ("🦟 Insecte","#A6B91A")},
            {"fire", ("🔥 Feu","#EE8130")},
            {"electric", ("⚡️ Electrik","#F7D02C")},
            {"dragon", ("🐲 Dragon","#6F35FC")}
        };
    }
}
