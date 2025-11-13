using System;

namespace NewSamDU.Application.Results;

public class PaginatedResult<T>
{
    public List<T> Items { get; set; }

    public Meta Meta { get; set; }

    public PaginatedResult(List<T> items, int totalCount, int pageSize, int page)
    {
        Items = items;
        Meta = new Meta(totalCount, page, pageSize);
    }
}

public class Meta
{
    public int TotalCount { get; set; }

    public int Page { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public int PageSize { get; set; }

    public Meta(int totalCount, int page, int pageSize)
    {
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}
