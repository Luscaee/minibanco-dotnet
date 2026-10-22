class ContaCorrente : Conta
{
    public decimal LimiteChequeEspecial { get; private set;}
    private const decimal TarifaManutencao = 10m;

    public ContaCorrente(string titular, decimal limiteChequeEspecial) : base(titular)
    {
        this.LimiteChequeEspecial = limiteChequeEspecial;
    }

    protected override void ValidarSaque(decimal valor)
    {
        if (Saldo + LimiteChequeEspecial < valor)
        {
            throw new SaldoInsuficienteException("Saldo insuficiente.");
        }
    }

    public override void FecharMes()
    {
        RegistrarTransacao(TipoTransacao.Tarifa, TarifaManutencao, "Tarifa de manutenção");
        Saldo -= TarifaManutencao;
    }
}