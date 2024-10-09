namespace Produtos.App.Application;

public static class ProdutosApplication
{
    /// <summary>
    /// Item - 1 - Mostrar Lista de Produtos
    /// </summary>
    /// <param name="mediator"></param>
    /// <returns></returns>
    public static async Task MostrarProdutos(IMediator mediator, string descricao = null)
    {
        Console.Clear();

        // Dispara a query via Mediator para obter os produtos
        var query = new ObterProdutosQuery { Descricao = descricao };
        var produtos = await mediator.Send(query);

        if (produtos.Any())
        {
            foreach (var produto in produtos)
            {
                Console.WriteLine($"Descrição: {produto.Descricao}, " +
                                  $"Valor Venda: {produto.ValorVenda:C}, " +
                                  $"Valor Compra: {produto.ValorCompra:C}, " +
                                  $"Tipo: {produto.Tipo}, " +
                                  $"Data Criação: {produto.DataCriacao:dd/MM/yyyy}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado.");
        }

        Console.ReadKey();
    }

    /// <summary>
    /// Item - 2 - Inserir Novo Produto
    /// </summary>
    public static async Task InserirProduto(IMediator mediator)
    {
        Console.Clear();

        Console.Write("Descrição: ");
        var descricao = Console.ReadLine() ?? string.Empty;

        Console.Write("Valor de Venda: ");
        var valorVenda = Convert.ToDouble(Console.ReadLine());

        Console.Write("Valor de Compra: ");
        var valorCompra = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Selecione o Tipo:");

        foreach (var tipo in Enum.GetValues(typeof(ProdutoTipoEntity)))
        {
            Console.WriteLine($"{(int)tipo} - {tipo}");
        }

        var prodTipo = (ProdutoTipoEntity)Convert.ToInt32(Console.ReadLine());

        var dataCriacao = DateTime.Now;

        var produto = new AdicionarProdutoCommand
        {
            Descricao = descricao ?? string.Empty,
            ValorVenda = valorVenda,
            ValorCompra = valorCompra,
            Tipo = prodTipo,
            DataCriacao = dataCriacao
        };

        try
        {
            await mediator.Send(produto);
            Console.WriteLine("Produto inserido com sucesso!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro: {e.Message}");
        }

        Console.ReadKey();
    }


    /// <summary>
    /// Item - 7 - Carga Inicial de Produtos
    /// </summary>
    /// <param name="mediator"></param>
    /// <returns></returns>
    public static async Task CargaInicialProdutos(IMediator mediator)
    {
        Console.Clear();

        var command = new List<AdicionarProdutoCommand>
        {
            new AdicionarProdutoCommand { Descricao = "Produto Alpha", ValorVenda = 120.00, ValorCompra = 100.00, Tipo = ProdutoTipoEntity.Final, DataCriacao = new DateTime(2024, 2, 10) },
            new AdicionarProdutoCommand { Descricao = "Produto Beta", ValorVenda = 300.00, ValorCompra = 200.00, Tipo = ProdutoTipoEntity.Consumo, DataCriacao = new DateTime(2024, 5, 15) },
            new AdicionarProdutoCommand { Descricao = "Produto Gamma", ValorVenda = 400.00, ValorCompra = 350.00, Tipo = ProdutoTipoEntity.Intermediario, DataCriacao = new DateTime(2024, 3, 5) },
            new AdicionarProdutoCommand { Descricao = "Produto Delta", ValorVenda = 500.00, ValorCompra = 400.00, Tipo = ProdutoTipoEntity.MateriaPrima, DataCriacao = new DateTime(2024, 6, 30) },
            new AdicionarProdutoCommand { Descricao = "Produto Epsilon", ValorVenda = 600.00, ValorCompra = 550.00, Tipo = ProdutoTipoEntity.Final, DataCriacao = new DateTime(2024, 4, 1) },
            new AdicionarProdutoCommand { Descricao = "Produto Zeta", ValorVenda = 700.00, ValorCompra = 650.00, Tipo = ProdutoTipoEntity.Consumo, DataCriacao = new DateTime(2024, 7, 10) },
            new AdicionarProdutoCommand { Descricao = "Produto Eta", ValorVenda = 800.00, ValorCompra = 700.00, Tipo = ProdutoTipoEntity.Intermediario, DataCriacao = new DateTime(2024, 8, 20) },
            new AdicionarProdutoCommand { Descricao = "Produto Theta", ValorVenda = 900.00, ValorCompra = 850.00, Tipo = ProdutoTipoEntity.MateriaPrima, DataCriacao = new DateTime(2024, 9, 5) },
            new AdicionarProdutoCommand { Descricao = "Produto Iota", ValorVenda = 150.00, ValorCompra = 120.00, Tipo = ProdutoTipoEntity.Final, DataCriacao = new DateTime(2024, 1, 20) },
            new AdicionarProdutoCommand { Descricao = "Produto Kappa", ValorVenda = 250.00, ValorCompra = 200.00, Tipo = ProdutoTipoEntity.Consumo, DataCriacao = new DateTime(2024, 3, 12) }
        };

        if (command.Any())
        {
            foreach (var produto in command)
            {
                await mediator.Send(produto);

                Console.WriteLine($"Descrição: {produto.Descricao}, " +
                                  $"Valor Venda: {produto.ValorVenda:C}, " +
                                  $"Valor Compra: {produto.ValorCompra:C}, " +
                                  $"Tipo: {produto.Tipo}, " +
                                  $"Data Criação: {produto.DataCriacao:dd/MM/yyyy}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto cadastrado.");
        }

        Console.ReadKey();
    }

    public static async Task MostrarProdutosPorCriterio(IMediator mediator, int filtro)
    {
        Console.Clear();

        var query = new ObterProdutosQuery();
        var produtos = await mediator.Send(query);

        var produtosFiltrados = new List<ProdutoEntity>();

        switch (filtro)
        {
            case 0:
                produtosFiltrados = produtos
                    .Where(p => p.DataCriacao >= new DateTime(2024, 4, 1) && p.DataCriacao < new DateTime(2024, 7, 1))
                    .ToList();
                break;
            case 1:
                produtosFiltrados = produtos
                    .OrderByDescending(p => p.Tipo)
                    .ToList();
                break;
            case 2:
                produtosFiltrados = produtos
                    .OrderByDescending(p => p.ValorVenda - p.ValorCompra)
                    .Take(3)
                    .ToList();
                break;
            default:
                produtosFiltrados = produtos;
                break;
        }

        if (produtosFiltrados.Any())
        {
            foreach (var produto in produtosFiltrados)
            {
                Console.WriteLine($"Descrição: {produto.Descricao}, " +
                                  $"Valor Venda: {produto.ValorVenda:C}, " +
                                  $"Valor Compra: {produto.ValorCompra:C}, " +
                                  $"Tipo: {produto.Tipo}, " +
                                  $"Data Criação: {produto.DataCriacao:dd/MM/yyyy}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado.");
        }

        Console.ReadKey();
    }
}
