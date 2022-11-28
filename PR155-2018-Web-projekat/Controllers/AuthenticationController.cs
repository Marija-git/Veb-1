using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR155_2018_Web_projekat.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {          
     
            return View(); 
        }

        public ActionResult Register()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);

        }

        [HttpPost]
        public ActionResult RegistracijaKorisnika(Korisnik korisnik)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            korisnik.Uloga = UlogaKorisnika.POSETILAC;
            korisnik.Prijavljen = true;
            korisnik.ListaTreninga = new List<string>();
            korisnik.ListaTreninga.Add("XXX");
            foreach (Korisnik k in korisnici)
            {
                if (k.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    ViewBag.Message = $"Korisnik {korisnik.KorisnickoIme} vec postoji!";
                    return View("RegisterError");
                }
            }

        
            korisnici.Add(korisnik);
            RadSaPodacima.SacuvajKorisnika(korisnik);
            Session["korisnik"] = korisnik;  //dodavanje na sesiju(automatski da bude prijavljen kada se registruje) 
           

            return RedirectToAction("Index", "FitnesCentar"); //**kasnije redirect za posetioca
        }

        [HttpPost]
        public ActionResult Login(string korisnickoIme,string lozinka) 
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            Korisnik k = korisnici.Find(u => u.KorisnickoIme.Equals(korisnickoIme) && u.Lozinka.Equals(lozinka));

            if (k == null)
            {
                ViewBag.Message = $"User with credentials does not exist!";
                return View("Index");
            }

            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            if (k.Uloga == UlogaKorisnika.TRENER)
            {
                foreach(string s in k.FitnesCentar)
                {
                    foreach(FitnesCentar fc in fitnesCentri)
                    {
                        if(s == fc.NazivFC)
                        {
                            if(fc.IsDeleted == true)
                            {
                                ViewBag.Message = "Ne mozes da se prijavis kao trener jer je vlasnik izbrisao fitnes centar";
                                return View("~/Views/Trener/Provera.cshtml");
                            }
                        }
                    }
                }
            }
            if(k.Prezime=="BLOKIRAN")
            {
                ViewBag.Message = "blokiran si";
                return View("~/Views/Trener/Provera.cshtml");
            }

            k.Prijavljen = true;
            Session["korisnik"] = k;
            return RedirectToAction("Index", "FitnesCentar"); //**treba da ode na stranicu u zavisnosti od uloge

        }

        public ActionResult Logout()
        {
            
            Session["korisnik"] = null; //obrisemo sve sa sesije
            ViewBag.Message = $"YOU LOGGED OUT";
            return View("Index");

        }


    }
}