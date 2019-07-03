using System;

namespace Contract
{
    public interface IMessageText
    {
        string Text { get; set; }
    }
    public interface CustomerAddressUpdated
    {
    }

    public class MessageTextCommand : IMessageText
    {
        public string Text { get; set; }
    }
}
