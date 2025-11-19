using System;

namespace NewSamDU.Application.DTOs.Queries;

public class BaseQuery
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;

    public bool OnlyDeletedItem = false;
}
