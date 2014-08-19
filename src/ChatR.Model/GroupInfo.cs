using System.Collections.Generic;

namespace ChatR.Model
{
    public class GroupInfo
    {
        public GroupInfo()
        {
            Users = new List<UserDetail>();
        }

        public List<UserDetail> Users { get; private set; }
        public string GroupName { get; set; }
    }
}