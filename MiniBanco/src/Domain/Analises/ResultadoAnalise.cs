public class ResultadoAnalise
{
    public int PontuacaoAtingida { get; }
    public int PontuacaoMaxima { get; }
    public decimal IndiceSaude { get; }
    public ClassificacaoConta Classificacao { get; }
    public IReadOnlyList<ItemPendencia> ItensCriticos { get; }
    public IReadOnlyList<ItemPendencia> ItensAtencao { get; }

    public ResultadoAnalise(
        int pontuacaoAtingida, 
        int pontuacaoMaxima, 
        decimal indiceSaude,
        ClassificacaoConta classificacao,
        List<ItemPendencia> itensCriticos,
        List<ItemPendencia> itensAtencao)
    {
        this.PontuacaoAtingida = pontuacaoAtingida;
        this.PontuacaoMaxima = pontuacaoMaxima;
        this.IndiceSaude = indiceSaude;
        this.Classificacao = classificacao;
        this.ItensCriticos = itensCriticos.AsReadOnly();
        this.ItensAtencao = itensAtencao.AsReadOnly();
    }
}