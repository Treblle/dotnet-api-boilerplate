namespace Treblle_Core_API_Boilerplate.Presentation.Versioning;

using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using Asp.Versioning.Builder;

[ExcludeFromCodeCoverage]
public static class ApiVersionExtensions
{
    public static ApiVersionSet AddVersionSet(this WebApplication app)
    {
        return app.NewApiVersionSet()
                    .HasApiVersion(new ApiVersion(1, 0))
                    .ReportApiVersions()
                    .Build();
    }
}
