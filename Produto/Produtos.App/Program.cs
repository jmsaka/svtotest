class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        DependencyInjectionConfig.ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Mostrar Lista de Produtos");
            Console.WriteLine("2 - Mostrar Produto por Descrição");
            Console.WriteLine("3 - Inserir Novo Produto");
            Console.WriteLine("4 - Mostrar produtos do segundo semestre de 2024");
            Console.WriteLine("5 - Mostrar Ordenado por Tipo");
            Console.WriteLine("6 - Mostrar Top 3 que tiveram maior margem de lucro");
            Console.WriteLine("7 - Carga Inicial de Produtos");
            Console.WriteLine("0 - Sair");
            Console.Write("\nDigite sua escolha: ");

            string opcao = Console.ReadLine() ?? string.Empty;

            switch (opcao)
            {
                case "1":
                    await ProdutosApplication.MostrarProdutos(mediator);
                    break;
                case "2":
                    Console.Write("Digite a descrição do produto que deseja buscar: ");
                    string descricao = Console.ReadLine() ?? string.Empty;
                    await ProdutosApplication.MostrarProdutos(mediator, descricao);
                    break;
                case "3":
                    await ProdutosApplication.InserirProduto(mediator);
                    break;
                case "4":
                    await ProdutosApplication.MostrarProdutosPorCriterio(mediator, 0);
                    break;
                case "5":
                    await ProdutosApplication.MostrarProdutosPorCriterio(mediator, 1);
                    break;
                case "6":
                    await ProdutosApplication.MostrarProdutosPorCriterio(mediator, 2);
                    break;
                case "7":
                    await ProdutosApplication.CargaInicialProdutos(mediator);
                    break;
                case "0":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}