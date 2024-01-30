using MessageDataBase.Repository;
using MessageService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController: ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly IMessageRepository _messageRepository;

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
        var messageId = _messageRepository.SendMessage(
            message.Text,
            message.SenderName,
            message.ReceiverName
        );
        return Ok(messageId);
    }
}