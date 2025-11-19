using System.Reflection;

namespace HanuMediSoftCore.Helpers
{
    public static  class ServiceRegistrationExtensions
    {
        public static void AddAllServices(this IServiceCollection services)
        {
            // Scan the assembly where your services exist
            var assembly = Assembly.GetExecutingAssembly();

            var serviceTypes = assembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsAbstract);

            foreach (var type in serviceTypes)
            {
                services.AddScoped(type);
            }
        }
    }
}
