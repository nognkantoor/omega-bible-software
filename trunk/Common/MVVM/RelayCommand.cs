using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;

namespace Common.Core.MVVM
{
    #region Generic RelayCommand

    /// <summary>
    /// Implements the ICommand interface by providing constructors
    /// with Execute and CanExecute method delegates as parameters.
    /// Execute is an Action&lt;T&gt; and CanExecute
    /// is Predicate&lt;T,bool&gt; (or returns true by default,
    /// if not given)
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        #region Protected properties

        /// <summary>
        /// Gets or sets the execution action.
        /// </summary>
        protected Action<T> ExecuteAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the predicate for CanExecute method.
        /// </summary>
        protected Predicate<T> CanExecutePredicate
        {
            get;
            set;
        }

        #endregion Protected properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RelayCommand with
        /// an Execute action. The CanExecute method will
        /// always return true in this case.
        /// </summary>
        /// <param name="execute">Action for executing for the command.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand with
        /// an Execute action and predicate for CanExecute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute action must not be null");

            ExecuteAction = execute;
            CanExecutePredicate = canExecute;
        }

        #endregion Constructors

        #region ICommand Members

        /// <summary>
        /// Returns the value indicating whether this command is able to execute.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if this command can be executed, false otherwise.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return CanExecutePredicate == null ? true : CanExecutePredicate((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the command action.
        /// </summary>
        /// <param name="parameter">Parameter for the action execution.</param>
        public void Execute(object parameter)
        {
            ExecuteAction((T)parameter);
        }

        #endregion ICommand Members
    }

    #endregion Generic RelayCommand

    #region Typeles RelayCommand

    /// <summary>
    /// Implements the ICommand interface by providing constructors
    /// with Execute and CanExecute method delegates as parameters.
    /// Execute is an Action&lt;object&gt; and CanExecute
    /// is Predicate&lt;object,bool&gt; (or returns true by default,
    /// if not given)
    /// </summary>
    public class RelayCommand : RelayCommand<object>
    {
        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : base(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            : base(execute,canExecute)
        {
        }

        #endregion Constructors
    }

    #endregion Typeles RelayCommand
}
