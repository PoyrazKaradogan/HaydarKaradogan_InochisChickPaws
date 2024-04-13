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
    {Task<Response<int>> GetMessageCount(string userId, bool isRead=false);


  Task<Response<List<MessageDetailsViewModel>>> GetAllSentMessageAsync(string userId);
        Task<Response<List<MessageDetailsViewModel>>> GetAllReceivedMessageAsync(string userId, bool isRead=false);


        Task<Response<MessageDetailsViewModel>> CreateAsync(MessageDetailsViewModel messageDetailsViewModel);
        Task<Response<NoContent>> HardDeleteAsync(int id);
      
        Task<Response<MessageDetailsViewModel>> GetByIdAsync(int id);
        
    }
}