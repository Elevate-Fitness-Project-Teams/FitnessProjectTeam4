using System.Reflection;

namespace NutritionService.Common
{
    public static class EndpointExtensions
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
            var endpointDefinitions = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpoint in endpointDefinitions)
            {
                endpoint.MapEndpoints(app);
            }
        }
    }
}
