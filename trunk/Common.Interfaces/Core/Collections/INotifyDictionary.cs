using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces.Core.Collections
{
    public interface INotifyDictionary<TKey,TResult> : IDictionary<TKey,TResult>
    {
        void NotifyIndexerChanged();
    }
}
