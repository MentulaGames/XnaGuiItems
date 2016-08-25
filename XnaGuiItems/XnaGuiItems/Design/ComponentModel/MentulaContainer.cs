using System;
using System.ComponentModel;

namespace Mentula.GuiItems.Design.ComponentModel
{
    internal class MentulaContainer : IContainer
    {
        public bool Disposed { get; private set; }
        public ComponentCollection Components { get { return new ComponentCollection(_base); } }

        private MentulaComponent[] _base;

        public MentulaContainer()
        {
            _base = new MentulaComponent[0];
        }

        public MentulaContainer(MentulaComponent[] components)
        {
            _base = components;
        }

        public void Add(IComponent component)
        {
            Add(component, component?.Site?.Name ?? string.Empty);
        }

        public void Add(IComponent component, string name)
        {
            int i = _base.Length;
            Array.Resize(ref _base, i + 1);
            component.Site.Name = name;
            _base[i] = new MentulaComponent(component);
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                for (int i = 0; i < _base.Length; i++)
                {
                    _base[i].Dispose();
                }

                _base = new MentulaComponent[0];
                Disposed = true;
            }
        }

        public void Remove(IComponent component)
        {
            int newLength = _base.Length - 1;
            int i = IndexOf(new MentulaComponent(component));

            if (i == -1) return;
            if (i != newLength)
            {
                MentulaComponent last = _base[newLength];
                _base[i] = last;
            }

            Array.Resize(ref _base, newLength);
        }

        private int IndexOf(MentulaComponent comp)
        {
            int i = -1;
            for (int j = 0; j < _base.Length; j++)
            {
                if (_base[i] == comp) i = j;
            }

            return i;
        }
    }
}
