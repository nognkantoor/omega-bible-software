using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Common.Core.MVVM
{
    /// <summary>
    /// This class implements the INotifyPropertyChanged interface.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Static methods

        /// <summary>
        /// Can be invoked by any object imlementing the INotifyPropertyChanged interface.
        /// </summary>
        /// <param name="sender">The sender object implementing INotifyPropertyChanged</param>
        /// <param name="handler">The PropertyChanged handler</param>
        /// <param name="property">Property name.</param>
        public static void InvokePropertyChanged(object sender, PropertyChangedEventHandler handler, string property)
        {
            if (ViewModelBase.PropertyExists(property, sender.GetType()))
            {
                if (handler != null)
                {
                    handler(sender, new PropertyChangedEventArgs(property));
                }
            }
            else
            {
                throw new ArgumentException("Property " + property + " does not exist on object of type " + sender.GetType().Name);
            }
        }

        /// <summary>
        /// Method for checking if the given property is a member of the given type.
        /// </summary>
        /// <param name="property">Property name</param>
        /// <param name="type">Type which supossed to be the owner of the property.</param>
        /// <returns>True if this property is a member of the given type, false otherwise</returns>
        public static bool PropertyExists(string property, Type type)
        {
#if DEBUG
            PropertyInfo propertyInfo = type.GetProperty(property);
            if (propertyInfo == null)
            {
                if (property.EndsWith("[]"))
                {
                    propertyInfo = type.GetProperty(property.Substring(0, property.LastIndexOf("[]")));
                }
            }

            if (propertyInfo == null)
            {
                return false;
            }

            return true;
#else
            return true;
#endif
        }

        #endregion Static methods

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Invokes the notification of change of a property.
        /// In Debug mode it also checkes for the existence of the given
        /// property.
        /// </summary>
        /// <param name="property">Property to notify the change.</param>
        protected void OnPropertyChanged(string property)
        {
            ViewModelBase.InvokePropertyChanged(this, PropertyChanged, property);
        }

        /// <summary>
        /// PropertyChanged event invokes when a property of this instance changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
