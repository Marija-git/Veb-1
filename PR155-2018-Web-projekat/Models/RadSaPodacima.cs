using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Hosting;

namespace PR155_2018_Web_projekat.Models
{
    public class RadSaPodacima
    {
       
        public static string FcPutanja = "~/App_Data/FitnesCentri.txt";
        public static string KPutanja = "~/App_Data/Korisnici.txt";
        public static string GtPutanja = "~/App_Data/GrupniTreninzi.txt";


        public static List<FitnesCentar> UcitajFitnesCentar()
        {
            List<FitnesCentar> fitncesCentri = new List<FitnesCentar>();
            List<FitnesCentar> sortirani = new List<FitnesCentar>();
            string line = "";
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath(FcPutanja)))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(';');
                    FitnesCentar fitncesCentar = new FitnesCentar()
                    {
                        NazivFC = tokens[0],
                        Adresa = new Adresa()
                        {
                            Ulica = tokens[1],
                            Broj = tokens[2],
                            Grad = tokens[3],
                            PostanskiBr = tokens[4]
                        },
                        GodinaOtvaranja = int.Parse(tokens[5]),
                        MesecnaClanarina = double.Parse(tokens[6]),
                        GodisnjaClanarina = double.Parse(tokens[7]),
                        CenaTreninga = double.Parse(tokens[8]),
                        CenaGrupnogTreninga = double.Parse(tokens[9]),
                        CenaIndividualnogTreninga = double.Parse(tokens[10]),

                        Vlasnik = new Korisnik()
                        {
                            KorisnickoIme = tokens[11],

                        },
                        IsDeleted = bool.Parse(tokens[12]),
                       

                    };
                    fitncesCentri.Add(fitncesCentar);

                    sortirani = fitncesCentri;
                    sortirani = sortirani.OrderBy(o => o.NazivFC).ToList();
                }
            }
            return sortirani;
        }

        public static List<Korisnik> UciatajKorisnika()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            string line = "";
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath(KPutanja)))
            {
                while ((line = sr.ReadLine()) != null)
                {

                    string[] tokens = line.Split(';');
                    Korisnik korisnik = new Korisnik()
                    {
                            KorisnickoIme = tokens[0],
                            Lozinka = tokens[1],
                            Ime = tokens[2],
                            Prezime = tokens[3],
                            Pol = (Pol)Enum.Parse(typeof(Pol), tokens[4]),
                            Email = tokens[5],
                            Uloga = (UlogaKorisnika)Enum.Parse(typeof(UlogaKorisnika), tokens[6]),
                            DatumRodjenja = DateTime.Parse(tokens[7]),
                            ListaTreninga = tokens[8].Split(',').ToList(),
                           FitnesCentar = tokens[9].Split(',').ToList(),

                    };
                        korisnici.Add(korisnik);                    
                }
            }
            return korisnici;
        }


        //za registrovanje dodajem novi
        public static void SacuvajKorisnika(Korisnik korisnik)    
        {            
            //sacuvaj korisnika u korisnici.txt
            using (StreamWriter sw = new StreamWriter(HostingEnvironment.MapPath(KPutanja), true))
            {
                foreach (string s in korisnik.ListaTreninga)
                {
                    string something = string.Join(",", korisnik.ListaTreninga);


                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};{8};",
                    korisnik.KorisnickoIme, korisnik.Lozinka, korisnik.Ime, korisnik.Prezime, korisnik.Pol,
                    korisnik.Email, korisnik.Uloga, korisnik.DatumRodjenja, something
                     );
                }
            }
        }

        public static void SacuvajGrupniTreningJedan(GrupniTrening gt)
        {
            using (StreamWriter sw = new StreamWriter(HostingEnvironment.MapPath(GtPutanja), true))
            {
                string something;
                foreach(string s in gt.Posetioci)
                {
                     something = string.Join(",", gt.Posetioci);

                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};",
                       gt.NazivGT, gt.Tip, gt.Trajanje, gt.MaxPosetilaca, gt.Fc.NazivFC, gt.Termin, something, gt.IsDeleted


                       );
                }
                   
            }
        }

        public static void SacuvajFitnesCentar(List<FitnesCentar> fitnesCentri)
        {
            
            using (StreamWriter sw = new StreamWriter(HostingEnvironment.MapPath(FcPutanja), false))
            {
                foreach (FitnesCentar fc in fitnesCentri)
                {


                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};",
                        fc.NazivFC,fc.Adresa.Ulica,fc.Adresa.Broj,fc.Adresa.Grad,fc.Adresa.PostanskiBr,
                        fc.GodinaOtvaranja,fc.MesecnaClanarina,fc.GodisnjaClanarina,fc.CenaTreninga,
                        fc.CenaGrupnogTreninga,fc.CenaIndividualnogTreninga,
                        fc.Vlasnik.KorisnickoIme,fc.IsDeleted
    

                        );
                }
            }
        }


        public static void SacuvajGrupniTrening(List<GrupniTrening> grupniTreninzi)
        {
            using (StreamWriter sw = new StreamWriter(HostingEnvironment.MapPath(GtPutanja), false))
            {
               
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    string something = string.Join(",", gt.Posetioci);

                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}",
                        gt.NazivGT, gt.Tip, gt.Trajanje, gt.MaxPosetilaca, gt.Fc.NazivFC, gt.Termin,something
                        ,gt.IsDeleted
                        );
                   
                }
            }
          
        }

        public static void Prelepi(List<Korisnik> korisnici) //append = false
        {           
            using (StreamWriter sw = new StreamWriter(HostingEnvironment.MapPath(KPutanja), false))
            {
               
                foreach (Korisnik korisnik in korisnici)
                {
                    string something = string.Join(",", korisnik.ListaTreninga);
                    string something2 = string.Join(",", korisnik.FitnesCentar);

                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                       korisnik.KorisnickoIme, korisnik.Lozinka, korisnik.Ime, korisnik.Prezime, korisnik.Pol,
                       korisnik.Email, korisnik.Uloga, korisnik.DatumRodjenja , something,something2

                       );                                 
                }
            }
        }

        public static List<GrupniTrening> UcitajGrupniTrening()
        {
            
          List<GrupniTrening> grupniTreninzi = new List<GrupniTrening>();
            string line = "";
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath(GtPutanja)))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(';');
                    string[] tokens2 = line.Split(',');

                    GrupniTrening gt = new GrupniTrening()
                    {
                        NazivGT = tokens[0],
                        Tip = (TipTreninga)Enum.Parse(typeof(TipTreninga), tokens[1]),
                        Trajanje = int.Parse(tokens[2]),
                        MaxPosetilaca = int.Parse(tokens[3]),
                        Fc = new FitnesCentar()
                        {
                            NazivFC = tokens[4]
                        },
                        // Datum i vreme treninga(čuvati u formatu dd / MM / yyyy HH: mm)
                        Termin = DateTime.Parse(DateTime.Parse(tokens[5]).ToString("dd/MM/yyyy")),
                        Posetioci = tokens[6].Split(',').ToList(),
                        IsDeleted = bool.Parse(tokens[7]),
                        
                };
                    grupniTreninzi.Add(gt);

                }
            }
            return grupniTreninzi;
        }

       

    }
}