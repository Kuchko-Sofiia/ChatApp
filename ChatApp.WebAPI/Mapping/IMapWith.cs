using AutoMapper;

namespace ChatApp.API.Mapping
{
    public interface IMapWith<T> where T : class
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}