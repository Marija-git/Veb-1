using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR155_2018_Web_projekat.Controllers
{
    public class GrupniTreningController : Controller
    {
        // GET: GrupniTrening
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            ViewBag.grupniTreninzi = grupniTreninzi;
            return View(grupniTreninzi);
        }

        #region pretrage
        [HttpPost]
        public ActionResult PretragaPoNazivu(string nazivGT)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaPosetilaca = new List<string>();
            listaPosetilaca = korisnik.ListaTreninga;
            List<string> listapretrazenih2 = new List<string>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();
            
            foreach(var gt in listaPosetilaca)
            {
                if(gt == nazivGT)
                {
                    listapretrazenih2.Add(gt);
                }
            }

            foreach(var gt in grupniTreninzi)
            {
                foreach(string s in listapretrazenih2)
                {
                    if(gt.NazivGT == s)
                    {
                        listaPretrazenih.Add(gt);
                    }
                }
            }
             return  View("Pretraga",listaPretrazenih);
        }

      [HttpPost]
      public ActionResult PretragaPoNazivuFitnesCentra(string nekinazivfc)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;

            List<GrupniTrening> listagt = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();

            foreach(var s in trP)
            {
                foreach(var gt in grupniTreninzi)
                {
                    if(s == gt.NazivGT)
                    {
                        listagt.Add(gt);
                    }
                }
            }

            foreach(var gtt in listagt)
            {
                if(gtt.Fc.NazivFC == nekinazivfc)
                {
                    listaPretrazenih.Add(gtt);
                }
            }
            return View("Pretraga", listaPretrazenih);
        }


        [HttpPost]
        public ActionResult PretragaPoTipuTreninga(string tiptreninga)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> listagt = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();

            //dobavim prvo grupne treninge koji imaju isti naziv kao 
            //grupni treninzi na kojima je korisnik ucestvovao
            foreach (var s in trP)
            {
                foreach (var gt in grupniTreninzi)
                {
                    if (s == gt.NazivGT)
                    {
                        listagt.Add(gt);
                    }
                }
            }
            foreach(var gtt in listagt)
            {
                if(gtt.Tip.ToString() == tiptreninga)
                {
                    listaPretrazenih.Add(gtt);
                }
            }

            return View("Pretraga", listaPretrazenih);
        }

        [HttpPost]
        public ActionResult KombinovanaPretraga(string nazivGT, string nekinazivfc, string tiptreninga)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> listaPosetilaca = new List<string>();
            listaPosetilaca = korisnik.ListaTreninga;
            List<string> listapretrazenih2 = new List<string>();
            

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> listaPretrazenih = new List<GrupniTrening>();
            List<GrupniTrening> listaPretrazenih3 = new List<GrupniTrening>();

            foreach (var gt in listaPosetilaca)
            {
                if (gt == nazivGT)
                {
                    listapretrazenih2.Add(gt);
                }
            }
           foreach(var ngt in listapretrazenih2)
            {
                foreach(var gtt in grupniTreninzi)
                {
                    if(ngt == gtt.NazivGT)
                    {
                        listaPretrazenih.Add(gtt);
                    }
                }
            }
           foreach(var gttt in listaPretrazenih)
            {
                if(gttt.NazivGT == nazivGT && gttt.Fc.NazivFC==nekinazivfc && gttt.Tip.ToString() == tiptreninga)
                {
                    listaPretrazenih3.Add(gttt);
                }
            }
            return View("Pretraga", listaPretrazenih3);
        }
        #endregion

        #region sort

        public ActionResult SortirajPoNazivu()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            //dobavim grupne treninge koje je korisnik posetio
            foreach(string s in trP)
            {
                foreach(var gt in grupniTreninzi)
                {
                    if(s==gt.NazivGT)
                    {
                        pomocna.Add(gt);
                    }
                }
            }
            sortirani = pomocna;
            sortirani = sortirani.OrderBy(o => o.NazivGT).ToList();
            return View("Pretraga", sortirani);
        }

        public ActionResult SortirajPoNazivu2()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            //dobavim grupne treninge koje je korisnik posetio
            foreach (string s in trP)
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
            return View("Pretraga", sortirani);
        }

        public ActionResult SortirajPoTipuTreninga()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            //dobavim grupne treninge koje je korisnik posetio
            foreach (string s in trP)
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
            return View("Pretraga", sortirani);
        }

        public ActionResult SortirajPoTipuTreninga2()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            //dobavim grupne treninge koje je korisnik posetio
            foreach (string s in trP)
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
            return View("Pretraga", sortirani);
        }

        public ActionResult SortirajPoDatumu()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            //dobavim grupne treninge koje je korisnik posetio
            foreach (string s in trP)
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
            sortirani = sortirani.OrderBy(o => o.Termin).ToList();
            return View("Pretraga", sortirani);
        }
        public ActionResult SortirajPoDatumu2()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            List<string> trP = new List<string>();
            trP = korisnik.ListaTreninga;
            List<GrupniTrening> pomocna = new List<GrupniTrening>();

            List<GrupniTrening> grupniTreninzi = (List<GrupniTrening>)HttpContext.Application["grupniTreninzi"];
            List<GrupniTrening> sortirani = new List<GrupniTrening>();

            //dobavim grupne treninge koje je korisnik posetio
            foreach (string s in trP)
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
            sortirani = sortirani.OrderByDescending(o => o.Termin).ToList();
            return View("Pretraga", sortirani);
        }

        #endregion


    }
}