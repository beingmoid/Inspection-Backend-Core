using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Services
{
    public class UserServices : BaseService<User, string>, IUserService
    {
        public UserServices(RequestScope requestScope,IUserRepository repository) : base(requestScope,repository)
        {
        }
    }

    public interface IUserService :IBaseService<User,string>
    {

    }
}
