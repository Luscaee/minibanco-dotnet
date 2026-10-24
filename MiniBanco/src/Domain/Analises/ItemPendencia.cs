public class ItemPendencia
{
    public string Descricao { get; }
    public string Orientacao { get; }

    public ItemPendencia(string descricao, string orientacao)
    {
        this.Descricao = descricao;
        this.Orientacao = orientacao;
    }
}