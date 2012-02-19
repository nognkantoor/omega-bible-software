using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using core=Common.Core;
using interfaces=Common.Interfaces;
using System.Reflection;
using Common.Interfaces.Core.Pattern;

namespace Omega.Tools.Utilities
{
    class Factory : IFactory
    {
        private Factory()
        {
            Types = new Dictionary<Type, Type>();
            Types.Add(typeof(interfaces.Core.Translation.ITranslator), typeof(core.Translation.Translator));
        }

        public Dictionary<Type, Type> Types
        {
            get;
            protected set;
        }

        public TObject Create<TObject>(params object [] parameters)
        {
            Type[] types = Type.EmptyTypes;
            if (parameters != null && parameters.Length > 0)
            {
                types = new Type[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    types[i] = parameters[i].GetType();
                }
            }

            if(Types.ContainsKey(typeof(TObject)))
            {
                Type type = Types[typeof(TObject)];
                ConstructorInfo constructor = type.GetConstructor(types);
                if(constructor != null)
                {
                    TObject result =  (TObject)constructor.Invoke(parameters);
                    return result;
                }
            }

            return default(TObject);
        }
    }
}
