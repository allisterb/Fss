using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    public class MessageQueue : RaspApi<MessageQueue, Message>
    {
        #region Constructors
        public MessageQueue(object[] objs)
        {
            Index = new SortedList(objs.Length);
            Queue = new BlockingCollection<Message>[(objs.Length)];
            for (int i = 0; i < objs.Length; i++)
            {
                Queue[i] = new BlockingCollection<Message>();
                Index.Add(i, objs[i]);
            }
        }
        #endregion

        #region Properties
        public SortedList Index { get; }
        public BlockingCollection<Message>[] Queue { get; }
        #endregion

        #region Methods
        public bool Enqueue(object obj, Message message) => Queue[Index.IndexOfValue(obj)].TryAdd(message);
        
        public Message Dequeue<T>(object obj, CancellationToken token) => Queue[Index.IndexOfValue(obj)].Take(token);

        public void Enqueue(Type type, Message message)
        {
            Queue[Index.IndexOfValue(type)].Add(message);
        }

        public Message Dequeue(object obj, CancellationToken token) => Queue[Index.IndexOfValue(obj)].Take(token);
        #endregion
    }
}
