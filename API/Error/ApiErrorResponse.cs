using System;

namespace API.Error;

public class ApiErrorResponse(int Statuscode,string message,string? details)
{
    public int Statuscode { get; set; } = Statuscode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
