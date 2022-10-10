namespace Treblle_Core_API_Boilerplate.Core.Common.Exceptions;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

[Serializable]
[ExcludeFromCodeCoverage]
public class AuthenticationException : Exception
{
    public AuthenticationException(string message)
        : base(message)
    {
    }

    protected AuthenticationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
