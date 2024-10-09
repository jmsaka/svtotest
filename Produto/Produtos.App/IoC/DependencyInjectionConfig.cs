

using Produtos.Domain.Validators;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ProdutoCommandValidator>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(AdicionarProdutoCommandHandler).Assembly,
            typeof(ObterProdutosQueryHandler).Assembly
        ));

        services.AddAutoMapper(typeof(ProdutoProfile));

        services.AddSingleton<IProdutoRepository, ProdutoRepository>();

        return services;
    }
}
