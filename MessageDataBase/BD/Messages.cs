namespace MessageDataBase.BD;

public class Messages {
    public int Id { get; set; }
    public virtual User Sender { get; set; }
    public int SenderId { get; set; }
    public virtual User Receiver { get; set; }
    public int ReceiverId { get; set; }
    public string Text { get; set; }
    public bool IsReceived { get; set; }
}