using System.Security.Cryptography;
using System.Text;
using MessageDataBase.BD;
using Microsoft.EntityFrameworkCore;

namespace MessageDataBase.Repository;

public class MessageRepository: IMessageRepository {
    public Guid SendMessage(string text, string senderName, string receiverName) {
        using (var context = new MessagesContext()) {
            var sender = context.Users.FirstOrDefault(user => user.Name == senderName);
            var receiver = context.Users.FirstOrDefault(user => user.Name == receiverName);
            if (sender != null & receiver != null) {
                var id = Guid.NewGuid();
                var message = new Message() {
                    Id = id,
                    Sender = sender,
                    Receiver = receiver,
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
        using (var context = new MessagesContext()) {
            var messages = context.Messages
                .Where(message => message.Receiver.Name == receiverName && message.IsReceived == false)
                .ToList();
            foreach (var message in messages) {
                message.IsReceived = true;
                context.SaveChanges();
            }

            return messages;
        }
    }
}