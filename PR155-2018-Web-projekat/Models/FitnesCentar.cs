using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PR155_2018_Web_projekat.Models
{
    public class FitnesCentar
    {
        private string nazivFC;
        private Adresa adresa;
        private int godinaOtvaranja;


        private double mesecnaClanarina;
        private double godisnjaClanarina;
        private double cenaTreninga;
        private double cenaGrupnogTreninga;
        private double cenaIndividualnogTreninga;
        private bool isDeleted = false;

        public Korisnik Vlasnik { get; set; }
        

        [Required]
        [DataType(DataType.Text)]
        public string NazivFC { get => nazivFC; set => nazivFC = value; }
        [Required]
        public Adresa Adresa { get => adresa; set => adresa = value; }
        [Required]
        public int GodinaOtvaranja { get => godinaOtvaranja; set => godinaOtvaranja = value; }

        [Required]
        public double MesecnaClanarina { get => mesecnaClanarina; set => mesecnaClanarina = value; }
        public double GodisnjaClanarina { get => godisnjaClanarina; set => godisnjaClanarina = value; }
        public double CenaTreninga { get => cenaTreninga; set => cenaTreninga = value; }
        public double CenaGrupnogTreninga { get => cenaGrupnogTreninga; set => cenaGrupnogTreninga = value; }
        public double CenaIndividualnogTreninga { get => cenaIndividualnogTreninga; set => cenaIndividualnogTreninga = value; }
        public bool IsDeleted { get => isDeleted; set => isDeleted = value; }

        public FitnesCentar()
        {

        }
    }
}