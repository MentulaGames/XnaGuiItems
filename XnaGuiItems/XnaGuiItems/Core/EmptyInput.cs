namespace Mentula.GuiItems.Core
{
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal sealed class EmptyInput
    {
        public Dictionary<Keys, DateTime> downs = new Dictionary<Keys, DateTime>();

        public KeyState A_KeyState { get { return A; } set { SetDown(Keys.A, value); A = value; } }
        public KeyState B_KeyState { get { return B; } set { SetDown(Keys.B, value); B = value; } }
        public KeyState C_KeyState { get { return C; } set { SetDown(Keys.C, value); C = value; } }
        public KeyState D_KeyState { get { return D; } set { SetDown(Keys.D, value); D = value; } }
        public KeyState E_KeyState { get { return E; } set { SetDown(Keys.E, value); E = value; } }
        public KeyState F_KeyState { get { return F; } set { SetDown(Keys.F, value); F = value; } }
        public KeyState G_KeyState { get { return G; } set { SetDown(Keys.G, value); G = value; } }
        public KeyState H_KeyState { get { return H; } set { SetDown(Keys.H, value); H = value; } }
        public KeyState I_KeyState { get { return I; } set { SetDown(Keys.I, value); I = value; } }
        public KeyState J_KeyState { get { return J; } set { SetDown(Keys.J, value); J = value; } }
        public KeyState K_KeyState { get { return K; } set { SetDown(Keys.K, value); K = value; } }
        public KeyState L_KeyState { get { return L; } set { SetDown(Keys.L, value); L = value; } }
        public KeyState M_KeyState { get { return M; } set { SetDown(Keys.M, value); M = value; } }
        public KeyState N_KeyState { get { return N; } set { SetDown(Keys.N, value); N = value; } }
        public KeyState O_KeyState { get { return O; } set { SetDown(Keys.O, value); O = value; } }
        public KeyState P_KeyState { get { return P; } set { SetDown(Keys.P, value); P = value; } }
        public KeyState Q_KeyState { get { return Q; } set { SetDown(Keys.Q, value); Q = value; } }
        public KeyState R_KeyState { get { return R; } set { SetDown(Keys.R, value); R = value; } }
        public KeyState S_KeyState { get { return S; } set { SetDown(Keys.S, value); S = value; } }
        public KeyState T_KeyState { get { return T; } set { SetDown(Keys.T, value); T = value; } }
        public KeyState U_KeyState { get { return U; } set { SetDown(Keys.U, value); U = value; } }
        public KeyState V_KeyState { get { return V; } set { SetDown(Keys.V, value); V = value; } }
        public KeyState W_KeyState { get { return W; } set { SetDown(Keys.W, value); W = value; } }
        public KeyState X_KeyState { get { return X; } set { SetDown(Keys.X, value); X = value; } }
        public KeyState Y_KeyState { get { return Y; } set { SetDown(Keys.Y, value); Y = value; } }
        public KeyState Z_KeyState { get { return Z; } set { SetDown(Keys.Z, value); Z = value; } }

        public KeyState Caps_KeyState { get; set; }
        public KeyState Space_KeyState { get { return Space; } set { SetDown(Keys.Space, value); Space = value; } }
        public KeyState Back_KeyState { get { return Back; } set { SetDown(Keys.Back, value); Back = value; } }
        public KeyState Return_KeyState { get { return Return; } set { SetDown(Keys.Enter, value); Return = value; } }

        public KeyState Oem_SemiColon_KeyState { get { return Oem_SemiColon; } set { SetDown(Keys.OemSemicolon, value); Oem_SemiColon = value; } }
        public KeyState Oem_Quotes_KeyState { get { return Oem_Quotes; } set { SetDown(Keys.OemQuotes, value); Oem_Quotes = value; } }
        public KeyState Oem_Comma_KeyState { get { return Oem_Comma; } set { SetDown(Keys.OemComma, value); Oem_Comma = value; } }
        public KeyState Oem_Period_KeyState { get { return Oem_Period; } set { SetDown(Keys.OemPeriod, value); Oem_Period = value; } }
        public KeyState Oem_Question_KeyState { get { return Oem_Question; } set { SetDown(Keys.OemQuestion, value); Oem_Question = value; } }

        public KeyState Oem_OpenBracket_KeyState { get { return Oem_OpenBracket; } set { SetDown(Keys.OemOpenBrackets, value); Oem_OpenBracket = value; } }
        public KeyState Oem_CloseBracket_KeyState { get { return Oem_CloseBracket; } set { SetDown(Keys.OemCloseBrackets, value); Oem_CloseBracket = value; } }
        public KeyState Oem_Pipe_KeyState { get { return Oem_Pipe; } set { SetDown(Keys.OemPipe, value); Oem_Pipe = value; } }

        public KeyState Oem_Tilde_KeyState { get { return Oem_Tilde; } set { SetDown(Keys.OemTilde, value); Oem_Tilde = value; } }
        public KeyState Oem_Minus_KeyState { get { return Oem_Minus; } set { SetDown(Keys.OemMinus, value); Oem_Minus = value; } }
        public KeyState Oem_Plus_KeyState { get { return Oem_Plus; } set { SetDown(Keys.OemPlus, value); Oem_Plus = value; } }

        public KeyState D1_KeyState { get { return D1; } set { SetDown(Keys.D1, value); D1 = value; } }
        public KeyState D2_KeyState { get { return D2; } set { SetDown(Keys.D2, value); D2 = value; } }
        public KeyState D3_KeyState { get { return D3; } set { SetDown(Keys.D3, value); D3 = value; } }
        public KeyState D4_KeyState { get { return D4; } set { SetDown(Keys.D4, value); D4 = value; } }
        public KeyState D5_KeyState { get { return D5; } set { SetDown(Keys.D5, value); D5 = value; } }
        public KeyState D6_KeyState { get { return D6; } set { SetDown(Keys.D6, value); D6 = value; } }
        public KeyState D7_KeyState { get { return D7; } set { SetDown(Keys.D7, value); D7 = value; } }
        public KeyState D8_KeyState { get { return D8; } set { SetDown(Keys.D8, value); D8 = value; } }
        public KeyState D9_KeyState { get { return D9; } set { SetDown(Keys.D9, value); D9 = value; } }
        public KeyState D0_KeyState { get { return D0; } set { SetDown(Keys.D0, value); D0 = value; } }

        public KeyState Num0_KeyState { get { return Num0; } set { SetDown(Keys.NumPad0, value); Num0 = value; } }
        public KeyState Num1_KeyState { get { return Num1; } set { SetDown(Keys.NumPad1, value); Num1 = value; } }
        public KeyState Num2_KeyState { get { return Num2; } set { SetDown(Keys.NumPad2, value); Num2 = value; } }
        public KeyState Num3_KeyState { get { return Num3; } set { SetDown(Keys.NumPad3, value); Num3 = value; } }
        public KeyState Num4_KeyState { get { return Num4; } set { SetDown(Keys.NumPad4, value); Num4 = value; } }
        public KeyState Num5_KeyState { get { return Num5; } set { SetDown(Keys.NumPad5, value); Num5 = value; } }
        public KeyState Num6_KeyState { get { return Num6; } set { SetDown(Keys.NumPad6, value); Num6 = value; } }
        public KeyState Num7_KeyState { get { return Num7; } set { SetDown(Keys.NumPad7, value); Num7 = value; } }
        public KeyState Num8_KeyState { get { return Num8; } set { SetDown(Keys.NumPad8, value); Num8 = value; } }
        public KeyState Num9_KeyState { get { return Num9; } set { SetDown(Keys.NumPad9, value); Num9 = value; } }

        private KeyState A;
        private KeyState B;
        private KeyState C;
        private KeyState D;
        private KeyState E;
        private KeyState F;
        private KeyState G;
        private KeyState H;
        private KeyState I;
        private KeyState J;
        private KeyState K;
        private KeyState L;
        private KeyState M;
        private KeyState N;
        private KeyState O;
        private KeyState P;
        private KeyState Q;
        private KeyState R;
        private KeyState S;
        private KeyState T;
        private KeyState U;
        private KeyState V;
        private KeyState W;
        private KeyState X;
        private KeyState Y;
        private KeyState Z;

        private KeyState Space;
        private KeyState Back;
        private KeyState Return;

        private KeyState Oem_SemiColon;
        private KeyState Oem_Quotes;
        private KeyState Oem_Comma;
        private KeyState Oem_Period;
        private KeyState Oem_Question;

        private KeyState Oem_OpenBracket;
        private KeyState Oem_CloseBracket;
        private KeyState Oem_Pipe;

        private KeyState Oem_Tilde;
        private KeyState Oem_Minus;
        private KeyState Oem_Plus;

        private KeyState D1;
        private KeyState D2;
        private KeyState D3;
        private KeyState D4;
        private KeyState D5;
        private KeyState D6;
        private KeyState D7;
        private KeyState D8;
        private KeyState D9;
        private KeyState D0;

        private KeyState Num0;
        private KeyState Num1;
        private KeyState Num2;
        private KeyState Num3;
        private KeyState Num4;
        private KeyState Num5;
        private KeyState Num6;
        private KeyState Num7;
        private KeyState Num8;
        private KeyState Num9;

        public bool IsShiftPressed(Keys[] keys)
        {
            bool result = false;

            for (int i = 0; i < keys.Length; i++)
            {
                Keys k = keys[i];
                if (k == Keys.LeftShift || k == Keys.RightShift)
                {
                    result = true;
                    break;
                }
            }

            if (result) SetDown(Keys.LeftShift, KeyState.Down);
            else
            {
                if (downs.ContainsKey(Keys.LeftShift))
                {
                    DateTime time = DateTime.UtcNow;
                    result = (time - downs[Keys.LeftShift]).Milliseconds < 250;

                    if (!result) SetDown(Keys.LeftShift, KeyState.Up);
                }
            }

            return result;
        }

        public bool IsControlPressed(Keys[] keys)
        {
            bool result = false;

            for (int i = 0; i < keys.Length; i++)
            {
                Keys k = keys[i];
                if (k == Keys.LeftControl || k == Keys.RightControl)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool RepeatKey(Keys key)
        {
            if (downs.ContainsKey(key))
            {
                DateTime time = DateTime.UtcNow;
                bool result = (time - downs[key]).Milliseconds > 250;

                if (result) downs[key] = downs[key].AddMilliseconds(50);
                return result;
            }

            return false;
        }

        private void SetDown(Keys key, KeyState state)
        {
            bool contains = downs.ContainsKey(key);
            DateTime time = DateTime.UtcNow;

            if (state == KeyState.Down && !contains) downs.Add(key, time);
            else if (contains) downs.Remove(key);
        }
    }
}