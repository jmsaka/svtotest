using Produtos.Domain.Commands;

namespace Produtos.Domain.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<AdicionarProdutoCommand, ProdutoEntity>();

        CreateMap<DataRow, ProdutoEntity>()
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Field<string>("Descricao") ?? string.Empty))
            .ForMember(dest => dest.ValorVenda, opt => opt.MapFrom(src => src.Field<double?>("ValorVenda") ?? 0.0))
            .ForMember(dest => dest.ValorCompra, opt => opt.MapFrom(src => src.Field<double?>("ValorCompra") ?? 0.0))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (ProdutoTipoEntity)(src.Field<int?>("Tipo") ?? 0)))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Field<DateTime?>("DataCriacao") ?? DateTime.MinValue));
    }
}
