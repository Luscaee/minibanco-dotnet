static class AnaliseSaudeService
{
    private const int PontosDeposito = 10;
    private const int PontosSaque = 5;
    private const int PontosTarifa = 0;
    private const int PontosMaximo = 10;

    private static (int Atingida, int Maxima) CalcularPontuacao(Conta conta)
    {
        int atingida = 0;
        int maxima = 0;

        foreach (Transacao transacao in conta.Extrato)
        {
            atingida += CalcularPontos(transacao.Tipo);
            maxima += PontosMaximo;
        }

        return (atingida, maxima);
    }

    public static int CalcularPontos(TipoTransacao tipo)
    {
        switch (tipo)
        {
            case TipoTransacao.Deposito: return PontosDeposito;
            case TipoTransacao.Saque: return PontosSaque;
            case TipoTransacao.Tarifa: return PontosTarifa;
            default: throw new ArgumentOutOfRangeException(nameof(tipo));
        }
    }

    private static decimal CalcularIndice(int atingida, int maxima)
    {
        return Math.Round(maxima == 0 ? 0 : (decimal)atingida / maxima * 100, 2);
    }

    private static ClassificacaoConta Classificar(decimal indice, int maxima)
    {
        if (indice >= 90 || maxima == 0)
        {
            return ClassificacaoConta.Saudavel;
        }
        else if (indice >= 60)
        {
            return ClassificacaoConta.Atencao;
        }
        else
        {
            return ClassificacaoConta.Risco;
        }
    }

    private static List<ItemPendencia> GerarItensCriticos(Conta conta)
    {
        List<ItemPendencia> itensCriticos = new List<ItemPendencia>();

        foreach (Transacao transacao in conta.Extrato)
        {
            if (transacao.Tipo == TipoTransacao.Tarifa)
            {
                itensCriticos.Add(new ItemPendencia(transacao.Descricao, "Avaliar isenção ou migração para pacote de serviços."));
            }
        }
        return itensCriticos;
    }

    private static List<ItemPendencia> GerarItensAtencao(Conta conta)
    {
        List<ItemPendencia> itensAtencao = new List<ItemPendencia>();
        decimal saldoAux = conta.Saldo;

        if (conta.Saldo >= 0)
        {
            return itensAtencao;
        }

        for (int i = conta.Extrato.Count - 1; i >= 0; i--)
        {
            Transacao transacao = conta.Extrato[i];

            if (saldoAux >= 0)
            {
                break;
            }

            if (transacao.Tipo == TipoTransacao.Saque)
            {
                itensAtencao.Add(new ItemPendencia(
                    $"Saque de {transacao.Valor:C}",
                    "Conta usando limite do cheque especial. Entrar em contato com o cliente."));
            }

            if (transacao.Tipo == TipoTransacao.Deposito)
            {
                saldoAux -= transacao.Valor;
            }
            else
            {
                saldoAux += transacao.Valor;
            }
        }
        return itensAtencao;
    }

    public static ResultadoAnalise Analisar(Conta conta)
    {
        var pontuacao = CalcularPontuacao(conta);
        decimal indice = CalcularIndice(pontuacao.Atingida, pontuacao.Maxima);
        ClassificacaoConta classificacao = Classificar(indice, pontuacao.Maxima);

        return new ResultadoAnalise(
            pontuacao.Atingida,
            pontuacao.Maxima,
            indice,
            classificacao,
            GerarItensCriticos(conta),
            GerarItensAtencao(conta));
    }
}