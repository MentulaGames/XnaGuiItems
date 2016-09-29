namespace Mentula.GuiItems.Core.Interfaces
{
    using Microsoft.Xna.Framework.Input;

    internal interface IDeltaKeyboardUpdate
    {
        void Update(MouseState mState, KeyboardState kState, float delta);
    }
}