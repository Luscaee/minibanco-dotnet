public class Transacao
{
    public string Descricao { get; }
    public TipoTransacao Tipo { get; }
    public decimal Valor { get; }

    public Transacao(string descricao, TipoTransacao tipo, decimal valor)
    {
        this.Descricao = descricao;
        this.Tipo = tipo;
        this.Valor = valor;
    }
}