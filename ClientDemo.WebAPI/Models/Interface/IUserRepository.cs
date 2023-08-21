using TravelNinjaz.B2B.WebAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IUserRepository
    {
        // UserInfo UserCheckAuthentication(string userName, string password);
        User uspGetUserDetailByUserName(string userName);
        string SaveUser(User userToCreate);
        string UpdateUser(UserInfo updateUser);
        string UpdateUserCompany(UserCompany updatecompany);
        string SaveUserCompany(UserCompany savecompany);
        User getUserCompany(string AspNetUserId);
        List<Entity.User> GetUserList();
    }
}
