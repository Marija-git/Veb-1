using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR155_2018_Web_projekat.Models
{
    public class Komentar
    {
        public Korisnik Korisnik { get; set; }
        public FitnesCentar FitnesCentar { get; set; }
        public string Text { get; set; }
        public string Ocena { get; set; }
    }
}