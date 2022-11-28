using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PR155_2018_Web_projekat.Models
{
    public enum Pol { Zensko, Musko, Ostalo }
    public class Korisnik
    {
        private string korisnickoIme;
        private string lozinka;
        private string ime;
        private string prezime;
        private Pol pol;
        private string email;
        private DateTime datumRodjenja;
        private List<string> listaTreninga;
        private List<string> fitnesCentar;
     
        private bool prijavljen = false;
        private bool prijavljenNaTrening;





        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Pol Pol { get => pol; set => pol = value; }
        public string Email { get => email; set => email = value; }

        public List<string> ListaTreninga { get; set; }
        public List<string> FitnesCentar { get; set; }
        public UlogaKorisnika Uloga { get; set; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public bool Prijavljen { get => prijavljen; set => prijavljen = value; }
        public bool PrijavljenNaTrening { get => prijavljenNaTrening; set => prijavljenNaTrening = value; }
       

        public Korisnik()
        {
            /*TreninziPosetilac = new List<string>();
            TreninziTrener = new List<string>();
            FcTrener = new List<string>();
            fcVlasnik = new List<FitnesCentar>();*/

            prijavljenNaTrening = false;

            listaTreninga = new List<string>();
            FitnesCentar = new List<string>();
        }

        public Korisnik(string korisnickoIme)
        {

            korisnickoIme = korisnickoIme;
        }
    }
}