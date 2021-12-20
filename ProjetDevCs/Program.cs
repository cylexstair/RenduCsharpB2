using System;
using System.Collections.Generic;

namespace ProjetDevCs
{
    public class Program
    {
        public static List<Humain> ToutLeMonde = new();
        public static List<Humain> Donneur = new();
        public static List<Humain> Receveur = new();
        public static List<Humain> PersonnesRembourse = new();
        public static float total = 0;
        public static void Main()
        {
            Menu();
            void Menu()
            {
                Console.WriteLine("Que voulez vous faire ? (0 : Ajouter des personnes, 1 : Calculer les remboursements, 2 : Récap)");
                string rep = Console.ReadLine();
                switch (rep)
                {
                    case "0":
                        Console.WriteLine("Combien de personnes souhaites-tu ajouter ?");
                        int nbrHumains = 0;
                        try
                        {
                            nbrHumains = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Erreur sur le nombre de personnes rentrée");
                            Menu();
                        }
                        string nom;
                        for (int i = 1; i <= nbrHumains; i++)
                        {
                            Console.WriteLine($"Comment s'appelle notre nouveau venu n°{i} ?");
                            nom = Console.ReadLine();
                            Console.WriteLine($"Combien {nom} as-t-il donner ?");
                            float argent = new();
                            try
                            {
                                argent = Arrondi(float.Parse(Console.ReadLine()));
                            }
                            catch
                            {
                                Console.WriteLine("Erreur sur le nombre rentrée");
                                Menu();
                            }
                            if (argent >= 0 && argent != float.NaN)
                            {
                                AjouterPersonne(nom, argent);
                            }
                            else
                            {
                                Console.WriteLine("Une personne ne peut pas donner une somme négative");
                                Console.WriteLine($"Combien {nom} as-t-il donner ?");
                                i--;
                            }

                        }

                        Menu();
                        break;
                    case "1":
                        Calculer();
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        float totalCaisse = 0;
                        foreach (var H in ToutLeMonde)
                        {
                            Console.WriteLine($"{H.Nom} : {H.Argent}");
                            totalCaisse += H.Argent;
                        }
                        Console.WriteLine($"Total : {totalCaisse}");
                        Menu();
                        break;
                    default:
                        break;
                }
            }
        }
        public static void AjouterPersonne(string nom, float argent)
            {
                var nouveau = new Humain(nom, argent);
                ToutLeMonde.Add(nouveau);
                total += nouveau.Argent;
                Console.WriteLine($"{nouveau.Nom} a rejoint le groupe et a payé : {nouveau.Argent}");
            }
        public static void Calculer()
            {
                float moyenne = total / ToutLeMonde.Count;
                Console.WriteLine($"moyenne : {moyenne}");
                Console.WriteLine($"total : {total}");
                foreach (var humain in ToutLeMonde)//affichage dette total de chacun
                {
                    humain.aRembourse = Arrondi(-(humain.Argent - moyenne));
                    Console.WriteLine($"humain.Nom devra remboursé un total de : {humain.aRembourse}");
                    if (humain.aRembourse > 0)
                    {
                        Donneur.Add(humain);
                    }
                    else { Receveur.Add(humain); }
                }
                float TotalDon = 0;
                float TotalRecu = 0;
                foreach (var donneur in Donneur)
                {
                    TotalDon += donneur.aRembourse;
                }
                foreach (var receveur in Receveur)
                {
                    TotalRecu += receveur.aRembourse;
                }
                Console.WriteLine($"Total a Rembourser : {TotalDon}");
                Console.WriteLine($"Total a Recevoir : {TotalRecu}");
                Console.WriteLine();
                while (ToutLeMonde.Count != PersonnesRembourse.Count)
                {
                    foreach (var donneur in Donneur)
                    {
                        foreach (var receveur in Receveur)
                        {
                            if (receveur.aRembourse < 0 && donneur.aRembourse > 0)
                            {
                                if (donneur.aRembourse > Math.Abs(receveur.aRembourse))
                                {
                                    float transaction = Arrondi(Math.Abs(receveur.aRembourse));
                                    Console.WriteLine($"{donneur.Nom} devra {transaction} a {receveur.Nom}");
                                    donneur.aRembourse -= transaction;
                                    receveur.aRembourse += transaction;
                                }
                                else
                                {
                                    float ecart = donneur.aRembourse;
                                    Console.WriteLine($"{donneur.Nom} devra {ecart} a {receveur.Nom}");
                                    donneur.aRembourse -= ecart;
                                    receveur.aRembourse += ecart;
                                }
                                if (receveur.aRembourse == 0)// ICI
                                {
                                    PersonnesRembourse.Add(receveur);
                                }
                                if (donneur.aRembourse == 0)
                                {
                                    PersonnesRembourse.Add(donneur);
                                }
                            }
                        }
                    }
                }
            }
        public static float Arrondi(float x)
            {
                return float.Parse(String.Format("{0:0.##}", x));
            }
        public class Humain
        {
            public string Nom;
            public float Argent;
            public float aRembourse;
            public Humain(string nom, float argent)
            {
                Nom = nom;
                Argent = argent;
            }
        }
    }
}


