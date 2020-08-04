using AutoMapper;
using Plus.AutoMapper;

namespace Plus.ObjectMapping
{
    public static class PlusAutoMapperObjectMapperExtensions
    {
        public static IMapper GetMapper(this IObjectMapper objectMapper)
        {
            return objectMapper.AutoObjectMappingProvider.GetMapper();
        }

        public static IMapper GetMapper(this IAutoObjectMappingProvider autoObjectMappingProvider)
        {
            if (autoObjectMappingProvider is AutoMapperAutoObjectMappingProvider autoMapperAutoObjectMappingProvider)
            {
                return autoMapperAutoObjectMappingProvider.MapperAccessor.Mapper;
            }

            throw new PlusException($"Given object is not an instance of {typeof(AutoMapperAutoObjectMappingProvider).AssemblyQualifiedName}. The type of the given object it {autoObjectMappingProvider.GetType().AssemblyQualifiedName}");
        }
    }
}