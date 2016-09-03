using Microsoft.Xna.Framework.Input;

namespace Mentula.GuiItems.Core.Interfaces
{
    internal interface IDeltaUpdate
    {
        void Update(MouseState mState, float delta);
    }
}