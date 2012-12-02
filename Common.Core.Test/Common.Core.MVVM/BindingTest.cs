using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.Core.MVVM;

namespace Common.Core.Test
{
    [TestFixture]
    public class BindingTest
    {
        [Test]
        public void BindingPathTest()
        { 
            var MyTopObject = new { PropertyC = "YES!!" };
            var MySecondObject = new { PropertyB = MyTopObject };
            var MyFirstObject = new { PropertyA = MySecondObject };
            Binding binding = new Binding("PropertyA.PropertyB.PropertyC", MyFirstObject);
            Assert.AreEqual("YES!!", binding.Value);
        }

        [Test]
        public void BindingPathWithPropertyBChangedTest()
        {
            ClassA a = new ClassA();
            a.PropertyA.PropertyB.PropertyC = "The last property";
            Binding binding = new Binding("PropertyA.PropertyB.PropertyC", a);
            Assert.AreEqual("The last property", binding.Value);

            a.PropertyA.PropertyB = new ClassC() { PropertyC = "Change here" };
            Assert.AreEqual("Change here", binding.Value);

            a.PropertyA.PropertyB.PropertyC = "Again!";
            Assert.AreEqual("Again!", binding.Value);

            a.PropertyA = new ClassB() { PropertyB = new ClassC() { PropertyC = "The last test" } };
            Assert.AreEqual("The last test", binding.Value);
        }
    }

    public class ClassA : ViewModelBase
    {
        private ClassB _propertyA;
        public ClassA()
        {
            PropertyA = new ClassB();
        }

        public ClassB PropertyA
        {
            get { return _propertyA; }
            set
            {
                _propertyA = value;
                OnPropertyChanged("PropertyA");
            }
        }
    }

    public class ClassB : ViewModelBase
    {
        private ClassC _propertyB;

        public ClassB()
        {
            PropertyB = new ClassC();
        }

        public ClassC PropertyB
        {
            get { return _propertyB; }
            set
            {
                _propertyB = value;
                OnPropertyChanged("PropertyB");
            }
        }
    }

    public class ClassC :ViewModelBase
    {
        private string _propertyC;

        public ClassC()
        {
            PropertyC = "The last property";
        }

        public string PropertyC
        {
            get { return _propertyC; }
            set
            {
                _propertyC = value;
                OnPropertyChanged("PropertyC");
            }
        }
    }
}
