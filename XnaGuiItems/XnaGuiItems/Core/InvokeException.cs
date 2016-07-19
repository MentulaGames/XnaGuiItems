using System;
using System.Reflection;

namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// Represents errors that occur during event invoking.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    [Serializable]
    public class InvokeException : Exception
    {
        /// <summary>
        /// Gets a string representation of the immediate frames on the call stack.
        /// </summary>
        public override string StackTrace { get { return stackTrace + base.StackTrace; } }

        private string stackTrace;

        internal InvokeException(TargetInvocationException e)
            :base(e.InnerException.Message, e.InnerException.InnerException)
        {
            CreateStackTrace(e);
        }

        private void CreateStackTrace(TargetInvocationException e)
        {
            stackTrace = e.InnerException.StackTrace;
            stackTrace += Environment.NewLine;
            stackTrace += e.StackTrace;
            stackTrace += Environment.NewLine;
        }
    }
}