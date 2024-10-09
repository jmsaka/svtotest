namespace Produtos.Domain.Queries
{
    public class ObterProdutosQuery : IRequest<List<ProdutoEntity>>
    {
        public string? Descricao { get; set; }
    }

    public class ObterProdutosQueryHandler : IRequestHandler<ObterProdutosQuery, List<ProdutoEntity>>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ObterProdutosQueryHandler(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProdutoEntity>> Handle(ObterProdutosQuery request, CancellationToken cancellationToken)
        {
            if (request.Descricao != null)
            {
                var produto = await _repository.ObterProduto(request.Descricao);
                return produto != null ? new List<ProdutoEntity> { produto } : new List<ProdutoEntity>(); 
            }

            var produtosDataTable = await _repository.ObterTodosProdutos();

            var produtos = _mapper.Map<List<ProdutoEntity>>(produtosDataTable.AsEnumerable());

            return produtos.ToList();
        }
    }
}
