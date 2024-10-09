using Produtos.Domain.Commands;
using FluentValidation;

namespace Produtos.Domain.Validators;

public class ProdutoCommandValidator : AbstractValidator<AdicionarProdutoCommand>
{
    public ProdutoCommandValidator()
    {
        RuleFor(produto => produto.ValorVenda)
            .GreaterThan(0)
            .WithMessage("O valor de venda deve ser maior que zero.")
            .GreaterThan(produto => produto.ValorCompra)
            .WithMessage("O valor de venda não pode ser menor ou igual ao valor de compra.");

        RuleFor(produto => produto.ValorCompra)
            .GreaterThan(0)
            .WithMessage("O valor de compra deve ser maior que zero.");

        RuleFor(produto => produto.DataCriacao)
            .GreaterThanOrEqualTo(new DateTime(2024, 1, 1))
            .WithMessage("A data de criação não pode ser anterior a 01/01/2024.");

        RuleFor(produto => produto.Descricao)
            .MinimumLength(5)
            .WithMessage("A descrição deve ter pelo menos 5 caracteres.");
    }
}
