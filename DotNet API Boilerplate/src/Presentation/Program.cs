using Serilog;
using DotNet_API_Boilerplate.Presentation.Endpoints.Users;
using DotNet_API_Boilerplate.Presentation.Endpoints.Posts;
using DotNet_API_Boilerplate.Presentation.Extensions;
using DotNet_API_Boilerplate.Presentation.Endpoints.Auth;
using DotNet_API_Boilerplate.Presentation.Versioning;

var builder = WebApplication
                .CreateBuilder(args)
                .ConfigureBuilder();
var app = builder
            .Build()
            .ConfigureApplication();

var apiVersionSet = app.AddVersionSet();

app.MapUserEndpoints(apiVersionSet);
app.MapPostEndpoints(apiVersionSet);
app.MapAuthEndpoints(apiVersionSet);

try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
