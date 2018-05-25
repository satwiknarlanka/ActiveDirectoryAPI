using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using ADWrapper.BusinessLogic.Interfaces;
using ADWrapper.Models;

namespace ADWrapper.BusinessLogic.Classes
{
    public class BUser :IBUser
    {

        public bool IsValidUser(string username)
        {
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                return UserPrincipal.FindByIdentity(ctx, username) != null;
            }
        }

        public AdUser GetUserDetails(string username)
        {
            if (!IsValidUser(username)) return null;
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                using (var userPrincipal = UserPrincipal.FindByIdentity(ctx,username))
                {
                    return new AdUser()
                    {
                        Username = userPrincipal?.SamAccountName,
                        Email = userPrincipal?.EmailAddress,
                        FirstName = userPrincipal?.GivenName,
                        LastName = userPrincipal?.Surname
                    };
                }
            }
        }
    }
}
