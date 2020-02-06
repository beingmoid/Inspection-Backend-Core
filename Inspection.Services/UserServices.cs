using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspection.Services
{
    public class UserServices : BaseService<User, string>, IUserService
    {
        public UserServices(RequestScope requestScope,IUserRepository repository) : base(requestScope,repository)
        {
        }

        public async Task<object> Register(Register reg)
        {
            var user = new User()
            {
                Id = reg.UserId,
                Password = reg.Password,
                RoleId = 1
            };
            List<User> li = new List<User>();
            li.Add(user);
           return (await this.Insert(li)).Entities.Values.SingleOrDefault();
        }
    }

    public interface IUserService :IBaseService<User,string>
    {
        Task<object> Register(Register reg);
    }
    public class Register
    {
        public string UserId { get; set; }
        public string Password { get; set; }

    }
}
