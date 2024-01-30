using System.Security.Cryptography;
using System.Text;
using MessageDataBase.BD;

namespace MessageDataBase.Repository;

public class MessageRepository: IMessageRepository {
    public Guid SendMessage(string text, string senderName, string receiverName) {
        using (var context = new MessagesContext()) {
            var sender = context.Users.FirstOrDefault(user => user.Name == senderName);
            var receiver = context.Users.FirstOrDefault(user => user.Name == receiverName);
            if (sender != null & receiver != null) {
                var id = new Guid();
                var message = new Message() {
                    Id = id,
                    Sender = sender,
                    SenderId = sender.Id,
                    Receiver = receiver,
                    ReceiverId = receiver.Id,
                    Text = text,
                    IsReceived = false
                };
                context.Messages.Add(message);
                context.SaveChanges();
                return id;
            }

            throw new ArgumentException("There are no such registered users in the system");
        }
    }

    public List<Message> GetAllMessages(string receiverName) {
        throw new NotImplementedException();
    }
}