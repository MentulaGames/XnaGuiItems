namespace Mentula.GuiItems.Core.Interfaces
{
    using Microsoft.Xna.Framework.Input;

    internal interface IDeltaUpdate
    {
        void Update(MouseState mState, float delta);
    }
}