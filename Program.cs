using System;
using System.Collections.Generic;
using System.Threading;
using API;
using Nancy.Hosting.Self;

class Program
{
    static void Main()
    {
        var uri = new Uri($"http://127.0.0.1:9000"); //{SettingsManager.ServerSettings.GetPort()}
        var host = new NancyHost(uri, new CustomBootstrapper());
        host.Start();

        Console.WriteLine("En attente de requête");
        while (true)
        {
            try
            {
                // Wait for the host to shutdown
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                host.Stop();
                break;
            }
        }

        // Example usage de l'arbre d'intervalle
        var availabilities = new List<List<TimeInterval>>
        {
            new List<TimeInterval>
            {
                new TimeInterval { Start = DateTime.Parse("2023-05-03T09:00:00"), End = DateTime.Parse("2023-05-03T12:00:00") },
                new TimeInterval { Start = DateTime.Parse("2023-05-03T14:00:00"), End = DateTime.Parse("2023-05-03T18:00:00") },
            },
            new List<TimeInterval>
            {
                new TimeInterval { Start = DateTime.Parse("2023-05-03T10:00:00"), End = DateTime.Parse("2023-05-03T11:30:00") },
                new TimeInterval { Start = DateTime.Parse("2023-05-03T15:00:00"), End = DateTime.Parse("2023-05-03T17:00:00") },
            },
            // ici suivre ce modèle pour ajouter des disponibilités en plus (idéalement il faudra les récupérer sur la base)
        };

        var commonAvailability = CommonAvailabilityFinder.FindCommonAvailability(availabilities);

        Console.WriteLine("Common availability intervals:");
        foreach (var interval in commonAvailability)
        {
            Console.WriteLine($"{interval.Start} - {interval.End}");
        }
    }
}
