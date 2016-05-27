using RecipeShoppingApp.Entities;
using RecipeShoppingApp.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeShoppingApp.Services
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
    }
}
