using Archery.API.Attributes;
using HotChocolate.Execution.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class SchemaRequestExecutorBuilderExtensions
    //public static class RequestBuilderExtensions
    {
        public static IRequestExecutorBuilder AddMyTypes(
        this IRequestExecutorBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var types = Assembly.GetExecutingAssembly().GetTypes()
                  .Where(t =>
                      t.Namespace != null &&
                      t.Namespace == "Archery.API.Types" &&
                      t.ReflectedType == null);

            // TODO Establish naming conventions, stop using custom attributes

            var queryGroupings = types.SelectMany(t =>
                    t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => !m.Name.StartsWith("Create") && !m.Name.StartsWith("Update") && !m.Name.StartsWith("Delete")))
                    .Where(m => m.GetCustomAttribute<IgnoreAttribute>() == null)
                    .GroupBy(g => g.DeclaringType).ToList();

            var mutationGroupings = types.SelectMany(t =>
                t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => m.Name.StartsWith("Create") || m.Name.StartsWith("Update") || m.Name.StartsWith("Delete")))
                    .GroupBy(g => g.DeclaringType).ToList();

            builder.ConfigureSchema(b => b.AddQueryType(d =>
            {
                d.Name(OperationTypeNames.Query);

                AddFields(d, queryGroupings);
            }));

            builder.ConfigureSchema(b => b.AddMutationType(d =>
            {
                d.Name(OperationTypeNames.Mutation);

                AddFields(d, mutationGroupings);
            }));

            return builder;
        }

        private static void AddFields(IObjectTypeDescriptor descriptor, List<IGrouping<Type?, MethodInfo>> groupings)
        {
            groupings.ForEach(g =>
            {
                g.ToList().ForEach(m =>
                {
                    var field = descriptor.Field(Char.ToLowerInvariant(m.Name[0]) + m.Name.Substring(1));

                    m.GetParameters().Where(p => p.Name != "mapper" && p.Name != "context" && p.Name != "parent").ToList().ForEach(p =>
                    {
                        field.Argument(p.Name, a =>
                        {
                            a.Type<NonNullType<AnyType>>();
                        });
                        
                        
                    });

                    field.ResolveWith(m);
                });
            });
        }
    }
}
