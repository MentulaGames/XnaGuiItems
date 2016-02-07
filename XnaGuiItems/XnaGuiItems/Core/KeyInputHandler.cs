using Microsoft.Xna.Framework.Input;

namespace Mentula.GuiItems.Core
{
    internal class KeyInputHandler
    {
        public string keyboadString;
        private EmptyInput clsInput;
        private bool caps;

        public KeyInputHandler()
        {
            keyboadString = "";
            clsInput = new EmptyInput();
        }

        public string GetInputString(KeyboardState state, bool allowReturn)
        {
            Keys[] keys = state.GetPressedKeys();
            bool shift = clsInput.IsShiftPressed(keys) || caps;
            bool ctrl = clsInput.IsControlPressed(keys);

            if (state.IsKeyDown(Keys.A))
            {
                if (clsInput.A_KeyState == KeyState.Up) clsInput.A_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.A)) keyboadString += shift ? 'A' : 'a';
            }
            else if (state.IsKeyUp(Keys.A))
            {
                if (clsInput.A_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'A' : 'a';
                    clsInput.A_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.B))
            {
                if (clsInput.B_KeyState == KeyState.Up) clsInput.B_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.B)) keyboadString += shift ? 'B' : 'b';
            }
            else if (state.IsKeyUp(Keys.B))
            {
                if (clsInput.B_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'B' : 'b';
                    clsInput.B_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.C))
            {
                if (clsInput.C_KeyState == KeyState.Up) clsInput.C_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.C)) keyboadString += shift ? 'C' : 'c';
            }
            else if (state.IsKeyUp(Keys.C))
            {
                if (clsInput.C_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'C' : 'c';
                    clsInput.C_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D))
            {
                if (clsInput.D_KeyState == KeyState.Up) clsInput.D_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D)) keyboadString += shift ? 'D' : 'd';
            }
            else if (state.IsKeyUp(Keys.D))
            {
                if (clsInput.D_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'D' : 'd';
                    clsInput.D_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.E))
            {
                if (clsInput.E_KeyState == KeyState.Up) clsInput.E_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.E)) keyboadString += shift ? 'E' : 'e';
            }
            else if (state.IsKeyUp(Keys.E))
            {
                if (clsInput.E_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'E' : 'e';
                    clsInput.E_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.F))
            {
                if (clsInput.F_KeyState == KeyState.Up) clsInput.F_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.F)) keyboadString += shift ? 'F' : 'f';
            }
            else if (state.IsKeyUp(Keys.F))
            {
                if (clsInput.F_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'F' : 'f';
                    clsInput.F_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.G))
            {
                if (clsInput.G_KeyState == KeyState.Up) clsInput.G_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.G)) keyboadString += shift ? 'G' : 'g';
            }
            else if (state.IsKeyUp(Keys.G))
            {
                if (clsInput.G_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'G' : 'g';
                    clsInput.G_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.H))
            {
                if (clsInput.H_KeyState == KeyState.Up) clsInput.H_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.H)) keyboadString += shift ? 'H' : 'h';
            }
            else if (state.IsKeyUp(Keys.H))
            {
                if (clsInput.H_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'H' : 'h';
                    clsInput.H_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.I))
            {
                if (clsInput.I_KeyState == KeyState.Up) clsInput.I_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.I)) keyboadString += shift ? 'I' : 'i';
            }
            else if (state.IsKeyUp(Keys.I))
            {
                if (clsInput.I_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'I' : 'i';
                    clsInput.I_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.J))
            {
                if (clsInput.J_KeyState == KeyState.Up) clsInput.J_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.J)) keyboadString += shift ? 'J' : 'j';
            }
            else if (state.IsKeyUp(Keys.J))
            {
                if (clsInput.J_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'J' : 'j';
                    clsInput.J_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.K))
            {
                if (clsInput.K_KeyState == KeyState.Up) clsInput.K_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.K)) keyboadString += shift ? 'K' : 'k';
            }
            else if (state.IsKeyUp(Keys.K))
            {
                if (clsInput.K_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'K' : 'k';
                    clsInput.K_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.L))
            {
                if (clsInput.L_KeyState == KeyState.Up) clsInput.L_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.L)) keyboadString += shift ? 'L' : 'l';
            }
            else if (state.IsKeyUp(Keys.L))
            {
                if (clsInput.L_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'L' : 'l';
                    clsInput.L_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.M))
            {
                if (clsInput.M_KeyState == KeyState.Up) clsInput.M_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.M)) keyboadString += shift ? 'M' : 'm';
            }
            else if (state.IsKeyUp(Keys.M))
            {
                if (clsInput.M_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'M' : 'm';
                    clsInput.M_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.N))
            {
                if (clsInput.N_KeyState == KeyState.Up) clsInput.N_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.N)) keyboadString += shift ? 'N' : 'n';
            }
            else if (state.IsKeyUp(Keys.N))
            {
                if (clsInput.N_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'N' : 'n';
                    clsInput.N_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.O))
            {
                if (clsInput.O_KeyState == KeyState.Up) clsInput.O_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.O)) keyboadString += shift ? 'O' : 'o';
            }
            else if (state.IsKeyUp(Keys.O))
            {
                if (clsInput.O_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'O' : 'o';
                    clsInput.O_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.P))
            {
                if (clsInput.P_KeyState == KeyState.Up) clsInput.P_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.P)) keyboadString += shift ? 'P' : 'p';
            }
            else if (state.IsKeyUp(Keys.P))
            {
                if (clsInput.P_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'P' : 'p';
                    clsInput.P_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.Q))
            {
                if (clsInput.Q_KeyState == KeyState.Up) clsInput.Q_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.Q)) keyboadString += shift ? 'Q' : 'q';
            }
            else if (state.IsKeyUp(Keys.Q))
            {
                if (clsInput.Q_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'Q' : 'q';
                    clsInput.Q_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.R))
            {
                if (clsInput.R_KeyState == KeyState.Up) clsInput.R_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.R)) keyboadString += shift ? 'R' : 'r';
            }
            else if (state.IsKeyUp(Keys.R))
            {
                if (clsInput.R_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'R' : 'r';
                    clsInput.R_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.S))
            {
                if (clsInput.S_KeyState == KeyState.Up) clsInput.S_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.S)) keyboadString += shift ? 's' : 's';
            }
            else if (state.IsKeyUp(Keys.S))
            {
                if (clsInput.S_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'S' : 's';
                    clsInput.S_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.T))
            {
                if (clsInput.T_KeyState == KeyState.Up) clsInput.T_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.T)) keyboadString += shift ? 'T' : 't';
            }
            else if (state.IsKeyUp(Keys.T))
            {
                if (clsInput.T_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'T' : 't';
                    clsInput.T_KeyState = KeyState.Up;
                }
            }
            if (state.IsKeyDown(Keys.U))
            {
                if (clsInput.U_KeyState == KeyState.Up) clsInput.U_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.U)) keyboadString += shift ? 'U' : 'u';
            }
            else if (state.IsKeyUp(Keys.U))
            {
                if (clsInput.U_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'U' : 'u';
                    clsInput.U_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.V))
            {
                if (ctrl)
                {
                    Utilities.RunInSTAThread(() =>
                    {
                        keyboadString += System.Windows.Forms.Clipboard.GetText(System.Windows.Forms.TextDataFormat.UnicodeText);
                    });
                }
                else if (clsInput.V_KeyState == KeyState.Up) clsInput.V_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.V)) keyboadString += shift ? 'V' : 'v';
            }
            else if (state.IsKeyUp(Keys.V))
            {
                if (clsInput.V_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'V' : 'v';
                    clsInput.V_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.W))
            {
                if (clsInput.W_KeyState == KeyState.Up) clsInput.W_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.W)) keyboadString += shift ? 'W' : 'w';
            }
            else if (state.IsKeyUp(Keys.W))
            {
                if (clsInput.W_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'W' : 'w';
                    clsInput.W_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.X))
            {
                if (clsInput.X_KeyState == KeyState.Up) clsInput.X_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.X)) keyboadString += shift ? 'X' : 'x';
            }
            else if (state.IsKeyUp(Keys.X))
            {
                if (clsInput.X_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'X' : 'x';
                    clsInput.X_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.Y))
            {
                if (clsInput.Y_KeyState == KeyState.Up) clsInput.Y_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.Y)) keyboadString += shift ? 'Y' : 'y';
            }
            else if (state.IsKeyUp(Keys.Y))
            {
                if (clsInput.Y_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'Y' : 'y';
                    clsInput.Y_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.Z))
            {
                if (clsInput.Z_KeyState == KeyState.Up) clsInput.Z_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.Z)) keyboadString += shift ? 'Z' : 'z';
            }
            else if (state.IsKeyUp(Keys.Z))
            {
                if (clsInput.Z_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? 'Z' : 'z';
                    clsInput.Z_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.CapsLock))
            {
                if (clsInput.Caps_KeyState == KeyState.Up) clsInput.Caps_KeyState = KeyState.Down;
            }
            else if (state.IsKeyUp(Keys.CapsLock))
            {
                if (clsInput.Caps_KeyState == KeyState.Down)
                {
                    caps = !caps;
                    clsInput.Caps_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.Space))
            {
                if (clsInput.Space_KeyState == KeyState.Up) clsInput.Space_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.Space)) keyboadString += " ";
            }
            else if (state.IsKeyUp(Keys.Space))
            {
                if (clsInput.Space_KeyState == KeyState.Down)
                {
                    keyboadString += " ";
                    clsInput.Space_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.Back))
            {
                if (clsInput.Back_KeyState == KeyState.Up) clsInput.Back_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.Back) && (keyboadString != "")) keyboadString = keyboadString.Remove(keyboadString.Length - 1);
            }
            else if (state.IsKeyUp(Keys.Back))
            {
                if (clsInput.Back_KeyState == KeyState.Down)
                {
                    if (keyboadString != "") keyboadString = keyboadString.Remove(keyboadString.Length - 1);
                    clsInput.Back_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.Enter))
            {
                if (clsInput.Return_KeyState == KeyState.Up) clsInput.Return_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.Enter)) keyboadString += '\n';
            }
            else if (state.IsKeyUp(Keys.Enter))
            {
                if (clsInput.Return_KeyState == KeyState.Down)
                {
                    if (allowReturn) keyboadString += '\n';
                    clsInput.Return_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemSemicolon))
            {
                if (clsInput.Oem_SemiColon_KeyState == KeyState.Up) clsInput.Oem_SemiColon_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemSemicolon)) keyboadString += shift ? ':' : ';';
            }
            else if (state.IsKeyUp(Keys.OemSemicolon))
            {
                if (clsInput.Oem_SemiColon_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? ':' : ';';
                    clsInput.Oem_SemiColon_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemQuotes))
            {
                if (clsInput.Oem_Quotes_KeyState == KeyState.Up) clsInput.Oem_Quotes_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemQuotes)) keyboadString += shift ? '"' : '\'';
            }
            else if (state.IsKeyUp(Keys.OemQuotes))
            {
                if (clsInput.Oem_Quotes_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '"' : '\'';
                    clsInput.Oem_Quotes_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemComma))
            {
                if (clsInput.Oem_Comma_KeyState == KeyState.Up) clsInput.Oem_Comma_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemComma)) keyboadString += shift ? '<' : ',';
            }
            else if (state.IsKeyUp(Keys.OemComma))
            {
                if (clsInput.Oem_Comma_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '<' : ',';
                    clsInput.Oem_Comma_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemPeriod))
            {
                if (clsInput.Oem_Period_KeyState == KeyState.Up) clsInput.Oem_Period_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemPeriod)) keyboadString += shift ? '>' : '.';
            }
            else if (state.IsKeyUp(Keys.OemPeriod))
            {
                if (clsInput.Oem_Period_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '>' : '.';
                    clsInput.Oem_Period_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemQuestion))
            {
                if (clsInput.Oem_Question_KeyState == KeyState.Up) clsInput.Oem_Question_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemQuestion)) keyboadString += shift ? '?' : '/';
            }
            else if (state.IsKeyUp(Keys.OemQuestion))
            {
                if (clsInput.Oem_Question_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '?' : '/';
                    clsInput.Oem_Question_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemOpenBrackets))
            {
                if (clsInput.Oem_OpenBracket_KeyState == KeyState.Up) clsInput.Oem_OpenBracket_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemOpenBrackets)) keyboadString += shift ? '{' : '[';
            }
            else if (state.IsKeyUp(Keys.OemOpenBrackets))
            {
                if (clsInput.Oem_OpenBracket_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '{' : '[';
                    clsInput.Oem_OpenBracket_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemCloseBrackets))
            {
                if (clsInput.Oem_CloseBracket_KeyState == KeyState.Up) clsInput.Oem_CloseBracket_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemCloseBrackets)) keyboadString += shift ? '}' : ']';
            }
            else if (state.IsKeyUp(Keys.OemCloseBrackets))
            {
                if (clsInput.Oem_CloseBracket_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '}' : ']';
                    clsInput.Oem_CloseBracket_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemPipe))
            {
                if (clsInput.Oem_Pipe_KeyState == KeyState.Up) clsInput.Oem_Pipe_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemPipe)) keyboadString += shift ? '|' : '\\';
            }
            else if (state.IsKeyUp(Keys.OemPipe))
            {
                if (clsInput.Oem_Pipe_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '|' : '\\';
                    clsInput.Oem_Pipe_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemTilde))
            {
                if (clsInput.Oem_Tilde_KeyState == KeyState.Up) clsInput.Oem_Tilde_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemTilde)) keyboadString += shift ? '~' : '`';
            }
            else if (state.IsKeyUp(Keys.OemTilde))
            {
                if (clsInput.Oem_Tilde_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '~' : '`';
                    clsInput.Oem_Tilde_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemMinus))
            {
                if (clsInput.Oem_Minus_KeyState == KeyState.Up) clsInput.Oem_Minus_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemMinus)) keyboadString += shift ? '_' : '-';
            }
            else if (state.IsKeyUp(Keys.OemMinus))
            {
                if (clsInput.Oem_Minus_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '_' : '-';
                    clsInput.Oem_Minus_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.OemPlus))
            {
                if (clsInput.Oem_Plus_KeyState == KeyState.Up) clsInput.Oem_Plus_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.OemPlus)) keyboadString += shift ? '+' : '=';
            }
            else if (state.IsKeyUp(Keys.OemPlus))
            {
                if (clsInput.Oem_Plus_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '+' : '=';
                    clsInput.Oem_Plus_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D1))
            {
                if (clsInput.D1_KeyState == KeyState.Up) clsInput.D1_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D1)) keyboadString += shift ? '!' : '1';
            }
            else if (state.IsKeyUp(Keys.D1))
            {
                if (clsInput.D1_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '!' : '1';
                    clsInput.D1_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D2))
            {
                if (clsInput.D2_KeyState == KeyState.Up) clsInput.D2_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D2)) keyboadString += shift ? '@' : '2';
            }
            else if (state.IsKeyUp(Keys.D2))
            {
                if (clsInput.D2_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '@' : '2';
                    clsInput.D2_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D3))
            {
                if (clsInput.D3_KeyState == KeyState.Up) clsInput.D3_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D3)) keyboadString += shift ? '#' : '3';
            }
            else if (state.IsKeyUp(Keys.D3))
            {
                if (clsInput.D3_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '#' : '3';
                    clsInput.D3_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D4))
            {
                if (clsInput.D4_KeyState == KeyState.Up) clsInput.D4_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D4)) keyboadString += shift ? '$' : '4';
            }
            else if (state.IsKeyUp(Keys.D4))
            {
                if (clsInput.D4_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '$' : '4';
                    clsInput.D4_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D5))
            {
                if (clsInput.D5_KeyState == KeyState.Up) clsInput.D5_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D5)) keyboadString += shift ? '%' : '5';
            }
            else if (state.IsKeyUp(Keys.D5))
            {
                if (clsInput.D5_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '%' : '5';
                    clsInput.D5_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D6))
            {
                if (clsInput.D6_KeyState == KeyState.Up) clsInput.D6_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D6)) keyboadString += shift ? '^' : '6';
            }
            else if (state.IsKeyUp(Keys.D6))
            {
                if (clsInput.D6_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '^' : '6';
                    clsInput.D6_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D7))
            {
                if (clsInput.D7_KeyState == KeyState.Up) clsInput.D7_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D7)) keyboadString += shift ? '&' : '7';
            }
            else if (state.IsKeyUp(Keys.D7))
            {
                if (clsInput.D7_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '&' : '7';
                    clsInput.D7_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D8))
            {
                if (clsInput.D8_KeyState == KeyState.Up) clsInput.D8_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D8)) keyboadString += shift ? '*' : '8';
            }
            else if (state.IsKeyUp(Keys.D8))
            {
                if (clsInput.D8_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '*' : '8';
                    clsInput.D8_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D9))
            {
                if (clsInput.D9_KeyState == KeyState.Up) clsInput.D9_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D9)) keyboadString += shift ? '(' : '9';
            }
            else if (state.IsKeyUp(Keys.D9))
            {
                if (clsInput.D9_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? '(' : '9';
                    clsInput.D9_KeyState = KeyState.Up;
                }
            }

            if (state.IsKeyDown(Keys.D0))
            {
                if (clsInput.D0_KeyState == KeyState.Up) clsInput.D0_KeyState = KeyState.Down;
                else if (clsInput.RepeatKey(Keys.D0)) keyboadString += shift ? ')' : '0';
            }
            else if (state.IsKeyUp(Keys.D0))
            {
                if (clsInput.D0_KeyState == KeyState.Down)
                {
                    keyboadString += shift ? ')' : '0';
                    clsInput.D0_KeyState = KeyState.Up;
                }
            }

            return keyboadString;
        }
    }
}