using AutoMapper;
using System.Reflection;


namespace ChatApp.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(Assembly[] assemblies) 
        {
            ApplyMappingsFromAssembly(assemblies);
        }
        public AutoMapperProfile() { }
        private void ApplyMappingsFromAssembly(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes()
                    .Where(type => type.GetInterfaces()
                        .Any(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                    .ToList();

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type);
                    var methodInfo = type.GetMethod("Mapping");
                    methodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}
