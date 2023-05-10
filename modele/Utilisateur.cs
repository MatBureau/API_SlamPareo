using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;


namespace API_SlamPareo.modele
{
    public class Utilisateur
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public List<TimeInterval> TranchesDispo { get; set; }

        public Utilisateur(int pID, string pNom, string pEmail, List<TimeInterval> pTranches) 
        {
            ID= pID;
            Nom= pNom;
            Email= pEmail;
            TranchesDispo= pTranches;
        }
    }
}
