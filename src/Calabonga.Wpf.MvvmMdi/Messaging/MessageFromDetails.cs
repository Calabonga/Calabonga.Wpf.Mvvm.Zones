namespace Calabonga.Wpf.MvvmMdi.Messaging;

public class MessageFromDetails
{
    public MessageFromDetails(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}