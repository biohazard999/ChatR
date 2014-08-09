using System.Collections.Generic;
using ChatR.Model;

namespace ChatR.ClientData.Repositories
{
    public class MessageRepository : IMessageRepository
    {
         public UserDetail CurrentUserDetail { get; set; }

        public List<UserDetail> OtherUsers { get; set; }
    }
}