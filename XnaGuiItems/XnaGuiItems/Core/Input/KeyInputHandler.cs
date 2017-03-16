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
    using System;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal class KeyInputHandler
    {
        public string keyboadString;

        private EmptyInput clsInput;
        private KeyboardState state;
        private bool shift, ctrl;

        public KeyInputHandler()
        {
            keyboadString = string.Empty;
            clsInput = new EmptyInput();
            state = new KeyboardState();
            shift = false;
        }

        public string GetInputString(KeyboardState kstate, bool allowReturn, int maxLength, out bool confirmed, InputFlags flags)
        {
            clsInput.SetShift(kstate[Keys.LeftShift] | kstate[Keys.RightShift]);
            clsInput.SetCaps(kstate[Keys.CapsLock]);
            clsInput.SetNum(kstate[Keys.NumLock]);

            state = kstate;
            shift = clsInput.ShiftDown();
            if (clsInput.CapsLock) shift = !shift;
            ctrl = clsInput.CtrlDown();
            confirmed = false;

            if (keyboadString.Length < maxLength || maxLength < 0)
            {
                if ((flags & InputFlags.NO_SPACE) == 0) HandleKey(Keys.Space, ' ', ' ');

                if ((flags & InputFlags.NO_CHARS) == 0)
                {
                    HandleKey(Keys.A, 'a', 'A');
                    HandleKey(Keys.B, 'b', 'B');
                    HandleKey(Keys.C, 'c', 'C');
                    HandleKey(Keys.D, 'd', 'D');
                    HandleKey(Keys.E, 'e', 'E');
                    HandleKey(Keys.F, 'f', 'F');
                    HandleKey(Keys.G, 'g', 'G');
                    HandleKey(Keys.H, 'h', 'H');
                    HandleKey(Keys.I, 'i', 'I');
                    HandleKey(Keys.J, 'j', 'J');
                    HandleKey(Keys.K, 'k', 'K');
                    HandleKey(Keys.L, 'l', 'L');
                    HandleKey(Keys.M, 'm', 'M');
                    HandleKey(Keys.N, 'n', 'N');
                    HandleKey(Keys.O, 'o', 'O');
                    HandleKey(Keys.P, 'p', 'P');
                    HandleKey(Keys.Q, 'q', 'Q');
                    HandleKey(Keys.R, 'r', 'R');
                    HandleKey(Keys.S, 's', 'S');
                    HandleKey(Keys.T, 't', 'T');
                    HandleKey(Keys.U, 'u', 'U');
                    HandleKey(Keys.V, 'v', 'V', Ctrl_V);
                    HandleKey(Keys.W, 'w', 'W');
                    HandleKey(Keys.X, 'x', 'X');
                    HandleKey(Keys.Y, 'y', 'Y');
                    HandleKey(Keys.Z, 'z', 'Z');
                }

                bool spec = (flags & InputFlags.NO_SPEC) == 0;
                if ((flags & InputFlags.NO_NUMS) == 0)
                {
                    HandleKey(Keys.D0, '0', spec ? ')' : '\0');
                    HandleKey(Keys.D1, '1', spec ? '!' : '\0');
                    HandleKey(Keys.D2, '2', spec ? '@' : '\0');
                    HandleKey(Keys.D3, '3', spec ? '#' : '\0');
                    HandleKey(Keys.D4, '4', spec ? '$' : '\0');
                    HandleKey(Keys.D5, '5', spec ? '%' : '\0');
                    HandleKey(Keys.D6, '6', spec ? '^' : '\0');
                    HandleKey(Keys.D7, '7', spec ? '&' : '\0');
                    HandleKey(Keys.D8, '8', spec ? '*' : '\0');
                    HandleKey(Keys.D9, '9', spec ? '(' : '\0');

                    if (clsInput.Numlock)
                    {
                        HandleKey(Keys.NumPad0, '0', '0');
                        HandleKey(Keys.NumPad1, '1', '1');
                        HandleKey(Keys.NumPad2, '2', '2');
                        HandleKey(Keys.NumPad3, '3', '3');
                        HandleKey(Keys.NumPad4, '4', '4');
                        HandleKey(Keys.NumPad5, '5', '5');
                        HandleKey(Keys.NumPad6, '6', '6');
                        HandleKey(Keys.NumPad7, '7', '7');
                        HandleKey(Keys.NumPad8, '8', '8');
                        HandleKey(Keys.NumPad9, '9', '9');
                    }
                }

                if (spec)
                {
                    HandleKey(Keys.OemSemicolon, ';', ':');
                    HandleKey(Keys.OemQuotes, '\'', '"');
                    HandleKey(Keys.OemComma, ',', '<');
                    HandleKey(Keys.OemPeriod, '.', '>');
                    HandleKey(Keys.OemQuestion, '/', '?');
                    HandleKey(Keys.OemOpenBrackets, '[', '{');
                    HandleKey(Keys.OemCloseBrackets, ']', '}');
                    HandleKey(Keys.OemPipe, '\\', '|');
                    HandleKey(Keys.OemTilde, '`', '~');
                    HandleKey(Keys.OemMinus, '-', '_');
                    HandleKey(Keys.OemPlus, '=', '+');
                }
            }
            else if (keyboadString.Length > maxLength) keyboadString = keyboadString.Remove(maxLength);

            if (kstate.IsKeyDown(Keys.Back))
            {
                if (clsInput.IsUp(Keys.Back))
                {
                    clsInput.SetWait(Keys.Back, KeyState.Down);
                    if (!string.IsNullOrEmpty(keyboadString)) RemoveLastChar();
                }
                else if (clsInput.RepeatKey(Keys.Back) && !string.IsNullOrEmpty(keyboadString)) RemoveLastChar();
            }
            else if (clsInput.IsDown(Keys.Back)) clsInput.SetWait(Keys.Back, KeyState.Up);

            if (kstate.IsKeyDown(Keys.Enter))
            {
                if (clsInput.IsUp(Keys.Enter))
                {
                    clsInput.SetWait(Keys.Enter, KeyState.Down);
                    if (allowReturn) keyboadString += '\n';
                    else confirmed = true;
                }
                else if (clsInput.RepeatKey(Keys.Enter) && allowReturn) keyboadString += '\n';
            }
            else if (clsInput.IsDown(Keys.Enter)) clsInput.SetWait(Keys.Enter, KeyState.Up);

            return keyboadString;
        }

        private void AddChar(char lKey, char uKey) => keyboadString += shift ? uKey : lKey;
        private void RemoveLastChar() => keyboadString = keyboadString.Remove(keyboadString.Length - 1);

        private void HandleKey(Keys key, char lKey, char uKey, Action onCtrl = null)
        {
            if (state.IsKeyDown(key))
            {
                if (ctrl && onCtrl != null) onCtrl();
                else if (clsInput.IsUp(key))
                {
                    clsInput.SetWait(key, KeyState.Down);
                    AddChar(lKey, uKey);
                }
                else if (clsInput.RepeatKey(key)) AddChar(lKey, uKey);
            }
            else if (clsInput.IsDown(key)) clsInput.SetWait(key, KeyState.Up);
        }

        private void Ctrl_V()
        {
            Utilities.RunInSTAThread(() =>
            {
                keyboadString += System.Windows.Forms.Clipboard.GetText(System.Windows.Forms.TextDataFormat.UnicodeText);
            });
        }
    }
}