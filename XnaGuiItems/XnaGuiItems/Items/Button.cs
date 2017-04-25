#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Items
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
#endif
    using Core;
    using Core.TextureHandlers;
    using System.Diagnostics.CodeAnalysis;
    using DeJong.Utilities.Core;
    using Core.EventHandlers;
    using static Utilities;
    using static DeJong.Utilities.Core.EventInvoker;

    /// <summary>
    /// A button base class.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class Button : Label
    {
        new private ButtonTextureHandler textures { get { return (ButtonTextureHandler)base.textures; } set { base.textures = value; } }

        private bool leftInvoked;
        private bool rightInvoked;
        private int doubleLeftClicked;
        private int doubleRightClicked;
        private float time;

        /// <summary>
        /// Occurs when the <see cref="Button"/> is left clicked.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event StrongEventHandler<Button, MouseEventArgs> LeftClick;
        /// <summary>
        /// Occurs when the <see cref="Button"/> is right clicked.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event StrongEventHandler<Button, MouseEventArgs> RightClick;
        /// <summary>
        /// Occurs when the <see cref="Button"/> is clicked twice in a short period of time.
        /// </summary>
        [SuppressMessage(CAT_DESIGN, CHECKID_EVENT, Justification = JUST_MOUSE)]
        public event StrongEventHandler<Button, MouseEventArgs> DoubleClick;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with default settings.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(ref SpriteBatch sb, SpriteFont font)
             : this(ref sb, DefaultBounds, font)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with a specified size.
        /// </summary>
        /// <param name="sb"> The <see cref="SpriteBatch"/> used for generating underlying <see cref="Texture2D"/>. </param>
        /// <param name="bounds"> The size of the <see cref="Button"/> in pixels. </param>
        /// <param name="font"> The <see cref="SpriteFont"/> to use while drawing the text. </param>
        public Button(ref SpriteBatch sb, Rect bounds, SpriteFont font)
             : base(ref sb, bounds, font)
        { }

        /// <summary>
        /// Updates the <see cref="Button"/>, checking if any mouse event are occuring.
        /// </summary>
        /// <param name="deltaTime"> The deltatime </param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Enabled)
            {
                if (!over) textures.state = ButtonStyle.Default;
                else if (over && !leftDown && !rightDown) textures.state = ButtonStyle.Hover;

                if (leftDown && !leftInvoked)
                {
                    Invoke(LeftClick, this, GetMouseEventArgs());
                    leftInvoked = true;

                    doubleLeftClicked++;
                    textures.state = ButtonStyle.Click;
                }
                if (rightDown && !rightInvoked)
                {
                    Invoke(RightClick, this, GetMouseEventArgs());
                    rightInvoked = true;

                    doubleRightClicked++;
                    textures.state = ButtonStyle.Click;
                }

                time += deltaTime;
                if (doubleLeftClicked > 1 || doubleRightClicked > 1 || time > 1)
                {
                    doubleLeftClicked = 0;
                    doubleRightClicked = 0;
                    if (time < 1) Invoke(DoubleClick, this, GetMouseEventArgs());
                    time = 0;
                }

                if (leftInvoked && !leftDown) leftInvoked = false;
                if (rightInvoked && !rightDown) rightInvoked = false;
            }
        }

        /// <summary>
        /// Draws the <see cref="Button"/> to the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"> The <see cref="SpriteBatch"/> to use. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(textures.DrawTexture.Texture, Position, textures.DrawTexture[textures.DrawId], Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 1f);
                spriteBatch.Draw(textures.DrawTexture.Texture, ForegroundRectangle.Position, textures.DrawTexture[1], Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Perfoms a specified button click.
        /// </summary>
        /// <param name="button"> The type of click event to invoke. </param>
        public void PerformClick(MouseClick button = MouseClick.Default)
        {
            switch (button)
            {
                case MouseClick.Default:
                    base.PerformClick();
                    return;
                case MouseClick.Left:
                    Invoke(LeftClick, this, GetMouseEventArgs());
                    return;
                case MouseClick.Right:
                    Invoke(RightClick, this, GetMouseEventArgs());
                    return;
                case MouseClick.Double:
                    Invoke(DoubleClick, this, GetMouseEventArgs());
                    return;
            }
        }

        /// <summary>
        /// Sets <see cref="GuiItem.textures"/> to the required <see cref="TextureHandler"/>.
        /// </summary>
        protected override void SetTextureHandler()
        {
            textures = new ButtonTextureHandler();
        }
    }
}