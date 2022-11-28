using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR155_2018_Web_projekat.Models
{
    public class Adresa
    {
        private string ulica;
        private string broj;
        private string grad;
        private string postanskiBr;

        public string Ulica { get => ulica; set => ulica = value; }
        public string Broj { get => broj; set => broj = value; }
        public string Grad { get => grad; set => grad = value; }
        public string PostanskiBr { get => postanskiBr; set => postanskiBr = value; }

        public Adresa()
        {
            ulica = "";
            broj = "";
            grad = "";
            postanskiBr = "";
        }
    }
}