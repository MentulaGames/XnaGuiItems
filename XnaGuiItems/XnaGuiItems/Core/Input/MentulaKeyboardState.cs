#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.Input
{
#if MONO
    using Mono::Microsoft.Xna.Framework.Input;
#else
    using Xna::Microsoft.Xna.Framework.Input;
#endif

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal struct MentulaKeyboardState
    {
        public uint _0, _1, _2, _3, _4, _5, _6, _7;

        public bool this[Keys key] { get { return IsDown(key); } }

        public void Clear()
        {
            _0 = 0x0;
            _1 = 0x0;
            _2 = 0x0;
            _3 = 0x0;
            _4 = 0x0;
            _5 = 0x0;
            _6 = 0x0;
            _7 = 0x0;
        }

        public bool IsDown(Keys key)
        {
            uint mask = (uint)1 << (((int)key) & 0x1F);
            uint element;

            switch ((uint)key >> 5)
            {
                case (0): element = _0; break;
                case (1): element = _1; break;
                case (2): element = _2; break;
                case (3): element = _3; break;
                case (4): element = _4; break;
                case (5): element = _5; break;
                case (6): element = _6; break;
                case (7): element = _7; break;
                default: element = 0x0; break;
            }

            return (element & mask) != 0;
        }

        public bool IsUp(Keys key) => !IsDown(key);

        public void SetDown(Keys key)
        {
            uint mask = (uint)1 << (((int)key) & 0x1F);
            switch ((uint)key >> 5)
            {
                case (0): _0 |= mask; break;
                case (1): _1 |= mask; break;
                case (2): _2 |= mask; break;
                case (3): _3 |= mask; break;
                case (4): _4 |= mask; break;
                case (5): _5 |= mask; break;
                case (6): _6 |= mask; break;
                case (7): _7 |= mask; break;
            }
        }

        public void SetUp(Keys key)
        {
            uint mask = ~((uint)1 << (((int)key) & 0x1F));
            switch ((uint)key >> 5)
            {
                case (0): _0 &= mask; break;
                case (1): _1 &= mask; break;
                case (2): _2 &= mask; break;
                case (3): _3 &= mask; break;
                case (4): _4 &= mask; break;
                case (5): _5 &= mask; break;
                case (6): _6 &= mask; break;
                case (7): _7 &= mask; break;
            }
        }
    }
}