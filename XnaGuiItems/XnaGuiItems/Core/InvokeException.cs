using System;
using System.Reflection;
using System.Runtime.Serialization;

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
        private TargetInvocationException ex;

        internal InvokeException(TargetInvocationException e)
            :base(e.InnerException.Message, e.InnerException.InnerException)
        {
            ex = e;
            CreateStackTrace();
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Internal exception", ex);
            base.GetObjectData(info, context);
        }

        private void CreateStackTrace()
        {
            stackTrace = ex.InnerException.StackTrace;
            stackTrace += Environment.NewLine;
            stackTrace += ex.StackTrace;
            stackTrace += Environment.NewLine;
        }
    }
}