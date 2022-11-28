using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR155_2018_Web_projekat.Controllers
{
    public class FitnesCentarController : Controller
    {
        // GET: FitnesCentar
        public ActionResult Index()
        {
            
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            ViewBag.fitnesCentri = fitnesCentri;

            return View(fitnesCentri);
        }

        [HttpPost]
        public ActionResult Details(string nazivFc)
        {

            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];

            ViewBag.model = grupniTreninzi;

            foreach (FitnesCentar fc in fitnesCentri)
            {

                if (fc.NazivFC == nazivFc)
                {

                    return View(fc);
                }
            }
            return RedirectToAction("Index");
        }


        #region pretrage


        [HttpPost]
        public ActionResult PretragaPoNazivu(string nazivFc)
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> listaPretrazenih = new List<FitnesCentar>();

            foreach (var fc in fitnesCentri)
            {
                if (fc.NazivFC == nazivFc)
                {
                    listaPretrazenih.Add(fc);
                }
            }
            return View("Index", listaPretrazenih);

        }

        [HttpPost]
        public ActionResult PretragaPoAdresi(string adresa)
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> listaPretrazenih = new List<FitnesCentar>();

            foreach (var fc in fitnesCentri)
            {
                if (fc.Adresa.Ulica == adresa)
                {
                    listaPretrazenih.Add(fc);
                }

                if (fc.Adresa.Grad == adresa)
                {
                    listaPretrazenih.Add(fc);
                }

            }
            return View("Index", listaPretrazenih);
        }

        [HttpPost]
        public ActionResult PretragaPoGodiniOtvaranja(int donjaGranica, int gornjaGranica)
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> listaPretrazenih = new List<FitnesCentar>();

            foreach (var fc in fitnesCentri)
            {
                if (fc.GodinaOtvaranja >= donjaGranica && fc.GodinaOtvaranja <= gornjaGranica)
                {
                    listaPretrazenih.Add(fc);
                }
            }
            return View("Index", listaPretrazenih);
        }

        [HttpPost]
        public ActionResult KombinovanaPretraga(string nazivfc, string grad, string godinaOtvaranja)
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> listaPretrazenih = new List<FitnesCentar>();

            int godOtv;
            godOtv = int.Parse(godinaOtvaranja);
            foreach (var fc in fitnesCentri)
            {
                if (fc.NazivFC == nazivfc && fc.Adresa.Grad == grad && fc.GodinaOtvaranja == godOtv)
                {
                    listaPretrazenih.Add(fc);
                }
            }
            return View("Index", listaPretrazenih);

        }


        #endregion

        #region sort
        public ActionResult SortirajPoNazivu()
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            sortirani = fitnesCentri;
            sortirani = sortirani.OrderBy(o => o.NazivFC).ToList();
            return View("Index", sortirani);
        }

        public ActionResult SortirajPoNazivu2()
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            sortirani = fitnesCentri;
            sortirani = sortirani.OrderByDescending(o => o.NazivFC).ToList();
            return View("Index", sortirani);
        }

        public ActionResult SortirajPoAdresi()
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            sortirani = fitnesCentri;
            sortirani = sortirani.OrderBy(o => o.Adresa.Ulica).ToList();
            return View("Index", sortirani);
        }

        public ActionResult SortirajPoAdresi2()
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            sortirani = fitnesCentri;
            sortirani = sortirani.OrderByDescending(o => o.Adresa.Ulica).ToList();
            return View("Index", sortirani);
        }

        public ActionResult SortirajGodiniOtvaranja()
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            sortirani = fitnesCentri;
            sortirani = sortirani.OrderBy(o => o.GodinaOtvaranja).ToList();
            return View("Index", sortirani);
        }
        public ActionResult SortirajGodiniOtvaranja2()
        {
            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            sortirani = fitnesCentri;
            sortirani = sortirani.OrderByDescending(o => o.GodinaOtvaranja).ToList();
            return View("Index", sortirani);

        }

        #endregion

        public ActionResult PrikaziGT(string nazivfc) 
        {
            
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme == "")
            {
                //idi da se prijavis
                return RedirectToAction("Index", "Authentication");
            }

            if(korisnik.Uloga == UlogaKorisnika.TRENER)
            {
                return View("UlogaError");
            }

            List<FitnesCentar> fitnesCentri = (List<FitnesCentar>)HttpContext.Application["fitnesCentri"];
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            ViewBag.model = grupniTreninzi;
            ViewBag.Message = nazivfc;
            ViewBag.Korisnik = korisnik;
            return View(); 
        }

        
        [HttpPost]
        public ActionResult PrijaviSe(string nazivgt)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme == "")
            {
                //idi da se prijavis
                return RedirectToAction("Index", "Authentication");
            }
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            GrupniTrening gt = grupniTreninzi.Find(x => x.NazivGT == nazivgt);

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik k = korisnici.Find(x => x.KorisnickoIme == korisnik.KorisnickoIme);

            if (gt.Posetioci.Contains(korisnik.KorisnickoIme))
            {
                ViewBag.Message = "Vec ste medju prijavljenim korisnicima";
                return View("PrijavaSuccess");
            }
            else
            {
                if(k.ListaTreninga[0] == "XXX")
                {
                    k.ListaTreninga[0] = gt.NazivGT;
                }
                else
                {
                    k.ListaTreninga.Add(gt.NazivGT);
                }
              

                if(gt.Posetioci[0] == "XXX")
                {
                    gt.Posetioci[0] = k.KorisnickoIme;
                    RadSaPodacima.SacuvajGrupniTrening(grupniTreninzi);

                }else
                {
                    gt.Posetioci.Add(k.KorisnickoIme);
                    RadSaPodacima.SacuvajGrupniTrening(grupniTreninzi);
                }

                RadSaPodacima.Prelepi(korisnici);
                ViewBag.Message = "Uspesna prijava";
                return View("PrijavaSuccess");
            }
                                   
        }




    }
}