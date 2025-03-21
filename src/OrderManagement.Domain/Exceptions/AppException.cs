using System.Globalization;

namespace OrderManagement.Domain.Exceptions;

public class AppException : Exception
{
    public AppException() : base() { }

    public AppException(string message) : base(message)
    {
    }
}
