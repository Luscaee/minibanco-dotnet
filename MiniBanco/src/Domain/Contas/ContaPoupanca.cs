class ContaPoupanca : Conta
{
    public double TaxaRendimentoMensal { get; private set; } // em %
    public int DiaAniversario { get; }

    public ContaPoupanca(string titular, int diaAniversario, double taxaRendimentoMensal) : base(titular)
    {
        this.TaxaRendimentoMensal = taxaRendimentoMensal;
        this.DiaAniversario = diaAniversario;
    }

    // TODO override FecharMes()
}