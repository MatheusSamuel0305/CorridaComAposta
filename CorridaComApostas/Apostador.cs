namespace CorridaApostasLib {
    public class Apostador {
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public int CorredorEscolhido { get; set; }

        public Apostador(string nome) {
            Nome = nome;
            Saldo = 20.00m;
        }
    }
}
