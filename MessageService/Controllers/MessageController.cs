using DataBase;
using DataBase.Repository;
using MessageDataBase.Repository;
using MessageService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController: ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly IMessageRepository _messageRepository;
    private UserContext _userRepository;

    public MessageController(IConfiguration configuration, IMessageRepository messageRepository) {
        _configuration = configuration;
        _messageRepository = messageRepository;
    }

    [HttpGet]
    [Route("GetMessages")]
    public IActionResult GetAllMessages(string userName) {
        var messages = _messageRepository.GetAllMessages(userName);
        return Ok(messages);
    }

    [HttpPost]
    [Route("SendMessage")]
    public IActionResult SendMessage(MessageDTO message) {
        _userRepository = new UserContext();
        var sender = _userRepository
            .Users
            .FirstOrDefault(sender => sender.Name == message.SenderName);
        var recever = _userRepository
            .Users
            .FirstOrDefault(reciver => reciver.Name == message.ReceiverName);
        if (sender!=null && recever!=null) {
            var messageId = _messageRepository.SendMessage(
                message.Text,
                message.SenderName,
                message.ReceiverName
            );
            return Ok(messageId);
        }

        return NotFound("Sender or Receiver not fount in base");
    }
}