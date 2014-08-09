using System.Collections.Generic;
using ChatR.Model;

namespace ChatR.WpfClient.Repositories
{
    public interface IMessageRepository
    {
        UserDetail CurrentUserDetail { get; set; }
        List<UserDetail> OtherUsers { get; set; }
    }

    public class MessageRepository : IMessageRepository
    {
         public UserDetail CurrentUserDetail { get; set; }

        public List<UserDetail> OtherUsers { get; set; }
    }
}