using Microsoft.Xna.Framework.Input;

namespace Mentula.GuiItems.Core.Interfaces
{
    internal interface IDeltaKeyboardUpdate
    {
        void Update(MouseState mState, KeyboardState kState, float delta);
    }
}