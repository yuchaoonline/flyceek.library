using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketCSharpConsoleTest.Lib
{
    public class ObjectPool<T> : IDisposable where T : new()
    {
        protected Stack<T> mObjects = new Stack<T>();
        public ObjectPool(int counts)
        {
            for (int i = 0; i < counts; i++)
            {
                this.mObjects.Push((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
            }
        }
        public virtual T Pop()
        {
            T result;
            lock (this.mObjects)
            {
                if (this.mObjects.Count > 0)
                {
                    result = this.mObjects.Pop();
                }
                else
                {
                    result = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
                }
            }
            return result;
        }
        public virtual void Push(T obj)
        {
            lock (this.mObjects)
            {
                if (obj != null)
                {
                    this.mObjects.Push(obj);
                }
            }
        }
        public void Dispose()
        {
            if (typeof(T) == typeof(IDisposable))
            {
                while (this.mObjects.Count > 0)
                {
                    ((IDisposable)((object)this.mObjects.Pop())).Dispose();
                }
            }
            this.mObjects.Clear();
        }
    }
}
