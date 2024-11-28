using CorridaApostasLib;
using System;
using System.Collections.Generic;
using System.Linq;

class Program {
    static void Main() {
        List<Apostador> apostadores = ConfigurarApostadores();
        List<Corredor> corredores = ConfigurarCorredores();

        ReceberApostas(apostadores, corredores.Count);

        IniciarCorrida(corredores);

        int vencedor = ExibirResultadoCorrida(corredores);
        DividirPremio(apostadores, vencedor);
        ExibirSaldoFinal(apostadores);
    }

    static List<Apostador> ConfigurarApostadores() {
        Console.Write("Número de apostadores: ");
        int numApostadores = int.Parse(Console.ReadLine());
        while (numApostadores < 5) {
            Console.WriteLine("É necessário pelo menos 5 apostadores.");
            numApostadores = int.Parse(Console.ReadLine());
        }

        List<Apostador> apostadores = new();
        for (int i = 0; i < numApostadores; i++) {
            Console.Write($"Nome do apostador {i + 1}: ");
            apostadores.Add(new Apostador(Console.ReadLine()));
        }
        return apostadores;
    }

    static List<Corredor> ConfigurarCorredores() {
        Console.Write("Número de corredores: ");
        int numCorredores = int.Parse(Console.ReadLine());
        while (numCorredores < 4) {
            Console.WriteLine("É necessário pelo menos 4 corredores.");
            numCorredores = int.Parse(Console.ReadLine());
        }

        List<Corredor> corredores = new()
        {
            new Corredor("Corredor 1", 0, 70),
            new Corredor("Corredor 2", 30, 50),
            new Corredor("Corredor 3", 20, 40),
            new Corredor("Corredor 4", 10, 60)
        };

        return corredores;
    }

    static void ReceberApostas(List<Apostador> apostadores, int numCorredores) {
        foreach (var apostador in apostadores) {
            Console.Write($"{apostador.Nome}, escolha um corredor (1-{numCorredores}): ");
            int escolha = int.Parse(Console.ReadLine());
            while (escolha < 1 || escolha > numCorredores) {
                Console.WriteLine("Escolha inválida. Tente novamente.");
                escolha = int.Parse(Console.ReadLine());
            }
            apostador.CorredorEscolhido = escolha;
        }
    }

    static void IniciarCorrida(List<Corredor> corredores) {
        bool corridaEmAndamento = true;
        while (corridaEmAndamento) {
            foreach (var corredor in corredores) {
                corredor.Mover();
                if (corredor.DistanciaPercorrida >= 10000) // 100 metros = 10000 cm
                {
                    corridaEmAndamento = false;
                    break;
                }
            }
        }
    }

    static int ExibirResultadoCorrida(List<Corredor> corredores) {
        corredores.Sort((a, b) => b.DistanciaPercorrida.CompareTo(a.DistanciaPercorrida));
        for (int i = 0; i < corredores.Count; i++) {
            Console.WriteLine($"{i + 1}º Lugar: {corredores[i].Nome} ({corredores[i].DistanciaPercorrida / 100} m)");
        }
        return corredores[0].Nome.Last() - '0';
    }

    static void DividirPremio(List<Apostador> apostadores, int vencedor) {
        var ganhadores = apostadores.Where(a => a.CorredorEscolhido == vencedor).ToList();
        if (ganhadores.Count == 0) {
            Console.WriteLine("Nenhum apostador acertou o vencedor.");
            return;
        }

        decimal premio = apostadores.Count * 20.00m / ganhadores.Count;

        foreach (var apostador in ganhadores) {
            apostador.Saldo += premio;
        }
    }

    static void ExibirSaldoFinal(List<Apostador> apostadores) {
        foreach (var apostador in apostadores) {
            Console.WriteLine($"Saldo final de {apostador.Nome}: R$ {apostador.Saldo:F2}");
        }
    }
}
