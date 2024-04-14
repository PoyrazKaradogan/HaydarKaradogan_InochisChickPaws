using Inochis.Shared.ResponseViewModels;
using Inochis.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Business.Abstract
{
    public interface IMessageService
    {
        Task<Response<MessageViewModel>> CreateAsync(MessageViewModel messageViewModel);
        Task<Response<NoContent>> HardDeleteAsync(int id);
        Task<Response<List<MessageViewModel>>> GetAllSentMessageAsync(string fromUserId);
        Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string toUserId, bool isRead);
        Task<Response<List<MessageViewModel>>> GetAllReceivedMessageAsync(string toUserId);
        Task<Response<MessageViewModel>> GetByIdAsync(int id);
        Task<Response<int>> GetMessageCountAsync(string userId, bool isRead = false);
        Task<Response<NoContent>> MakeRead(int id);
    }
}