class ContaSalario : Conta
{
    public string Empregador { get; private set; }
    public int LimiteSaquesMensais { get; private set; }

    public ContaSalario(string titular, string empregador, int limiteSaquesMensais) : base(titular)
    {
        this.Empregador = empregador;
        this.LimiteSaquesMensais = limiteSaquesMensais;
    }

    // TODO override Sacar() e fecharMes
}