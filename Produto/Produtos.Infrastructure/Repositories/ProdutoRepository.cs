namespace Produtos.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly DataTable _produtos;

    public ProdutoRepository()
    {
        _produtos = new DataTable();
        _produtos.Columns.Add("Descricao", typeof(string));
        _produtos.Columns.Add("ValorVenda", typeof(double));
        _produtos.Columns.Add("ValorCompra", typeof(double));
        _produtos.Columns.Add("Tipo", typeof(int));
        _produtos.Columns.Add("DataCriacao", typeof(DateTime));
    }

    public Task<bool> AdicionarProduto(ProdutoEntity produto)
    {
        try
        {
            _produtos.Rows.Add(produto.Descricao, produto.ValorVenda, produto.ValorCompra, (int)produto.Tipo, produto.DataCriacao);
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }

    public Task<DataTable> ObterTodosProdutos()
    {
        return Task.FromResult(_produtos);
    }

    public Task<ProdutoEntity?>? ObterProduto(string descricao)
    {
        var row = _produtos.AsEnumerable().ToList();
        
        if (row == null) return null;

        var p = row.FirstOrDefault(r => r.Field<string>("Descricao") == descricao);

        var produto = new ProdutoEntity
        {
            Descricao = p["Descricao"].ToString() ?? string.Empty,
            ValorVenda = (double)(p["ValorVenda"] ?? 0.0),
            ValorCompra = (double)(p["ValorCompra"] ?? 0.0),
            Tipo = (ProdutoTipoEntity)(p["Tipo"] ?? 0),
            DataCriacao = (DateTime)(p["DataCriacao"] ?? DateTime.MinValue)
        };

        return Task.FromResult(produto ?? null);
    }

    public Task<bool> AtualizarProduto(string descricao, ProdutoEntity produtoAtualizado)
    {
        try
        {
            var row = _produtos.AsEnumerable()
            .FirstOrDefault(r => r.Field<string>("Descricao") == descricao);

            if (row != null)
            {
                row["Descricao"] = produtoAtualizado.Descricao;
                row["ValorVenda"] = produtoAtualizado.ValorVenda;
                row["ValorCompra"] = produtoAtualizado.ValorCompra;
                row["Tipo"] = (int)produtoAtualizado.Tipo;
                row["DataCriacao"] = produtoAtualizado.DataCriacao;
            }
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> RemoverProduto(string descricao)
    {
        try
        {
            var row = _produtos.AsEnumerable()
                .FirstOrDefault(r => r.Field<string>("Descricao") == descricao);

            if (row != null)
            {
                _produtos.Rows.Remove(row);
            }

            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}
