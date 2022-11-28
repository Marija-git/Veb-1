using PR155_2018_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PR155_2018_Web_projekat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            List<FitnesCentar> fitnesCentri = RadSaPodacima.UcitajFitnesCentar();
            HttpContext.Current.Application["fitnesCentri"] = fitnesCentri;

            List<Korisnik> korisnici = RadSaPodacima.UciatajKorisnika();
            HttpContext.Current.Application["korisnici"] = korisnici;

            List<GrupniTrening> grupniTreninzi = RadSaPodacima.UcitajGrupniTrening();
            HttpContext.Current.Application["grupniTreninzi"] = grupniTreninzi;




        }
    }
}
