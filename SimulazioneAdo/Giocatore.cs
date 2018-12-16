using System;
using System.Collections.Generic;
using System.Text;

namespace SimulazioneAdo
{
    class Giocatore
    {
        public int Id { get; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public string Nickname { get; set; }
        public int Livello { get; set; }

        public Giocatore(string nome,string cognome,DateTime dataNascita,string nickname,int livello)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita.Date;
            Nickname = nickname;
            Livello = livello;
        }
    }
}
