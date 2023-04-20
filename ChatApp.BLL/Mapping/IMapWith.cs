using AutoMapper;

namespace ChatApp.BLL.Mapping
{
    public interface IMapWith<T> where T : class
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}