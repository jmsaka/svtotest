using Produtos.Domain.Entities;

namespace Produtos.Test;

public class ProdutoTests
{
    private readonly List<ProdutoEntity> _produtos;

    public ProdutoTests()
    {
        _produtos = new List<ProdutoEntity>();
    }

    [Fact]
    public void AdicionarProduto_ProdutoValido_Sucesso()
    {
        // Arrange
        var produto = new ProdutoEntity
        {
            Descricao = "Produto Teste",
            ValorVenda = 150.0,
            ValorCompra = 100.0,
            Tipo = ProdutoTipoEntity.Final,
            DataCriacao = new DateTime(2024, 5, 1)
        };

        // Act
        _produtos.Add(produto);

        // Assert
        Assert.Single(_produtos);
        Assert.Equal(produto, _produtos[0]);
    }

    [Fact]
    public void AdicionarProduto_ValorVendaMenorOuIgualValorCompra_ThrowsArgumentException()
    {
        // Arrange
        var produto = new ProdutoEntity
        {
            Descricao = "Produto Teste",
            ValorVenda = 100.0,
            ValorCompra = 100.0,
            Tipo = ProdutoTipoEntity.Intermediario,
            DataCriacao = new DateTime(2024, 5, 1)
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => AdicionarProduto(produto));
        Assert.Equal("O valor de venda não pode ser menor ou igual ao valor de compra.", exception.Message);
    }

    [Fact]
    public void AdicionarProduto_DataCriacaoAnteriorA2024_ThrowsArgumentException()
    {
        // Arrange
        var produto = new ProdutoEntity
        {
            Descricao = "Produto Teste",
            ValorVenda = 150.0,
            ValorCompra = 100.0,
            Tipo = ProdutoTipoEntity.Consumo,
            DataCriacao = new DateTime(2023, 12, 31)
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => AdicionarProduto(produto));
        Assert.Equal("A data de criação não pode ser anterior a 01/01/2024.", exception.Message);
    }

    [Fact]
    public void AdicionarProduto_DescricaoMenorQue5Caracteres_ThrowsArgumentException()
    {
        // Arrange
        var produto = new ProdutoEntity
        {
            Descricao = "Test",
            ValorVenda = 150.0,
            ValorCompra = 100.0,
            Tipo = ProdutoTipoEntity.MateriaPrima,
            DataCriacao = new DateTime(2024, 5, 1)
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => AdicionarProduto(produto));
        Assert.Equal("A descrição deve conter no mínimo 5 caracteres.", exception.Message);
    }

    [Fact]
    public void AdicionarProduto_ValoresNegativos_ThrowsArgumentException()
    {
        // Arrange
        var produto = new ProdutoEntity
        {
            Descricao = "Produto Teste",
            ValorVenda = -150.0,
            ValorCompra = -100.0,
            Tipo = ProdutoTipoEntity.Final,
            DataCriacao = new DateTime(2024, 5, 1)
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => AdicionarProduto(produto));
        Assert.Contains("O valor de venda não pode ser menor ou", exception.Message);
    }

    [Fact]
    public void ObterProdutosFiltrados_FiltraCorretamente()
    {
        // Arrange
        AdicionarProduto(new ProdutoEntity
        {
            Descricao = "Produto A",
            ValorVenda = 150.0,
            ValorCompra = 100.0,
            Tipo = ProdutoTipoEntity.Final,
            DataCriacao = new DateTime(2024, 5, 1) 
        });

        AdicionarProduto(new ProdutoEntity
        {
            Descricao = "Produto B",
            ValorVenda = 200.0,
            ValorCompra = 120.0,
            Tipo = ProdutoTipoEntity.Consumo,
            DataCriacao = new DateTime(2024, 4, 15) 
        });

        AdicionarProduto(new ProdutoEntity
        {
            Descricao = "Produto C",
            ValorVenda = 250.0,
            ValorCompra = 150.0,
            Tipo = ProdutoTipoEntity.Intermediario,
            DataCriacao = new DateTime(2024, 6, 1) 
        });

        // Act
        var produtosFiltrados = ObterProdutosFiltrados();

        // Assert
        Assert.Equal(3, produtosFiltrados.Count); 
        Assert.Contains(produtosFiltrados, p => p.Descricao == "Produto A");
        Assert.Contains(produtosFiltrados, p => p.Descricao == "Produto B");
        Assert.Contains(produtosFiltrados, p => p.Descricao == "Produto C");
    }


    private void AdicionarProduto(ProdutoEntity produto)
    {
        if (produto.ValorVenda <= produto.ValorCompra)
            throw new ArgumentException("O valor de venda não pode ser menor ou igual ao valor de compra.");
        if (produto.DataCriacao < new DateTime(2024, 1, 1))
            throw new ArgumentException("A data de criação não pode ser anterior a 01/01/2024.");
        if (produto.Descricao.Length < 5)
            throw new ArgumentException("A descrição deve conter no mínimo 5 caracteres.");
        if (produto.ValorVenda <= 0 || produto.ValorCompra <= 0)
            throw new ArgumentException("Os valores de compra e venda não podem ser zero ou abaixo de zero.");

        _produtos.Add(produto);
    }

    private List<ProdutoEntity> ObterProdutosFiltrados()
    {
        return _produtos
            .Where(p => p.DataCriacao >= new DateTime(2024, 4, 1) && p.DataCriacao < new DateTime(2024, 7, 1))
            .OrderBy(p => p.Tipo)
            .ToList();
    }
}

