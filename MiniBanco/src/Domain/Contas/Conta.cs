abstract class Conta
{
    public long Id { get; }
    public string Titular { get; }
    public decimal Saldo { get; protected set; }
    private List<Transacao> _extrato = new List<Transacao>();
    public IReadOnlyList<Transacao> Extrato => _extrato.AsReadOnly();
    private static long _geradorId { get; set; } = 1000;


    protected Conta(string titular)
    {
        _geradorId++;
        this.Id = _geradorId;
        this.Titular = titular;
    }

    // TODO
    // FecharMes

    public void Depositar(decimal valor, string descricao = "Depósito")
    {
        if (valor <= 0)
        {
            throw new ValorInvalidoException("Valor deve ser maior que zero(0).");
        }

        RegistrarTransacao(TipoTransacao.Deposito, valor, descricao);
        this.Saldo += valor;
    }

    public void Sacar(decimal valor, string descricao = "Saque")
    {
        if (valor <= 0)
        {
            throw new ValorInvalidoException("Valor deve ser maior que zero(0).");
        }

        ValidarSaque(valor);
        RegistrarTransacao(TipoTransacao.Saque, valor, descricao);
        this.Saldo -= valor;
    }

    protected virtual void ValidarSaque(decimal valor)
    {
        if (this.Saldo < valor)
        {
            throw new SaldoInsuficienteException("Saldo insuficiente.");
        }
    }

    protected void RegistrarTransacao(TipoTransacao tipo, decimal valor, string descricao)
    {
        Transacao transacao = new Transacao(tipo, valor, descricao);
        _extrato.Add(transacao);
    }

}