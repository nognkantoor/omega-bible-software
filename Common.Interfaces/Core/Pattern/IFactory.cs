using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces.Core.Pattern
{
    public interface IFactory
    {
        TObject Create<TObject>(params object [] parameters);
        void Register<TBase, TImplementation>(params Type [] constructorParamTypes);
        void Register(Type baseType, Type implementationType, params Type[] constructorParamTypes);
    }
}
