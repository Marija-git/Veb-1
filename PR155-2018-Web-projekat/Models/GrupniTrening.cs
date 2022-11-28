using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR155_2018_Web_projekat.Models
{
    public enum TipTreninga { Yoga, LesMillsTone, BodyPump }
    public class GrupniTrening
    {
        private string nazivGT;
        private TipTreninga tip;
        private FitnesCentar fc;
        private int trajanje; //u min?
        private DateTime termin; //dd/MM/yyyy HH:mm
        private int maxPosetilaca;
        private List<string> posetioci = new List<string>();
        private bool isDeleted = false;

        public string NazivGT { get => nazivGT; set => nazivGT = value; }
        public TipTreninga Tip { get => tip; set => tip = value; }
        public FitnesCentar Fc { get => fc; set => fc = value; }
        public int Trajanje { get => trajanje; set => trajanje = value; }
        public DateTime Termin { get => termin; set => termin = value; }
        public int MaxPosetilaca { get => maxPosetilaca; set => maxPosetilaca = value; }
        public List<string> Posetioci { get => posetioci; set => posetioci = value; }
        public bool IsDeleted { get => isDeleted; set => isDeleted = value; }

        public GrupniTrening()
        {
            //posetioci = new List<Korisnik>();
           // Posetioci = new List<string>();
        }

    }
}