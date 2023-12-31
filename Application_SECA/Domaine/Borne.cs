﻿namespace Seca.WebAPI.Models.Domaine
{
    public enum EtatBorne { Connected,Disconnected}
    public class Borne
    {
        public Borne(int id, string? ip, int port, EtatBorne etat)
        {
            Id = id;
            Ip = ip;
            Port = port;
            Etat = etat;
            
        }

        public int Id { get; set; }
        public string? Ip { get; set; }
        public int Port { get; set; }
        public EtatBorne Etat { get; set; }
        public int? IdProp { get; set; }
        public override string ToString()
        {
            return $"Borne ID: {Id}, Ip: {Ip}, Port: {Port},Etat:{Etat},Id Proprietaire:{IdProp}";
        }
    }
}
