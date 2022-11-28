using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR155_2018_Web_projekat.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        public ActionResult Index()
        {
           
            //samo ako je prijavljen moci ce da menja profil
            Korisnik korisnik = (Korisnik)Session["korisnik"]; 
            if(korisnik == null || korisnik.KorisnickoIme == "")
            {
                //idi da se prijavis
                return RedirectToAction("Index", "Authentication");
            }
           
           //vju da menja profil
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult IzmeniProfil(Korisnik korisnik)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            Korisnik izmenjenKorisnik = (Korisnik)Session["korisnik"];
         
                izmenjenKorisnik.Lozinka = korisnik.Lozinka;
                izmenjenKorisnik.Ime = korisnik.Ime;
                izmenjenKorisnik.Prezime = korisnik.Prezime;
                izmenjenKorisnik.Pol = korisnik.Pol;
                izmenjenKorisnik.DatumRodjenja = korisnik.DatumRodjenja;

            izmenjenKorisnik.ListaTreninga = izmenjenKorisnik.ListaTreninga;
            izmenjenKorisnik.FitnesCentar = izmenjenKorisnik.FitnesCentar;

                korisnici[korisnici.FindIndex(x => x.KorisnickoIme == izmenjenKorisnik.KorisnickoIme)] = izmenjenKorisnik;
                RadSaPodacima.Prelepi(korisnici);

                return RedirectToAction("Index", "FitnesCentar");
         
        }


    }
}