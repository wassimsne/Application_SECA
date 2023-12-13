namespace Seca.WebAPI.Models.Domaine
{
    public class Proprietaire
    {
        public int IdProprietaire{ get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return $"Proprietaire ID: {IdProprietaire}, Nom: {Nom}, Prenom: {Prenom},Login:{Login},Password:{Password}";
        }

    }
}
