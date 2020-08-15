using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    public class MessageQueue : RaspApi<MessageQueue, Message>
    {
        #region Properties
        public SortedList Index { get; } = new SortedList();
        public List<BlockingCollection<Message>> Queue { get; } = new List<BlockingCollection<Message>>();
        #endregion

        #region Methods
        public void Add(object obj)
        {
            Index.Add(Index.Count, obj);
            Queue.Add(new BlockingCollection<Message>());
        }
        public bool Enqueue(object obj, Message message) => Queue[Index.IndexOfValue(obj)].TryAdd(message);
        
        public Message Dequeue(object obj, CancellationToken token) => Queue[Index.IndexOfValue(obj)].Take(token);
        #endregion
    }
}
