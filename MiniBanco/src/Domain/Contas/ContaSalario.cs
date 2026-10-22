class ContaSalario : Conta
{
    public string Empregador { get; private set; }
    public int LimiteSaquesMensais { get; private set; }
    private int _saquesRealizadosNoMes = 0;
    public int SaquesRestantes => Math.Max(0, LimiteSaquesMensais - _saquesRealizadosNoMes);

    public ContaSalario(string titular, string empregador, int limiteSaquesMensais) : base(titular)
    {
        this.Empregador = empregador;
        this.LimiteSaquesMensais = limiteSaquesMensais;
    }

    protected override void ValidarSaque(decimal valor)
    {
        base.ValidarSaque(valor);
        if (_saquesRealizadosNoMes >= LimiteSaquesMensais)
        {
            RegistrarTransacao(TipoTransacao.Tarifa, 5, "Limite de saque atingido");
        }
    }

    public override void FecharMes()
    {
        _saquesRealizadosNoMes = 0;
    }
}