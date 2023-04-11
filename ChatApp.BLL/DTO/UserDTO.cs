using ChatApp.BLL.Mapping;
using ChatApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BLL.DTO
{
    public class UserDTO : IMapWith<User>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
