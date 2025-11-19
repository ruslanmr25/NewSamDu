using System;

namespace NewSamDU.Application.DTOs.Queries;

public class PageQuery : BaseQuery
{
    public bool OnlyUnasignedPages = false;
}
