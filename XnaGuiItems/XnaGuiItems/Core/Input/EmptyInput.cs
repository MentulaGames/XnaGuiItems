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
    using Input;
    using System;
    using System.Collections.Generic;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal sealed class EmptyInput
    {
        public bool CapsLock { get; private set; }
        public bool Numlock { get; private set; }

        private const int HOLD_MILI = 250, SHIFT_MILI = 50;

        private MentulaKeyboardState keys;
        private Dictionary<Keys, DateTime> downs;

        public EmptyInput()
        {
            keys = new MentulaKeyboardState();
            downs = new Dictionary<Keys, DateTime>();

            SetCaps(NativeMethods.CapsLockState() ? KeyState.Down : KeyState.Up);
            SetNum(NativeMethods.NumLockState() ? KeyState.Down : KeyState.Up);
        }

        public void SetWait(Keys key, KeyState state)
        {
            if (state == KeyState.Down) keys.SetDown(key);
            else keys.SetUp(key);

            bool contains = downs.ContainsKey(key);
            if (state == KeyState.Down && !contains) downs.Add(key, DateTime.UtcNow);
            else if (state == KeyState.Up && contains) downs.Remove(key);
        }

        public void SetShift(KeyState state)
        {
            DateTime time = DateTime.UtcNow;

            if (state == KeyState.Down)
            {
                keys.SetDown(Keys.LeftShift);
                downs.AddSet(Keys.LeftShift, time);
            }
            else if (downs.ContainsKey(Keys.LeftShift))
            {
                TimeSpan delta = time - downs[Keys.LeftShift];
                if (delta.Milliseconds > SHIFT_MILI)
                {
                    keys.SetUp(Keys.LeftShift);
                    downs.Remove(Keys.LeftShift);
                }
            }
        }

        public void SetCaps(KeyState state)
        {
            if (state == KeyState.Down)
            {
                if (keys.IsUp(Keys.CapsLock)) CapsLock = !CapsLock;
                keys.SetDown(Keys.CapsLock);
            }
            else keys.SetUp(Keys.CapsLock);
        }

        public void SetNum(KeyState state)
        {
            if (state == KeyState.Down)
            {
                if (keys.IsUp(Keys.NumLock)) Numlock = !Numlock;
                keys.SetDown(Keys.NumLock);
            }
            else keys.SetUp(Keys.NumLock);
        }

        public bool IsDown(Keys key) => keys.IsDown(key);
        public bool IsUp(Keys key) => keys.IsUp(key);

        public bool ShiftDown() => IsDown(Keys.LeftShift);
        public bool CtrlDown() => IsDown(Keys.LeftControl) || keys.IsDown(Keys.RightControl);

        public bool RepeatKey(Keys key)
        {
            if (!downs.ContainsKey(key)) return false;

            TimeSpan delta = DateTime.UtcNow - downs[key];
            bool result = delta.Milliseconds > HOLD_MILI;

            if (result) downs[key] = downs[key].AddMilliseconds(50);
            return result;
        }
    }
}