using System;
using System.ComponentModel;

namespace Mentula.GuiItems.Design.ComponentModel
{
    internal class MentulaComponent : IComponent
    {
        public bool IsDisposed { get; private set; }
        public ISite Site { get; set; }

        public event EventHandler Disposed;

        public MentulaComponent() { }

        public MentulaComponent(IComponent comp)
        {
            Site = comp.Site;
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Utilities.Invoke(Disposed, this, EventArgs.Empty);
                IsDisposed = true;
            }
        }
    }
}