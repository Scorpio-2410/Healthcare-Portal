using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class CommunicationChannel
{
    public int MessageId { get; set; }

    public int? SenderId { get; set; }

    public int? ReceiverId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string MessageText { get; set; } = null!;
}
