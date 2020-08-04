using AutoMapper;

namespace Plus.AutoMapper
{
    internal class MapperAccessor : IMapperAccessor
    {
        public IMapper Mapper { get; set; }
    }
}