using System;

namespace Contract
{
    public interface ISaveMessageCommand
    {
        long id {get; set;}
        string text { get; set; }
    }
    public interface CustomerAddressUpdated
    {
    }

    //public class MessageTextCommand : ISaveMessageCommand
    //{
    //    public string Text { get; set; }
    //}
}
