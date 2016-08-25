using System;
using System.ComponentModel;

namespace Mentula.GuiItems.Design.ComponentModel
{
    internal class MentulaSite : ISite
    {
        public IComponent Component { get; private set; }
        public IContainer Container { get; private set; }
        public bool DesignMode { get; private set; }
        public string Name { get; set; }

        public MentulaSite(MentulaContainer actvCntr, MentulaComponent prntCmpnt)
        {
            Component = prntCmpnt;
            Container = actvCntr;
            DesignMode = false;
            Name = null;
        }

        public virtual object GetService(Type serviceType)
        {
            return null;
        }
    }
}