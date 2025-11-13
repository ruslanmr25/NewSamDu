using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NewSamDU.Application.Responses;

public class Response<T>
    where T : class
{
    public Response(T? data)
    {
        Data = data;
    }

    public bool Success { get; set; } = true;

    public string Message { get; set; } = "Operation was done successfully!";

    public T? Data { get; set; } = default;

    public IEnumerable<object> Errors { get; set; } = [];
}

public class Response : Response<object>
{
    public Response()
        : base(null) { }
}
