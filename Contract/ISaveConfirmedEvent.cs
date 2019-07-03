using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface ISaveConfirmedEvent 
    {
        long id {get; set;}
        string text {get; set;}
        DateTime time { get; set; }
    }
}
