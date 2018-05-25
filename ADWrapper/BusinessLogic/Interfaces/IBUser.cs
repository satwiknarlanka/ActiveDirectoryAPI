using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADWrapper.Models;

namespace ADWrapper.BusinessLogic.Interfaces
{
    public interface IBUser
    {
        bool IsValidUser(string username);
        AdUser GetUserDetails(string username);
    }
}
