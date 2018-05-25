using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADWrapper.BusinessLogic.Interfaces
{
    public interface IBGroup
    {
        bool IsValidGroup(string groupName);
        List<string> GetGroupMembersList(string groupName);
        bool IsUserGroupMember(string groupName, string username);
    }
}
