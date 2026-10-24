class BancoService
{
    private readonly List<Conta> _contas = new List<Conta>();
    public IReadOnlyList<Conta> Contas => _contas.AsReadOnly();

    public void CriarContaCorrente(string titular, decimal limiteChequeEspecial)
    {
        ContaCorrente contaCorrente = new ContaCorrente(titular, limiteChequeEspecial);
        _contas.Add(contaCorrente);
    }

    public void CriarContaSalario(string titular, string empregador, int limiteSaquesMensais)
    {
        ContaSalario contaSalario = new ContaSalario(titular, empregador, limiteSaquesMensais);
        _contas.Add(contaSalario);
    }

    public void CriarContaPoupanca(string titular, int diaAniversario, double taxaRendimentoMensal)
    {
        ContaPoupanca contaPoupanca = new ContaPoupanca(titular, diaAniversario, taxaRendimentoMensal);
        _contas.Add(contaPoupanca);
    }

    public Conta BuscarContaPorId(long id)
    {
        foreach (Conta conta in _contas)
        {
            if (conta.Id == id)
            {
                return conta;
            }
        }
        throw new ContaNaoEncontradaException($"Conta de Id:{id} não encontrada");
    }

    public Conta BuscarContaPorTitular(string titular)
    {
        foreach (Conta conta in _contas)
        {
            if (conta.Titular == titular)
            {
                return conta;
            }
        }
        throw new ContaNaoEncontradaException($"Conta de {titular} não encontrada");
    }

    public void Depositar(long id, decimal valor)
    {
        Conta conta = BuscarContaPorId(id);
        conta.Depositar(valor);
    }

    public void Sacar(long id, decimal valor)
    {
        Conta conta = BuscarContaPorId(id);
        conta.Sacar(valor);
    }

    public void Transferir(long idOrigem, long idDestino, decimal valor)
    {
        Conta contaOrigem = BuscarContaPorId(idOrigem);
        Conta contaDestino = BuscarContaPorId(idDestino);

        contaOrigem.Sacar(valor, "Transferência");
        contaDestino.Depositar(valor, "Transferência");
    }

    public void AplicarFimMes()
    {
        foreach (Conta conta in _contas)
        {
            conta.FecharMes();
        }
    }
}