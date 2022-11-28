using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR155_2018_Web_projekat.Controllers
{
    public class TrenerController : Controller
    {
        // GET: Trener
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme == "")
            {
                return RedirectToAction("Index", "Authentication");
            }

            if(korisnik.Uloga != UlogaKorisnika.TRENER)
            {
                return View("~/Views/FitnesCentar/UlogaError.cshtml");
            }

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            ViewBag.Korisnik = korisnik;

            return View(grupniTreninzi);
        }
        
        [HttpPost]
        public ActionResult PrikaziPosetioce(string nazivgt)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<string> pomocna = new List<string>();
            List<Korisnik> pomocna2 = new List<Korisnik>();
            //dobavim listu posetilaca(stringovi)
            foreach(GrupniTrening gt in grupniTreninzi)
            {
                if(gt.NazivGT == nazivgt)
                {
                    foreach(string s in gt.Posetioci)
                    {
                        pomocna.Add(s);
                    }
                }
            }
            //sad ih nadjem medju korisnicima i dobavim korisnike sa tim imenima
            foreach(Korisnik k in korisnici)
            {
                foreach(string s in pomocna)
                {
                    if(k.KorisnickoIme == s)
                    {
                         
                        pomocna2.Add(k);
                        ViewBag.model = pomocna2;
                    }
                }
            }
            
            return View("Posetioci");

        }

        #region pretrage
        [HttpPost]
        public ActionResult PretragaPoNazivu(string nazivGT)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<string> listapretrazenih2 = new List<string>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();

            foreach (var gt in listaTreninga)
            {
                if (gt == nazivGT)
                {
                    listapretrazenih2.Add(gt);
                }
            }

            foreach (var gt in grupniTreninzi)
            {
                foreach (string s in listapretrazenih2)
                {
                    if (gt.NazivGT == s)
                    {
                        listaPretrazenih.Add(gt);
                    }
                }
            }
            return View("Pronadjeni", listaPretrazenih);
        }

        [HttpPost]
        public ActionResult PretragaPoTipuTreninga(string tiptreninga)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<GrupniTrening> listagt = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();

            foreach (var s in listaTreninga)
            {
                foreach (var gt in grupniTreninzi)
                {
                    if (s == gt.NazivGT)
                    {
                        listagt.Add(gt);
                    }
                }
            }
            foreach (var gtt in listagt)
            {
                if (gtt.Tip.ToString() == tiptreninga)
                {
                    listaPretrazenih.Add(gtt);
                }
            }

            return View("Pronadjeni", listaPretrazenih);
        }


        //ostao datum posebno i datum komb
        [HttpPost]
        public ActionResult KombinovanaPretraga(string nazivGT, string tiptreninga)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<string> listapretrazenih2 = new List<string>();


            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();
            List<GrupniTrening> listaPretrazenih3 = new List<GrupniTrening>();

            foreach (var gt in listaTreninga)
            {
                if (gt == nazivGT)
                {
                    listapretrazenih2.Add(gt);
                }
            }
            foreach (var ngt in listapretrazenih2)
            {
                foreach (var gtt in grupniTreninzi)
                {
                    if (ngt == gtt.NazivGT)
                    {
                        listaPretrazenih.Add(gtt);
                    }
                }
            }
            foreach (var gttt in listaPretrazenih)
            {
                if (gttt.NazivGT == nazivGT &&  gttt.Tip.ToString() == tiptreninga)
                {
                    listaPretrazenih3.Add(gttt);
                }
            }
            return View("Pronadjeni", listaPretrazenih3);
        }
        #endregion

        #region sort
        public ActionResult SortirajPoNazivu()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            foreach (string s in listaTreninga)
            {
                foreach (var gt in grupniTreninzi)
                {
                    if (s == gt.NazivGT)
                    {
                        pomocna.Add(gt);
                    }
                }
            }
            sortirani = pomocna;
            sortirani = sortirani.OrderBy(o => o.NazivGT).ToList();
            return View("Pronadjeni", sortirani);
        }

        public ActionResult SortirajPoNazivu2()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            foreach (string s in listaTreninga)
            {
                foreach (var gt in grupniTreninzi)
                {
                    if (s == gt.NazivGT)
                    {
                        pomocna.Add(gt);
                    }
                }
            }
            sortirani = pomocna;
            sortirani = sortirani.OrderByDescending(o => o.NazivGT).ToList();
            return View("Pronadjeni", sortirani);
        }

        public ActionResult SortirajPoTipuTreninga()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            foreach (string s in listaTreninga)
            {
                foreach (var gt in grupniTreninzi)
                {
                    if (s == gt.NazivGT)
                    {
                        pomocna.Add(gt);
                    }
                }
            }
            sortirani = pomocna;
            sortirani = sortirani.OrderBy(o => o.Tip.ToString()).ToList();
            return View("Pronadjeni", sortirani);
        }

        public ActionResult SortirajPoTipuTreninga2()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaTreninga = new List<string>();
            listaTreninga = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            foreach (string s in listaTreninga)
            {
                foreach (var gt in grupniTreninzi)
                {
                    if (s == gt.NazivGT)
                    {
                        pomocna.Add(gt);
                    }
                }
            }
            sortirani = pomocna;
            sortirani = sortirani.OrderByDescending(o => o.Tip.ToString()).ToList();
            return View("Pronadjeni", sortirani);
        }

        #endregion

        [HttpPost]
        public ActionResult DeleteOrRestoreGT(string nazivgt)
        {
            // isDeleted je false prvi put !!
            //if(gt.isDeleted == false)->return vju(vjubag.message :obrisan)->gt.isDeleted = true;
            //elseif(gt.isdeleted = true)->return vju(vjubag.message:vracen)->gt.isdelete=false;

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            GrupniTrening gt = grupniTreninzi.Find(x => x.NazivGT == nazivgt);
            gt.IsDeleted = !gt.IsDeleted;

            grupniTreninzi[grupniTreninzi.FindIndex(x => x.NazivGT == gt.NazivGT)] = gt;
            HttpContext.Application["grupniTreninzi"] = grupniTreninzi;
            RadSaPodacima.SacuvajGrupniTrening(grupniTreninzi);

            return View("DeletedOrRestoredMessage");
            
            
        }

        [HttpPost]
        public ActionResult Modifikuj(string nazivgt)
        {
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];

            foreach(GrupniTrening gt in grupniTreninzi)
            {
                if(gt.NazivGT == nazivgt)
                {
                    ViewBag.model = gt;
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult IzmeniGrupniTrening(string nazivgt ,TipTreninga tip,int trajanje,DateTime termin)
        {
             List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
             GrupniTrening gt = grupniTreninzi.Find(x => x.NazivGT == nazivgt);

            gt.NazivGT = gt.NazivGT;
            gt.Fc = gt.Fc;
            gt.MaxPosetilaca = gt.MaxPosetilaca;
            gt.Posetioci = gt.Posetioci;
            gt.IsDeleted = gt.IsDeleted;

            gt.Tip = tip;
            gt.Trajanje = trajanje;
            gt.Termin = termin;

            grupniTreninzi[grupniTreninzi.FindIndex(x => x.NazivGT == gt.NazivGT)] = gt;
            HttpContext.Application["grupniTreninzi"] = grupniTreninzi;
            RadSaPodacima.SacuvajGrupniTrening(grupniTreninzi);

            ViewBag.Message = "Modifikacija uspesna";
            return View("Provera");

  
        }

        [HttpPost]
        public ActionResult KreirajGT()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KreiranjeGrupnogTreninga(string nazivGT,TipTreninga tip,int trajanje,int maxPosetilaca,string nazivFC,DateTime termin)
        {
            if((termin -DateTime.Now).TotalDays < 3)
            {
                ViewBag.Message = "Trening mora biti kreiran najmanje 3 dana unapred!";
                return View("Provera");
            }
                            

            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            GrupniTrening gt = new GrupniTrening();
            FitnesCentar fc = new FitnesCentar();

            gt.Fc = fc;
            fc.NazivFC = nazivFC;

            gt.NazivGT = nazivGT;
            gt.Tip = tip;
            gt.Trajanje = trajanje;
            gt.MaxPosetilaca = maxPosetilaca;
            gt.Fc.NazivFC = nazivFC;
            gt.Termin = termin;
            gt.Posetioci = new List<string>();
            gt.Posetioci.Add("XXX");
            gt.IsDeleted = false;
            
            foreach(GrupniTrening gtt in grupniTreninzi)
            {
                if(gtt.NazivGT == gt.NazivGT)
                {
                    ViewBag.Message = $"Grupni trening {gtt.NazivGT} vec postoji";
                    return View("Provera");
                }
            }

            List<string> pomocnaLista = korisnik.ListaTreninga;
            pomocnaLista.Add(gt.NazivGT);
            korisnik.KorisnickoIme = korisnik.KorisnickoIme;
            korisnik.Lozinka = korisnik.Lozinka;
            korisnik.Ime = korisnik.Ime;
            korisnik.Prezime = korisnik.Prezime;
            korisnik.Pol = korisnik.Pol;
            korisnik.Email = korisnik.Email;
            korisnik.DatumRodjenja = korisnik.DatumRodjenja;
            korisnik.ListaTreninga = pomocnaLista;
            korisnik.FitnesCentar = korisnik.FitnesCentar;          
            korisnici[korisnici.FindIndex(x => x.KorisnickoIme == korisnik.KorisnickoIme)] = korisnik;


            grupniTreninzi.Add(gt);
            RadSaPodacima.SacuvajGrupniTreningJedan(gt);
            RadSaPodacima.Prelepi(korisnici);
            ViewBag.Message = "Uspesno kreiranje";
            return View("Provera");
        }




    }
}