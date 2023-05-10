using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Nancy;
using Newtonsoft.Json;

namespace API
{
    public class NancyControlleur : NancyModule
    {
        public NancyControlleur()
        {
            Get("/disponibilites/{userid}", args =>
            {
                var response = new Response();

                response.ContentType = "json";

                response.Contents = stream =>
                {
                    var writer = new System.IO.StreamWriter(stream);
                    writer.Write("");
                    writer.Flush();
                };
                    
                return response;
            });
            
            Get("/xml/{name}", args =>
            {
                var response = new Response();

                response.ContentType = "application/xml";

                string filePath = $"data/playerData/{args.name}.xml";
                if (File.Exists(filePath))
                {
                    
                    string xmlString = File.ReadAllText(filePath);

                    response.Contents = stream =>
                    {
                        var writer = new System.IO.StreamWriter(stream);
                        writer.Write(xmlString);
                        writer.Flush();
                    };
                    return response;
                }
                else
                {
                    response.ContentType = "string";
                    response.Contents = stream =>
                    {
                        var writer = new System.IO.StreamWriter(stream);
                        writer.Write("no xml found");
                        writer.Flush();
                    };
                    return response;
                }
            });


            Get("/load", args =>
            {
                var response = new Response();

                Console.WriteLine("Traitement de la demande terminé");
                response.ContentType = "json";
                    response.Contents = stream =>
                    {
                        var writer = new System.IO.StreamWriter(stream);
                        var result = new { message = $"Traitement terminé" }; // Crée un objet JSON valide
                        var json = JsonConvert.SerializeObject(result); // Converti l'objet en chaîne JSON
                        writer.Write(json);
                        writer.Flush();
                    };
                
                if (response != null)
                {
                }
                else
                {
                }
                return response;

            });
        }
    }
}
