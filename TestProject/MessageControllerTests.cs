using MessageDataBase.BD;
using MessageService.Controllers;
using MessageService.DTO;
using Microsoft.AspNetCore.Mvc;
using MessageDataBase.Repository;
using Moq;

namespace TestProject;

[TestFixture]
public class MessageControllerTests {
    private MessageController _controller;
    private Mock<IMessageRepository> _mockMessageRepository;

    [SetUp]
    public void Setup() {
        _mockMessageRepository = new Mock<IMessageRepository>();
        _controller = new MessageController(null, _mockMessageRepository.Object);
    }

    [Test]
    public void GetAllMessages_ReturnsOkResult() {
        // Arrange
        var expectedMessages = new List<Message> {
            new Message {
                Text = "Hello, world!", SenderName = "John Doe",
                ReceiverName = "John Doe"
            },
            new Message {
                Text = "How are you?", SenderName = "John Doe",
                ReceiverName = "John Doe"
            }
        };
        _mockMessageRepository.Setup(x => x.GetAllMessages(It.IsAny<string>())).Returns(expectedMessages);

        // Act
        var result = _controller.GetAllMessages("John Doe");

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public void SendMessage_ReturnsOkResult() {
        // Arrange
        var id = Guid.NewGuid();
        var message = new MessageDTO { Text = "Hello, world!", SenderName = "John Doe", ReceiverName = "Jane Doe" };
        _mockMessageRepository.Setup(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(id);

        // Act
        var result = _controller.SendMessage(message).ToString();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        Assert.AreEqual(id.ToString(), result);
    }
}