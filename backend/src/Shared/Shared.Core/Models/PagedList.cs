namespace Shared.Core.Models;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public long TotalCount { get; init; }
    public bool IsHasNaxtPage => Page * PageSize < TotalCount;
    public bool IsHasЗreviousPage => Page > 1;
}
