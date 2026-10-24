class ContaPoupanca : Conta
{
    public override string TipoConta => "Conta Poupança";
    public double TaxaRendimentoMensal { get; private set; } // em %
    public int DiaAniversario { get; }

    public ContaPoupanca(string titular, int diaAniversario, double taxaRendimentoMensal) : base(titular)
    {
        this.TaxaRendimentoMensal = taxaRendimentoMensal;
        this.DiaAniversario = diaAniversario;
    }

    public override void FecharMes()
    {
        decimal rendimento = Math.Round(Saldo * (decimal)TaxaRendimentoMensal / 100, 2);
        if (rendimento > 0)
        {
            Depositar(rendimento, "Rendimento mensal");
        }
    }
}