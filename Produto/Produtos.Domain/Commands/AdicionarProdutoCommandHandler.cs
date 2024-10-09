namespace Produtos.Domain.Commands;

public class AdicionarProdutoCommand : IRequest<ProdutoEntity>
{
    public required string Descricao { get; set; }
    public double ValorVenda { get; set; }
    public double ValorCompra { get; set; }
    public ProdutoTipoEntity Tipo { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class AdicionarProdutoCommandHandler : IRequestHandler<AdicionarProdutoCommand, ProdutoEntity>
{
    private readonly IProdutoRepository _repository;
    private readonly IValidator<AdicionarProdutoCommand> _validator;
    private readonly IMapper _mapper;

    public AdicionarProdutoCommandHandler(IProdutoRepository repository, IValidator<AdicionarProdutoCommand> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ProdutoEntity> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var produto = _mapper.Map<ProdutoEntity>(request);

        await _repository.AdicionarProduto(produto);
        return produto;
    }
}
