namespace DotNet_API_Boilerplate.Core.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

[Serializable]
[ExcludeFromCodeCoverage]
public class UserDeleteException : Exception
{
    public IEnumerable<string> Errors { get; set; }
    public UserDeleteException(string message, IEnumerable<string> errors)
        : base(message)
    {
        Errors = errors;
    }

    protected UserDeleteException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>Throws an <see cref="UserDeleteException"/> if <paramref name="identityResult"/> contains errors.</summary>
    /// <param name="identityResult">The result of user creation.</param>
    public static void ThrowIfFailed(IdentityResult identityResult)
    {
        if (!identityResult.Succeeded)
        {
            Throw(identityResult);
        }
    }

    /// <summary>Throws an <see cref="UserDeleteException"/></summary>
    /// <param name="identityResult">The result of user creation.</param>
    public static void Throw(IdentityResult identityResult)
    {
        if (identityResult.Errors != null && identityResult.Errors.Any())
        {
            throw new UserCreateException($"The user could not be deleted.", identityResult.Errors.Select(x => x.Description));
        }
    }
}
