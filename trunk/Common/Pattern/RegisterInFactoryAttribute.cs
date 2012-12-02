using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.Application;
using Common.Core.Pattern;

namespace Common.Core.Pattern
{
    public class RegisterInFactoryAttribute:OnStartupAttribute
    {
        private Type _baseType;
        public RegisterInFactoryAttribute(Type baseType)
        {
            _baseType = baseType;
        }

        public override void OnStartup(Type ownerClass)
        {
            Factory.Instance.Register(_baseType, ownerClass);
        }
    }
}
