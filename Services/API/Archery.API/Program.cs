using Archery.API;
using Archery.API.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));


builder.Services.AddDbContext<AE.Context>((serviceProvider, dbContextOptionsBuilder) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpResponseFormatter<CustomHttpResponseFormatter>();

//builder.Services.AddErrorFilter<CustomErrorFilter>();

builder.Services.AddGraphQLServer()

    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = false)

    .AddMutationConventions(
        new MutationConventionOptions
        {
            PayloadErrorTypeNamePattern = "{MutationName}Error",
            PayloadErrorsFieldName = "errors",
        }
    )

    .RegisterDbContext<AE.Context>()

    .RegisterService<MappingProfile>()

    //.AddMutationType<Mutation>()

    // TODO Create a method called AddTypes()

    .AddMyTypes();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                          builder.AllowAnyOrigin();
                          //builder.AllowCredentials();
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                      });
});

var app = builder.Build();

app.MapGraphQL();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.Run();