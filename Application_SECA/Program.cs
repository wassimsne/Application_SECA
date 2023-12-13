using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Seca.WebAPI.Models.Domaine;

class Program
{
    static string apiUrl = "http://localhost:5258/Borne"; // Remplacez ceci par l'URL de votre API

    static async Task Main(string[] args)
    {
        bool continuer = true;

        while (continuer)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Créer une nouvelle borne");
            Console.WriteLine("2. Afficher les détails d'une borne");
            Console.WriteLine("3. Mettre à jour les détails d'une borne");
            Console.WriteLine("4. Supprimer une borne");
            Console.WriteLine("5. Créer un nouveau proprietaire");
            Console.WriteLine("6. Afficher les détails du proprietaire");
            Console.WriteLine("7. Mettre à jour les détails du proprietaire");
            Console.WriteLine("8. Supprimer un proprietaire");
            Console.WriteLine("5. Quitter");

            Console.Write("Entrez votre choix: ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    await CreerBorne();
                    break;
                case "2":
                    await AfficherBorne();
                    break;
                case "3":
                   // await MettreAJourBorne();
                    break;
                case "4":
                   // await SupprimerBorne();
                    break;
                case "5":
                    continuer = false;
                    Console.WriteLine("Au revoir!");
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
    }

    static async Task CreerBorne()
    {
        Console.Write("Entrez l'ID de la borne: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Entrez Ip de la borne: ");
        string Ip = Console.ReadLine();

        Console.Write("Entrez le port de la borne: ");
        int port = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Choisissez l'état de la borne : ");
        foreach (EtatBorne etat in Enum.GetValues(typeof(EtatBorne)))
        {
            Console.WriteLine($"{(int)etat}. {etat}");
        }

        EtatBorne etatBorne=new EtatBorne();
        bool isValidInput = false;

        do
        {
            Console.Write("Entrez le numéro correspondant à l'état de la borne : ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int etatNumber) && Enum.IsDefined(typeof(EtatBorne), etatNumber))
            {
                etatBorne = (EtatBorne)etatNumber;
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Veuillez entrer un numéro valide correspondant à l'état de la borne.");
            }
        } while (!isValidInput);

        


        Borne nouvelleBorne = new Borne(id,Ip, port, etatBorne);
        await PostBorne(nouvelleBorne);

        Console.WriteLine("Borne ajoutée avec succès!");
    }

    static async Task AfficherBorne()
    {
        Console.Write("Entrez l'ID de la borne à afficher: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Borne borne = await GetBorne(id);

        if (borne != null)
            Console.WriteLine(borne);
        else
            Console.WriteLine($"Borne avec l'ID {id} n'a pas été trouvée.");
    }

   /* static async Task MettreAJourBorne()
    {
        Console.Write("Entrez l'ID de la borne à mettre à jour: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Borne borne = await GetBorne(id);

        if (borne != null)
        {
            Console.WriteLine("Modifier les détails de la borne:");
            Console.Write("Nouveau nom de la borne (Appuyez sur Entrée pour ne pas changer): ");
            string nom = Console.ReadLine();

            Console.Write("Nouveau type de la borne (Appuyez sur Entrée pour ne pas changer): ");
            string typeBorne = Console.ReadLine();

            if (!string.IsNullOrEmpty(nom))
                borne.Nom = nom;

            if (!string.IsNullOrEmpty(typeBorne))
                borne.TypeBorne = typeBorne;

            await PutBorne(id, borne);

            Console.WriteLine("Détails de la borne mis à jour avec succès!");
        }
        else
        {
            Console.WriteLine($"Borne avec l'ID {id} n'a pas été trouvée.");
        }
    }*/

    static async Task SupprimerBorne()
    {
        Console.Write("Entrez l'ID de la borne à supprimer: ");
        int id = Convert.ToInt32(Console.ReadLine());

        await DeleteBorne(id);

        Console.WriteLine($"Borne avec l'ID {id} a été supprimée.");
    }

    static async Task<Borne> GetBorne(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                Borne borne = JsonConvert.DeserializeObject<Borne>(jsonResult);
                return borne;
            }
            return null;
        }
    }

    static async Task PostBorne(Borne nouvelleBorne)
    {
        using (HttpClient client = new HttpClient())
        {
            string jsonBorne = JsonConvert.SerializeObject(nouvelleBorne);
            HttpContent content = new StringContent(jsonBorne, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erreur : {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }

    static async Task PutBorne(int id, Borne borne)
    {
        using (HttpClient client = new HttpClient())
        {
            string jsonBorne = JsonConvert.SerializeObject(borne);
            HttpContent content = new StringContent(jsonBorne, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"{apiUrl}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erreur : {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }

    static async Task DeleteBorne(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erreur : {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}