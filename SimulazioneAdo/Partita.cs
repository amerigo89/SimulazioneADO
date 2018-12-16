using System;
using System.Collections.Generic;
using System.Text;

namespace SimulazioneAdo
{
    class Partita
    {
        public int Id { get; }
        public string Tipo { get; set; }
        public int Campo { get; set; }
        public DateTime OraInizio { get; set; }
        public DateTime OraFine { get; set; }
        public string Risultato { get; set; }

        public Partita(string tipo, int campo, DateTime inizio, DateTime fine, string risultato)
        {
            Tipo = tipo;
            Campo = campo;
            OraInizio = inizio;
            OraFine = fine;
            Risultato = risultato;
        }
    }
}
