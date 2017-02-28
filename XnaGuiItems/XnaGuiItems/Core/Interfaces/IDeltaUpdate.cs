#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.Interfaces
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Input;
#else
    using Xna.Microsoft.Xna.Framework.Input;
#endif

    internal interface IDeltaUpdate
    {
        void Update(MouseState mState, float delta);
    }
}