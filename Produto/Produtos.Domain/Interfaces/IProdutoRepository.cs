namespace Produtos.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<bool> AdicionarProduto(ProdutoEntity produto);
    Task<DataTable> ObterTodosProdutos();
    Task<ProdutoEntity?>? ObterProduto(string descricao);
    public Task<bool> AtualizarProduto(string descricao, ProdutoEntity produtoAtualizado);
    Task<bool> RemoverProduto(string descricao);
}
