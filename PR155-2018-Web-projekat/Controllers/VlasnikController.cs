using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR155_2018_Web_projekat.Controllers
{
    public class VlasnikController : Controller
    {
        // GET: Vlasnik
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme == "")
            {
                return RedirectToAction("Index", "Authentication");
            }

            if (korisnik.Uloga != UlogaKorisnika.VLASNIK)
            {
                return View("~/Views/FitnesCentar/UlogaError.cshtml");
            }

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            ViewBag.Korisnik = korisnik;

            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            ViewBag.fitnesCentri = fitnesCentri;

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            ViewBag.grupniTreninzi = grupniTreninzi;

            return View(korisnici);

        }

        [HttpPost]
        public ActionResult RegistrujTrenera()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            ViewBag.Korisnik = korisnik;
            return View();

        }

        [HttpPost] 
        public ActionResult Registracija(string korisnickoIme,string lozinka,string ime,string prezime,Pol pol,string email,
            DateTime datumRodjenja,string FitnesCentar)
        {
           
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<string> listaGrupnihTreningaZaTrenera = new List<string>();

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik noviTrener = new Korisnik();

            noviTrener.KorisnickoIme = korisnickoIme;
            noviTrener.Lozinka = lozinka;
            noviTrener.Ime = ime;
            noviTrener.Prezime = prezime;
            noviTrener.Pol = pol;
            noviTrener.Email = email;
            noviTrener.DatumRodjenja = datumRodjenja;
            noviTrener.Uloga = UlogaKorisnika.TRENER;
            noviTrener.FitnesCentar = new List<string>();
            noviTrener.FitnesCentar.Add(FitnesCentar);
            //sto se tice grupnih treninga dodelicu mu sve grupne treninge iz dodeljenog fitnes centra
            //ne pise u specifikaciji = implemetacija prepustena studentu
            noviTrener.ListaTreninga = new List<string>();
            foreach(GrupniTrening gt in grupniTreninzi)
            {
                if(gt.Fc.NazivFC == FitnesCentar)
                {
                    listaGrupnihTreningaZaTrenera.Add(gt.NazivGT);
                }
            }
            noviTrener.ListaTreninga = listaGrupnihTreningaZaTrenera;
            
            foreach(Korisnik k in korisnici)
            {
                if(k.KorisnickoIme==noviTrener.KorisnickoIme)
                {
                    ViewBag.Message = $"Korisisnik {k.KorisnickoIme} vec postoji";
                    return View("~/Views/Trener/Provera.cshtml");
                }
            }

            korisnici.Add(noviTrener);
            RadSaPodacima.Prelepi(korisnici);
            ViewBag.Message = "Uspesno kreiranje TRENERA";
            return View("~/Views/Trener/Provera.cshtml");
        }



        [HttpPost]
        public ActionResult ObrisiFitnesCentar(string nazivfc)
        {
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            foreach(GrupniTrening gt in grupniTreninzi)
            {
                if(gt.Fc.NazivFC == nazivfc && gt.Termin.CompareTo(DateTime.Now)>0)
                {
                    ViewBag.Message = "Zabranjeno brisanje zbog treninga u buducnosti";
                    return View("~/Views/Trener/Provera.cshtml");
                }
                
            }

            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            FitnesCentar fc = fitnesCentri.Find(x => x.NazivFC == nazivfc);
            fc.IsDeleted = !fc.IsDeleted;

            fitnesCentri[fitnesCentri.FindIndex(x => x.NazivFC == fc.NazivFC)] = fc;
            HttpContext.Application["fitnesCentri"] = fitnesCentri;
            RadSaPodacima.SacuvajFitnesCentar(fitnesCentri);


            ViewBag.Message = "Uspesno ste obrisali/vratili fitnes centar";
            return View("~/Views/Trener/Provera.cshtml");
        }


        [HttpPost]
        public ActionResult BlokirajTrenera(string imeTrenera)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik Newkorisnik = new Korisnik();
            foreach(Korisnik k in korisnici)
            {
                if(k.KorisnickoIme == imeTrenera && k.Uloga==UlogaKorisnika.TRENER)
                {
                    Newkorisnik.KorisnickoIme = k.KorisnickoIme;
                    Newkorisnik.Lozinka = k.Lozinka;
                    Newkorisnik.Ime = k.Ime;
                    Newkorisnik.Prezime = k.Prezime;
                    Newkorisnik.Pol = k.Pol;
                    Newkorisnik.Email = k.Email;
                    Newkorisnik.Uloga = k.Uloga;
                    Newkorisnik.DatumRodjenja = k.DatumRodjenja;
                    Newkorisnik.ListaTreninga = k.ListaTreninga;
                    Newkorisnik.FitnesCentar = k.FitnesCentar;
                }
            }
            korisnici[korisnici.FindIndex(x => x.KorisnickoIme == Newkorisnik.KorisnickoIme)] = Newkorisnik;
            Newkorisnik.Prezime = "BLOKIRAN";
            RadSaPodacima.Prelepi(korisnici);
            return View("~/Views/Trener/Provera.cshtml");


        }

        [HttpPost]
        public ActionResult KreirajFC()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KreiranjeFitnesCentra(string NazivFC,string Ulica,string Broj,string Grad
            ,string PostanskiBr, string mesecnaClanarina, string godisnjaClanarina, string cenaTreninga,
            string cenaGrupnogTreninga, string cenaIndividualnogTreninga)
        {
          
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            FitnesCentar noviFC = new FitnesCentar();
          string s =  DateTime.Now.Year.ToString();
            int broj = int.Parse(s);
            Adresa adr = new Adresa();
            adr.Ulica = Ulica;
            adr.Broj = Broj;
            adr.Grad = Grad;
            adr.PostanskiBr = PostanskiBr;
            Korisnik vlasnik = new Korisnik();
            vlasnik.KorisnickoIme = korisnik.KorisnickoIme;


            noviFC.NazivFC = NazivFC;
            noviFC.Adresa = adr;
            noviFC.GodinaOtvaranja = broj;
            noviFC.MesecnaClanarina = 100;
            noviFC.GodisnjaClanarina = 100;
            noviFC.CenaTreninga = 100;
            noviFC.CenaGrupnogTreninga = 100;
            noviFC.CenaIndividualnogTreninga = 100;
            noviFC.Vlasnik = vlasnik;
            noviFC.IsDeleted = false;
            foreach(FitnesCentar fc in fitnesCentri)
            {
                if(fc.NazivFC == NazivFC)
                {
                    ViewBag.Message = "Vec postoji fintes centar sa zadatim imenom";
                    return View("~/Views/Trener/Provera.cshtml");
                }
            }
            fitnesCentri.Add(noviFC);
            RadSaPodacima.SacuvajFitnesCentar(fitnesCentri);
            ViewBag.Message = $"Kreiran je Fitnes centar {noviFC.NazivFC}";
            return View("~/Views/Trener/Provera.cshtml");

        }

        [HttpPost]
        public ActionResult ModifikujFC(string nazivfc)
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];

            foreach (FitnesCentar fc in fitnesCentri)
            {
                if (fc.NazivFC==nazivfc)
                {
                    ViewBag.model = fc;
                    return View();
                }
            }
            return View();
      
        }

        [HttpPost]
        public ActionResult Modifikacija(string nazivFC,string ulica,string broj,string grad,string postanskibr,
           string mesecnaClanarina,string godisnjaClanarina
           ,string cenaTreninga,string cenaGrupnogTreninga,string cenaIndividualnogTreninga)
        {                   
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            FitnesCentar fc = fitnesCentri.Find(x => x.NazivFC == nazivFC);
            fc.Adresa.Ulica = ulica;
            fc.Adresa.Broj = broj;
            fc.Adresa.Grad = grad;
            fc.Adresa.PostanskiBr = postanskibr;
            fc.MesecnaClanarina = double.Parse(mesecnaClanarina);
            fc.GodisnjaClanarina = double.Parse(godisnjaClanarina);
            fc.CenaTreninga = double.Parse(cenaTreninga);
            fc.CenaGrupnogTreninga = double.Parse(cenaGrupnogTreninga);
            fc.CenaIndividualnogTreninga = double.Parse(cenaIndividualnogTreninga);
            fc.GodinaOtvaranja = fc.GodinaOtvaranja;
            fc.IsDeleted = fc.IsDeleted;
            fc.NazivFC = fc.NazivFC;
            fc.Vlasnik = fc.Vlasnik;
            fitnesCentri[fitnesCentri.FindIndex(x => x.NazivFC == fc.NazivFC)] = fc;
            HttpContext.Application["fitnesCentri"] = fitnesCentri;
            RadSaPodacima.SacuvajFitnesCentar(fitnesCentri);
            ViewBag.Message = "Modifikacija uspesna";
            return View("~/Views/Trener/Provera.cshtml"); 

        }


    }
}