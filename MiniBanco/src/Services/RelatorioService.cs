using System.Text;

static class RelatorioService
{
    public static string GerarRelatorio(IReadOnlyList<Conta> contas)
    {
        StringBuilder sb = new StringBuilder();

        if (contas.Count == 0)
        {
            sb.AppendLine("Nenhuma conta existente!");
            return sb.ToString();            
        }

        sb.AppendLine(new string('=', 68));
        sb.AppendLine("        MINIBANCO - MOTOR DE GESTÃO DE CONTAS BANCÁRIAS");
        sb.AppendLine(new string('=', 68));
        sb.AppendLine();

        int contaAtual = 1;
        foreach (Conta conta in contas)
        {
            int contaMax = contas.Count;

            sb.AppendLine($"[{contaAtual}/{contaMax}] PROCESSANDO CONTA");
            sb.AppendLine(new string('-', 68));
            sb.AppendLine();
            sb.AppendLine("> DADOS DA CONTA:");
            AdicionaDadosConta(sb, conta);
            sb.AppendLine();

            sb.AppendLine("> MOVIMENTAÇÕES REGISTRADAS:");
            AdicionaMovimentacoes(sb, conta);
            sb.AppendLine();

            sb.AppendLine("> RESUMO DA PONTUAÇÃO:");
            ResultadoAnalise pontuacaoConta = AnaliseSaudeService.Analisar(conta);
            AdicionaPontuacao(sb, pontuacaoConta);
            sb.AppendLine();

            sb.AppendLine("> RELATÓRIO DE PENDÊNCIAS E RECOMENDAÇÕES DA GERÊNCIA:");
            AdicionaPendencias(sb, pontuacaoConta);
            sb.AppendLine();
            sb.AppendLine(new string('-', 68));

            contaAtual++;
        }

        sb.AppendLine(new string('=', 68));
        sb.AppendLine("        FIM DO PROCESSAMENTO DE CONTAS");
        sb.AppendLine(new string('=', 68));

        return sb.ToString();
    }

    public static string GerarExtrato(Conta conta)
    {
        Console.Clear();
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("╔═════════════════════════════════════════════════════╗");
        sb.AppendLine("║   MINIBANCO - MOTOR DE GESTÃO DE CONTAS BANCÁRIAS   ║");
        sb.AppendLine("╚═════════════════════════════════════════════════════╝");
        sb.AppendLine();

        sb.AppendLine($">>> EXTRATO DE {conta.Titular} <<<");
        sb.AppendLine(new string('-', 68));
        sb.AppendLine("> DADOS DA CONTA:");
        AdicionaDadosConta(sb, conta);
        sb.AppendLine();

        sb.AppendLine("> MOVIMENTAÇÕES REGISTRADAS:");
        AdicionaMovimentacoes(sb, conta);
        sb.AppendLine();

        sb.AppendLine("> RESUMO DA PONTUAÇÃO:");
        ResultadoAnalise pontuacaoConta = AnaliseSaudeService.Analisar(conta);
        AdicionaPontuacao(sb, pontuacaoConta);
        sb.AppendLine();

        sb.AppendLine("> RELATÓRIO DE PENDÊNCIAS E RECOMENDAÇÕES DA GERÊNCIA:");
        AdicionaPendencias(sb, pontuacaoConta);
        sb.AppendLine(new string('-', 68));

        return sb.ToString();
    }

    private static void AdicionaDadosConta(StringBuilder sb, Conta conta)
    {
        sb.AppendLine($" - Tipo: {conta.TipoConta}");
        sb.AppendLine($" - Titular: {conta.Titular} | Conta: {conta.Id}");
        sb.AppendLine($" - Atributo específico: {conta.AtributoEspecifico}");
        sb.AppendLine($" - Saldo atual: {conta.Saldo:C}");
    }

    private static void AdicionaMovimentacoes(StringBuilder sb, Conta conta)
    {
        foreach (Transacao transacao in conta.Extrato)
        {
            string marcador;
            switch (transacao.Tipo)
            {
                case TipoTransacao.Deposito:
                    marcador = "[ ✓ ]";
                    break;
                case TipoTransacao.Saque:
                    marcador = "[ ! ]";
                    break;
                case TipoTransacao.Tarifa:
                    marcador = "[ X ]";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transacao.Tipo));
            }

            sb.Append($"{marcador} {transacao.Descricao.PadRight(30, '-')} | ");
            sb.AppendLine($"{transacao.Tipo}: {transacao.Valor:C} ({AnaliseSaudeService.CalcularPontos(transacao.Tipo)} pts)");
        }
    }

    private static void AdicionaPontuacao(StringBuilder sb, ResultadoAnalise resultado)
    {
        sb.AppendLine($" - Pontuação atingida: {resultado.PontuacaoAtingida} de {resultado.PontuacaoMaxima} pontos possíveis");
        sb.AppendLine($" - Índice de saúde: {resultado.IndiceSaude}%");
        sb.Append(" - Classificação final: ");
        ConverterClassificacao(sb, resultado.Classificacao);
    }

    private static void ConverterClassificacao(StringBuilder sb, ClassificacaoConta classificacao)
    {
        switch (classificacao)
        {
            case ClassificacaoConta.Saudavel:
                sb.AppendLine("[ CONTA SAUDÁVEL ]");
                break;
            case ClassificacaoConta.Atencao:
                sb.AppendLine("[ CONTA EM ATENÇÃO ]");
                break;
            case ClassificacaoConta.Risco:
                sb.AppendLine("[ CONTA EM RISCO ]");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(classificacao));
        }
    }

    private static void AdicionaPendencias(StringBuilder sb, ResultadoAnalise resultado)
    {
        if (resultado.ItensCriticos.Count == 0 && resultado.ItensAtencao.Count == 0)
        {
            sb.AppendLine("🟢 Nenhuma pendência identificada. Oferecer produtos de investimento ao cliente.");
            return;
        }

        if (resultado.ItensCriticos.Count > 0)
        {
            sb.AppendLine("🔴 ITENS CRÍTICOS (TARIFAS):");
            foreach (ItemPendencia item in resultado.ItensCriticos)
            {
                sb.AppendLine($"{item.Descricao}: {item.Orientacao}");
            }
            sb.AppendLine();
        }

        if (resultado.ItensAtencao.Count > 0)
        {
            sb.AppendLine("🟡 ITENS DE ATENÇÃO (SAQUES):");
            foreach (ItemPendencia item in resultado.ItensAtencao)
            {
                sb.AppendLine($"{item.Descricao}: {item.Orientacao}");
            }
        }
    }
}