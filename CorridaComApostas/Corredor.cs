using System;

namespace CorridaApostasLib {
    public class Corredor {
        public string Nome { get; set; }
        public int DistanciaPercorrida { get; set; }
        private readonly Random random = new Random();
        private readonly int minMovimento;
        private readonly int maxMovimento;

        public Corredor(string nome, int min, int max) {
            Nome = nome;
            minMovimento = min;
            maxMovimento = max;
            DistanciaPercorrida = 0;
        }

        public void Mover() {
            DistanciaPercorrida += random.Next(minMovimento, maxMovimento + 1);
        }
    }
}
