using API;
using API_SlamPareo.modele;
using DLL_DAO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API_SlamPareo.DAO
{
    public static class DAO_Utilisateurs
    {
        /// <summary>
        /// Requete select sans condition
        /// </summary>
        /// <returns></returns>
        public static List<Utilisateur> requete_select(int user_id)
        {
            DAO_BDD.Connecter();
            DAO_BDD connexion = new DAO_BDD();
            List<Utilisateur> list = new List<Utilisateur>();
            try
            {
                using (var conn = connexion.Connexion)

                {
                    Console.Out.WriteLine("Opening connection");

                    using (var command = new NpgsqlCommand($"SELECT u.id AS utilisateur_id, u.nom AS utilisateur_nom, u.email AS utilisateur_email, d.id AS disponibilite_id, th.id AS tranche_horaire_id, th.heure_debut AS tranche_horaire_debut, th.heure_fin AS tranche_horaire_fin FROM utilisateurs u INNER JOIN disponibilites d ON u.id = d.utilisateur_id INNER JOIN tranches_horaires th ON d.tranche_horaire_id = th.id WHERE u.id = {user_id};", conn))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            List<TimeInterval> timeIntervals = new List<TimeInterval>();
                            Utilisateur user = new Utilisateur(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),timeIntervals);
                            list.Add(user);
                        }
                    }
                }
                DAO_BDD.CloseConn();
                return list;
            }
            catch (Exception ex)
            {
                DAO_BDD.CloseConn();
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
