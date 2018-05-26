using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using ADWrapper.BusinessLogic.Interfaces;

namespace ADWrapper.BusinessLogic.Classes
{
    public class BGroup : IBGroup
    {
        private readonly IBUser _bUser;
        public BGroup(IBUser bUser)
        {
            _bUser = bUser;
        }
        public bool IsValidGroup(string groupName)
        {
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                return GroupPrincipal.FindByIdentity(ctx, groupName) != null;
            }
        }

        public List<string> GetGroupMembersList(string groupName)
        {
            if (!IsValidGroup(groupName)) return null;
            var userList = new HashSet<string>();
            var groupList = new HashSet<string>
            {
                groupName
            };
            while (groupList.Count > 0)
            {
                AddMembersToSet(ref userList, ref groupList,groupList.First());
            }
            return userList.ToList();
        }

        private void AddMembersToSet(ref HashSet<string> userList, ref HashSet<string> groupList, string groupName)
        {
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                var group = GroupPrincipal.FindByIdentity(ctx, groupName);
                if (group == null) return;
                foreach (var principal in group.GetMembers())
                {
                    if (principal is UserPrincipal)
                        userList.Add(principal.SamAccountName);
                    else
                        groupList.Add(principal.SamAccountName);
                }
            }

            groupList.Remove(groupName);
        }

        public bool IsUserGroupMember(string groupName, string username)
        {
            if (!IsValidGroup(groupName)) return false;
            if (!_bUser.IsValidUser(username)) return false;

            var groupList = new HashSet<string>
            {
                groupName
            };
            while (groupList.Count > 0)
            {
                var currentGroup = groupList.First();
                using (var ctx = new PrincipalContext(ContextType.Domain))
                {
                    var group = GroupPrincipal.FindByIdentity(ctx, currentGroup);
                    if (group == null)
                    {
                        groupList.Remove(currentGroup);
                        continue;
                    }

                    foreach (var principal in group.GetMembers())
                    {
                        if (principal is UserPrincipal)
                        {
                            if (principal.SamAccountName == username)
                                return true;
                        }
                        else
                        {
                            groupList.Add(principal.SamAccountName);
                        }
                        
                    }
                }
                groupList.Remove(currentGroup);
            }
            return false;
        }
    }
}
