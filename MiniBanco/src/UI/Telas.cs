static class Telas
{
    public static void Cabecalho()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════════════════╗");
        Console.WriteLine("║   MINIBANCO - MOTOR DE GESTÃO DE CONTAS BANCÁRIAS   ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }
    public static void ExibirOpcoes()
    {
        Console.WriteLine("1. Criar conta");
        Console.WriteLine("2. Acessar conta");
        Console.WriteLine("3. Aplicar fim de mês (todas as contas)");
        Console.WriteLine("4. Exibir relatório de contas");
        Console.WriteLine("5. Sair");
        Console.WriteLine();
        Console.Write("Digite:");
    }

    public static void CriarConta(BancoService banco)
    {
        Cabecalho();
        Console.WriteLine("> Tipo da conta:");
        Console.WriteLine();
        Console.WriteLine("1. Conta Corrente");
        Console.WriteLine("2. Conta Poupança");
        Console.WriteLine("3. Conta Salário");
        Console.WriteLine("4. Sair");
        Console.WriteLine();
        Console.Write("Digite:");

        string? opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1": CriarContaCorrente(banco); break;
            case "2": CriarContaPoupanca(banco); break;
            case "3": CriarContaSalario(banco); break;
            case "4": return;
            default: Console.WriteLine("Opção inválida."); break;
        }
    }

    private static void CriarContaCorrente(BancoService banco)
    {
        Cabecalho();
        Console.WriteLine("> Criando Conta Corrente");
        Console.WriteLine();

        string titular = ConsoleHelper.LerTexto("Nome do titular: ");
        decimal limite = ConsoleHelper.LerDecimal("Limite de cheque especial: ");

        banco.CriarContaCorrente(titular, limite);
        Console.WriteLine("Conta Criada!");
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }

    private static void CriarContaPoupanca(BancoService banco)
    {
        Cabecalho();
        Console.WriteLine("> Criando Conta Poupança");
        Console.WriteLine();

        string titular = ConsoleHelper.LerTexto("Nome do titular: ");
        int diaAniversario = ConsoleHelper.LerInt("Dia do Aniversário: ");
        double taxa = ConsoleHelper.LerDouble("Taxa de rendimento: ");

        banco.CriarContaPoupanca(titular, diaAniversario, taxa);
        Console.WriteLine("Conta Criada!");
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }

    private static void CriarContaSalario(BancoService banco)
    {
        Cabecalho();
        Console.WriteLine("> Criando Conta Salário");
        Console.WriteLine();

        string titular = ConsoleHelper.LerTexto("Nome do titular: ");
        string empregador = ConsoleHelper.LerTexto("Empregador: ");
        int limiteSaque = ConsoleHelper.LerInt("Limite de saques mensais: ");

        banco.CriarContaSalario(titular, empregador, limiteSaque);
        Console.WriteLine("Conta Criada!");
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }

    public static void AcessarConta(BancoService banco)
    {
        Cabecalho();
        Console.WriteLine("> Acessando conta");
        Console.WriteLine();

        string titular = ConsoleHelper.LerTexto("Nome do titular: ");

        try
        {
            AcoesConta(banco.BuscarContaPorTitular(titular), banco);
        }
        catch (ContaNaoEncontradaException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void AcoesConta(Conta conta, BancoService banco)
    {
        bool executando = true;
        while (executando)
        {
            Cabecalho();
            Console.WriteLine($"> Conta de {conta.Titular}");
            Console.WriteLine();

            Console.WriteLine("1. Sacar");
            Console.WriteLine("2. Depositar");
            Console.WriteLine("3. Transferir");
            Console.WriteLine("4. Extrato");
            Console.WriteLine("5. Sair");
            Console.WriteLine();
            Console.Write("Digite:");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": Sacar(conta, banco); break;
                case "2": Depositar(conta, banco); break;
                case "3": Transferir(conta, banco); break;
                case "4":
                    Console.WriteLine(RelatorioService.GerarExtrato(conta));
                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    break;
                case "5": executando = false; break;
                default: Console.WriteLine("Opção inválida."); break;
            }

        }
    }

    private static void Sacar(Conta conta, BancoService banco)
    {
        Cabecalho();
        Console.WriteLine($"> Saque");
        Console.WriteLine();

        decimal valorSaque = ConsoleHelper.LerDecimal("Valor: ");
        try
        {
            banco.Sacar(conta.Id, valorSaque);
            Console.WriteLine($"Saque realizado de {valorSaque:C}");
            Console.WriteLine($"Novo saldo: {conta.Saldo:C}");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        catch (ValorInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (SaldoInsuficienteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Depositar(Conta conta, BancoService banco)
    {
        Cabecalho();
        Console.WriteLine($"> Depósito");
        Console.WriteLine();

        decimal valorDeposito = ConsoleHelper.LerDecimal("Valor: ");
        try
        {
            banco.Depositar(conta.Id, valorDeposito);
            Console.WriteLine($"Depósito realizado de {valorDeposito:C}");
            Console.WriteLine($"Novo saldo: {conta.Saldo:C}");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        catch (ValorInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Transferir(Conta conta, BancoService banco)
    {
        Cabecalho();
        Console.WriteLine($"> Transferencia");
        Console.WriteLine();

        string nomeDestino = ConsoleHelper.LerTexto("Nome do titular destinatário: ");
        decimal valorTransferencia = ConsoleHelper.LerDecimal("Valor: ");
        try
        {
            Conta destinatario = banco.BuscarContaPorTitular(nomeDestino);
            banco.Sacar(conta.Id, valorTransferencia);
            banco.Depositar(destinatario.Id, valorTransferencia);

            Console.WriteLine($"Transferência realizada de {valorTransferencia:C} para {destinatario.Titular}");
            Console.WriteLine($"Novo saldo: {conta.Saldo:C}");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        catch (ValorInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (SaldoInsuficienteException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ContaNaoEncontradaException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void AplicarFimMes(BancoService banco)
    {
        Cabecalho();
        Console.WriteLine($"> Aplicando fim de mês");
        Console.WriteLine();

        banco.AplicarFimMes();
        
        Console.Write("Fim de mês aplicado");

        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }
}