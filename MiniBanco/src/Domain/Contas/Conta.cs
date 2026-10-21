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
    // exceptions
    // FecharMes

    public void Depositar(decimal valor, string descricao = "Depósito")
    {
        RegistrarTransacao(TipoTransacao.Deposito, valor, descricao);
        this.Saldo += valor;
    }

    public virtual void Sacar(decimal valor, string descricao = "Saque")
    {
        RegistrarTransacao(TipoTransacao.Saque, valor, descricao);
        this.Saldo -= valor;
    }

    protected void RegistrarTransacao(TipoTransacao tipo, decimal valor, string descricao)
    {
        Transacao transacao = new Transacao(tipo, valor, descricao);
        _extrato.Add(transacao);
    }

}