class MenuPrincipal
{
    private readonly BancoService _banco;

    public MenuPrincipal(BancoService banco)
    {
        _banco = banco;
    }

    public void Executar()
    {
        bool executando = true;

        while (executando)
        {
            Telas.Cabecalho();

            Telas.ExibirOpcoes();
            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": Telas.CriarConta(_banco); break;
                case "2": Telas.AcessarConta(_banco); break;
                case "3": Telas.AplicarFimMes(_banco); break;
                case "4":
                    Console.Clear();
                    Console.WriteLine(RelatorioService.GerarRelatorio(_banco.Contas));
                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    break;
                case "5": executando = false; break;
                default:
                    Console.WriteLine("Opção inválida.");
                    Telas.PressioneContinuar();
                    break;
            }
        }
    }
}