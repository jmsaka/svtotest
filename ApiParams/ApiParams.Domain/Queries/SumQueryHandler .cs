namespace ApiParams.Domain.Queries;

public class SumQuery : IRequest<int>
{
    public int A { get; set; }
    public int B { get; set; }
}

public class SumQueryHandler : IRequestHandler<SumQuery, int>
{
    public Task<int> Handle(SumQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.A + request.B);
    }
}
