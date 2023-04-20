using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.DTO
{
    public class PaginatedDataDTO<T>: IMapWith<PaginatedData<UserInfoDTO>>
    {
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }
        public int PageIndex { get; private set; }
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;
        public List<T> Items { get; set; } = new();

        public PaginatedDataDTO() { }

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<PaginatedData<User>, PaginatedDataDTO<UserInfoDTO>>()
        //        .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
        //}
    }
}
