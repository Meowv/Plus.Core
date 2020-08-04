using AutoMapper;

namespace Plus.AutoMapper
{
    public interface IMapperAccessor
    {
        IMapper Mapper { get; }
    }
}