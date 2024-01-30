using MessageDataBase.BD;

namespace TestProject;

[TestFixture]
public class MessageTests {
    [Test]
    public void Message_Properties_SetAndGet() {
        // Arrange
        var message = new Message();
        var Id = Guid.NewGuid();

        // Act
        message.Id = Id;
        message.SenderName = "";
        message.ReceiverName = "new User()";
        message.Text = "Hello, World!";
        message.IsReceived = true;

        // Assert
        Assert.AreEqual(message.Id, Id);
        Assert.IsNotNull(message.SenderName);
        Assert.IsNotNull(message.ReceiverName);
        Assert.AreEqual(message.Text, "Hello, World!");
        Assert.IsTrue(message.IsReceived);
    }
}