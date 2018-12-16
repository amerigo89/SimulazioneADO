using System;
using System.Collections.Generic;

namespace SimulazioneAdo
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller ctrl = new Controller();

            string choice = "";
            do
            {
                Console.WriteLine("Seleziona un'azione da eseguire: \n" +
                    "1 -> Crea nuova partita \n" +
                    "2 -> Crea nuovo giocatore \n" +
                    "3 -> Cerca le partite per data \n" +
                    "4 -> Calcola livello medio per età \n" +
                    "5 -> Mostra tutti i giocatori \n" +
                    "6 -> Mostra tutte le partite \n" +
                    "exit -> Close \n");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            string tipo;
                            do
                            {
                                Console.WriteLine("Inserisci il tipo di partita:");
                                tipo = Console.ReadLine();
                            }
                            while (tipo != "tennis" && tipo != "paddle");

                            List<int> giocatoriId = new List<int>();
                            int id = 0;

                            if (tipo.Equals("tennis"))
                            {
                                for (int i=0; i<2; i++)
                                {
                                    do
                                    {
                                        Console.WriteLine("Inserisci il nome del giocatore " + i);
                                        id = ctrl.CercaGiocatorePerNome(Console.ReadLine());
                                    }
                                    while (id == 0);

                                    giocatoriId.Add(id);
                                }
                            }
                            else if (tipo.Equals("paddle"))
                            {          
                                for (int i = 0; i < 4; i++)
                                {
                                    do
                                    {
                                        Console.WriteLine("Inserisci il nome del giocatore " + i);
                                        id = ctrl.CercaGiocatorePerNome(Console.ReadLine());
                                    }
                                    while (id == 0);
                                    
                                    giocatoriId.Add(id);
                                }
                            }

                            Console.WriteLine("Inserisci il campo:");
                            int campo = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Inserisci la data e ora di inizio partita:");
                            DateTime inizio = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Inserisci la data e ora di fine partita:");
                            DateTime fine = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Inserisci il risultato:");
                            string risultato = Console.ReadLine();

                            Partita partita = new Partita(tipo, campo, inizio, fine, risultato);
                            ctrl.CreaPartita(partita, giocatoriId);

                            break;       
                        }
                    case "2":
                        {
                            Console.WriteLine("Inserisci il nome:");
                            string nome = Console.ReadLine();
                            Console.WriteLine("Inserisci il cognome:");
                            string cognome = Console.ReadLine();
                            Console.WriteLine("Inserisci la data di nascita:");
                            DateTime dataNascita = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Inserisci il nickname:");
                            string nickname = Console.ReadLine();
                            Console.WriteLine("Inserisci il livello:");
                            int livello = Convert.ToInt32(Console.ReadLine());

                            Giocatore giocatore = new Giocatore(nome, cognome, dataNascita, nickname, livello);
                            ctrl.CreaGiocatore(giocatore);

                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Inserisci la data: \n");
                            DateTime data = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Partite trovate:");

                            foreach(var p in ctrl.CercaPartitePerData(data))
                            {
                                Console.WriteLine("Tipo: " + p.Tipo);
                                Console.WriteLine("Campo " + p.Campo);
                                Console.WriteLine("Ora di inizio: " + p.OraInizio);
                                Console.WriteLine("Ora di fine: " + p.OraFine);
                                Console.WriteLine("Risultato: " + p.Risultato);
                                Console.WriteLine(" ##################### \n");
                            }

                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Inserisci l'età limite: ");
                            int eta = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Il livello medio dei giocatori con età minore di " + eta + " anni è " +
                            ctrl.LivelloMedio(eta));

                            break;
                        }
                    case "5":
                        {
                            foreach(var g in ctrl.MostraGiocatori())
                            {
                                Console.WriteLine("Nome: " + g.Nome);
                                Console.WriteLine("Cognome: " + g.Cognome);
                                Console.WriteLine("Data di nascita: " + g.DataNascita.Date);
                                Console.WriteLine("Nickname: " + g.Nickname);
                                Console.WriteLine("Livello: " + g.Livello);
                                Console.WriteLine(" ##################### \n");
                            }

                            break;
                        }
                    case "6":
                        {
                            foreach(var p in ctrl.MostraPartite())
                            {
                                Console.WriteLine("Tipo: " + p.Tipo);
                                Console.WriteLine("Campo " + p.Campo);
                                Console.WriteLine("Ora di inizio: " + p.OraInizio);
                                Console.WriteLine("Ora di fine: " + p.OraFine);
                                Console.WriteLine("Risultato: " + p.Risultato);
                                Console.WriteLine(" ##################### \n");
                            }
                            break;
                        }
                }

                if (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice !="6" && choice != "exit")
                    Console.WriteLine("Please write a correct choice \n");
            }
            
            while (choice != "exit") ;
        }
    }
}
