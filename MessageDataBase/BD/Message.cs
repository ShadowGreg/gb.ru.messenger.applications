namespace MessageDataBase.BD;

public class Message {
    public Guid Id { get; set; }
    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
    public string Text { get; set; }
    public bool IsReceived { get; set; }
}