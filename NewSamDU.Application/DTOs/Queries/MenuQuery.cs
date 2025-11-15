using System;

namespace NewSamDU.Application.DTOs.Queries;

public class MenuQuery : BaseQuery
{
    public int Depth { get; set; } = 0;
}
