namespace Produtos.Domain.Entities;
public class ProdutoEntity
{
    public string? Descricao { get; set; }
    public double ValorVenda { get; set; }
    public double ValorCompra { get; set; }
    public ProdutoTipoEntity Tipo { get; set; }
    public DateTime DataCriacao { get; set; }

    public double MargemLucro => ValorVenda - ValorCompra;
}
