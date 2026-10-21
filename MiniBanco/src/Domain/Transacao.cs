public class Transacao
{
    public string Descricao { get; }
    public TipoTransacao Tipo { get; }
    public decimal Valor { get; }

    public Transacao(TipoTransacao tipo, decimal valor, string descricao)
    {
        this.Tipo = tipo;
        this.Descricao = descricao;
        this.Valor = valor;
    }
}