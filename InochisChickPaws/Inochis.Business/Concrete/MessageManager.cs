using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Inochis.Business.Abstract;
using Inochis.Data.Abstract;
using Inochis.Entity.Concrete;
using Inochis.Shared.ResponseViewModels;
using Inochis.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _repository;

        public MessageManager(IMapper mapper, IMessageRepository messageRepository)
        {
            _mapper = mapper;
            _repository = messageRepository;
        }

        public async Task<Response<MessageDetailsViewModel>> CreateAsync(MessageDetailsViewModel messageDetailsViewModel)
        {
            var message = _mapper.Map<Message>(messageDetailsViewModel);
            var createdMessage = await _repository.CreateAsync(message);
            if (createdMessage == null)
            {
                return Response<MessageDetailsViewModel>.Fail("Mesaj gönderilemedi");
            }
            var createdMessageDetailsViewModel = _mapper.Map<MessageDetailsViewModel>(createdMessage);
            return Response<MessageDetailsViewModel>.Success(createdMessageDetailsViewModel);
        }

        public async Task<Response<List<MessageDetailsViewModel>>> GetAllReceivedMessageAsync(string userId, bool isRead = false)
        {
            var messageList = await _repository.GetAllAsync(x => x.FromId == userId && x.IsRead==isRead);
            if (messageList.Count == 0)
            {
                var infoText = isRead ? "Okunmuş" : "Okunmamış";
                return Response<List<MessageDetailsViewModel>>.Fail($"{infoText} mesajınız bulunmamaktadır.");
            }
            var messageViewModelList = _mapper.Map<List<MessageDetailsViewModel>>(messageList);
            return Response<List<MessageDetailsViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<List<MessageDetailsViewModel>>> GetAllSentMessageAsync(string userId)
        {
            var messageList = await _repository.GetAllAsync(x=>x.FromId==userId);
            if (messageList.Count == 0)
            {
                return Response<List<MessageDetailsViewModel>>.Fail("Giden kutusu boş");
            }
            var messageViewModelList = _mapper.Map<List<MessageDetailsViewModel>>(messageList);
            return Response<List<MessageDetailsViewModel>>.Success(messageViewModelList);
        }

        public async Task<Response<MessageDetailsViewModel>> GetByIdAsync(int id)
        {
            var message = await _repository.GetByIdAsync(x => x.Id == id);
            if (message == null)
            {
                return Response<MessageDetailsViewModel>.Fail("Mesaj açılamadı");
            }
            var messageDetailsViewModel = _mapper.Map<MessageDetailsViewModel>(message);
            return Response<MessageDetailsViewModel>.Success(messageDetailsViewModel);
        }

        public async Task<Response<int>> GetMessageCount(string userId, bool isRead = false)
        {
            var count = await _repository.GetCount(x => x.FromId==userId && x.IsRead==isRead);
            return Response<int>.Success(count);
        }

        public async Task<Response<NoContent>> HardDeleteAsync(int id)
        {
            var message = await _repository.GetByIdAsync(x => x.Id == id);
            if (message == null)
            {
                return Response<NoContent>.Fail("Silinecek mesaj bulunamadı");
            }
            await _repository.HardDeleteAsync(message);
            return Response<NoContent>.Success();
        }
    }
}
