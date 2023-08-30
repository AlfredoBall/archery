using Archery.API;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));


builder.Services.AddDbContext<AE.Context>((serviceProvider, dbContextOptionsBuilder) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddHttpContextAccessor();

builder.Services.AddGraphQLServer()

    .AddMutationConventions()

    .RegisterDbContext<AE.Context>()

    .RegisterService<MappingProfile>()

    //.AddMutationType<Mutation>()

    // TODO Create a method called AddTypes()

    .AddMyTypes();




var app = builder.Build();

app.MapGraphQL();

app.UseHttpsRedirection();

app.Run();