class ContaCorrente : Conta
{
    public decimal LimiteChequeEspecial { get; private set;}

    public ContaCorrente(string titular, decimal limiteChequeEspecial) : base(titular)
    {
        this.LimiteChequeEspecial = limiteChequeEspecial;
    }

    // TODO override Sacar() e FecharMes()
}