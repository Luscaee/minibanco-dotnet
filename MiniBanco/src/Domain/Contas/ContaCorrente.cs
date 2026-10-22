class ContaCorrente : Conta
{
    public decimal LimiteChequeEspecial { get; private set;}

    public ContaCorrente(string titular, decimal limiteChequeEspecial) : base(titular)
    {
        this.LimiteChequeEspecial = limiteChequeEspecial;
    }

    // TODO override FecharMes()
    protected override void ValidarSaque(decimal valor)
    {
        if (Saldo + LimiteChequeEspecial < valor)
        {
            throw new SaldoInsuficienteException("Saldo insuficiente.");
        }
    }
}