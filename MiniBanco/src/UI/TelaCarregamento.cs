
using System;
using System.Threading;

public static class TelaCarregamento
{

    public static void Exibir()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine();
        Console.WriteLine("  ╔══════════════════════════════════════╗");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║              MINIBANCO               ║");
        Console.WriteLine("  ║       Seu dinheiro, sua gestão       ║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ╚══════════════════════════════════════╝");

        Console.ResetColor();
        Console.WriteLine("\n  Inicializando o sistema...\n");

        string[] etapas =
        {
        "Inicializando sistema bancário",
        "Conectando aos serviços",
        "Carregando contas",
        "Preparando ambiente"
    };

        const int larguraBarra = 30;
        int totalEtapas = etapas.Length;

        for (int i = 0; i < totalEtapas; i++)
        {
            Console.WriteLine($"  {etapas[i]}");

            for (int progresso = 0; progresso <= larguraBarra; progresso++)
            {
                int percentual = progresso * 100 / larguraBarra;

                Console.Write("\r  [");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(new string('#', progresso));
                Console.ResetColor();
                Console.Write(new string('-', larguraBarra - progresso));
                Console.Write($"] {percentual,3}%");

                Thread.Sleep(15);
            }

            Console.WriteLine("\n");
            Thread.Sleep(500);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  Sistema iniciado com sucesso!");
        Console.ResetColor();

        Thread.Sleep(1800);
        Console.Clear();
    }

}
