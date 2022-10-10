namespace Treblle_Core_API_Boilerplate.Core.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

[Serializable]
[ExcludeFromCodeCoverage]
public class UserUpdateException : Exception
{
    public IEnumerable<string> Errors { get; set; }
    public UserUpdateException(string message, IEnumerable<string> errors)
        : base(message)
    {
        Errors = errors;
    }

    protected UserUpdateException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>Throws an <see cref="UserUpdateException"/> if <paramref name="identityResult"/> contains errors.</summary>
    /// <param name="identityResult">The result of user creation.</param>
    public static void ThrowIfFailed(IdentityResult identityResult)
    {
        if (!identityResult.Succeeded)
        {
            Throw(identityResult);
        }
    }

    /// <summary>Throws an <see cref="UserUpdateException"/></summary>
    /// <param name="identityResult">The result of user creation.</param>
    public static void Throw(IdentityResult identityResult)
    {
        if (identityResult.Errors != null && identityResult.Errors.Any())
        {
            throw new UserCreateException($"The user could not be updated.", identityResult.Errors.Select(x => x.Description));
        }
    }
}
