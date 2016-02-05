using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Mentula.GuiItems.Core;

namespace Mentula.GuiItems.Items
{
     /// <summary>
     /// A button base class.
     /// </summary>
     public class Button : Label
     {
          protected Texture2D hoverTexture;
          protected Texture2D clickTexture;

          private Texture2D drawTexture;
          private int doubleLeftClicked;
          private int doubleRightClicked;
          private float time;

          /// <summary>
          /// Occurs when the XnaMentula.GuiItems.Items.Button is left cliked.
          /// </summary>
          public event MouseEventHandler LeftClick;
          /// <summary>
          /// Occurs when the XnaMentula.GuiItems.Items.Button is right clicked.
          /// </summary>
          public event MouseEventHandler RightClick;
          /// <summary>
          /// Occurs when the XnaMentula.GuiItems.Items.Button is clicked twice ina short period of time.
          /// </summary>
          public event MouseEventHandler DoubleClick;

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.Button class with default settings.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Button to. </param>
          /// <param name="font"> The font to use while drawing the text. </param>
          public Button(GraphicsDevice device, SpriteFont font)
               : base(device, font)
          {
               drawTexture = backColorImage;
          }

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.Button class with a specified size.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Button to. </param>
          /// <param name="bounds"> The size of the button in pixels. </param>
          /// <param name="font"> The font to use while drawing the text. </param>
          public Button(GraphicsDevice device, Rectangle bounds, SpriteFont font)
               : base(device, bounds, font)
          {
               drawTexture = backColorImage;
          }

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.Button class as a child.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Button to. </param>
          /// <param name="parent"> The XnaMentula.GuiItems.Core.GuiItem to be the parent of the button. </param>
          /// <param name="font"> The font to use while drawing the text. </param>
          public Button(GraphicsDevice device, GuiItem parent, SpriteFont font)
               : base(device, parent, font)
          {
               drawTexture = backColorImage;
          }

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.Button class as a child with a specified size.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.Button to. </param>
          /// <param name="parent"> The XnaMentula.GuiItems.Core.GuiItem to be the parent of the button. </param>
          /// <param name="bounds"> The size of the button in pixels. </param>
          /// <param name="font"> The font to use while drawing the text. </param>
          public Button(GraphicsDevice device, GuiItem parent, Rectangle bounds, SpriteFont font)
               : base(device, parent, bounds, font)
          {
               drawTexture = backColorImage;
          }

          [Obsolete("Use Update(MouseState, float) instead", true)]
          new public void Update(MouseState mState) { }

          /// <summary>
          /// Updates the XnaMentula.GuiItems.Items.Button and its childs, checking if any mouse event are occuring.
          /// </summary>
          /// <param name="state"> The current mouse state. </param>
          public void Update(MouseState state, float deltaTime)
          {
               if (Enabled)
               {
                    bool over = new Rectangle(Position.X(), Position.Y(), bounds.Width, bounds.Height).Contains(state.X, state.Y);
                    bool lDown = over && state.LeftButton == ButtonState.Pressed;
                    bool rDown = over && state.RightButton == ButtonState.Pressed;

                    if (!over) drawTexture = backColorImage;
                    else if (over && !lDown && !rDown) drawTexture = hoverTexture;

                    if (!leftClicked && lDown)
                    {
                         if (LeftClick != null) LeftClick.DynamicInvoke(this, state);
                         doubleLeftClicked++;
                         leftClicked = true;
                         drawTexture = clickTexture;
                    }
                    if (!rigthClicked && rDown)
                    {
                         if (RightClick != null) RightClick.DynamicInvoke(this, state);
                         doubleRightClicked++;
                         rigthClicked = true;
                         drawTexture = clickTexture;
                    }

                    if (leftClicked && state.LeftButton == ButtonState.Released) leftClicked = false;
                    if (rigthClicked && state.RightButton == ButtonState.Released) rigthClicked = false;

                    time += deltaTime;

                    if (doubleLeftClicked > 1 || doubleRightClicked > 1 || time > 1)
                    {
                         doubleLeftClicked = 0;
                         doubleRightClicked = 0;
                         if (DoubleClick != null && time < 1) DoubleClick.DynamicInvoke(this, state);
                         time = 0;
                    }
               }

               base.Update(state);
          }

          /// <summary>
          /// Draws the XnaMentula.GuiItems.Items.Button and its childs to the specified spritebatch.
          /// </summary>
          public override void Draw(SpriteBatch spriteBatch)
          {
               if (visible)
               {
                    if (parent != null) spriteBatch.Draw(drawTexture, parent.Position + Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                    else spriteBatch.Draw(drawTexture, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);

                    if (backgroundImage != null)
                    {
                         if (parent != null) spriteBatch.Draw(backgroundImage, parent.Position + Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                         else spriteBatch.Draw(backgroundImage, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
                    }

                    for (int i = 0; i < Controls.Count; i++)
                    {
                         Controls[i].Draw(spriteBatch);
                    }

                    if (parent != null) spriteBatch.Draw(foregoundTexture, foregroundRectangle.Add(Parent.Position), null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
                    else spriteBatch.Draw(foregoundTexture, foregroundRectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
               }
          }

          /// <summary>
          /// Recalculates the background and the foreground.
          /// </summary>
          public override void Refresh()
          {
               if (AutoSize)
               {
                    Vector2 dim = font.MeasureString(text);
                    dim.X += 3;
                    bool width = dim.X != bounds.Width ? true : false;
                    bool height = dim.Y != bounds.Height ? true : false;

                    if (width && height) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), dim.Y());
                    else if (width) Bounds = new Rectangle(bounds.X, bounds.Y, dim.X(), bounds.Height);
                    else if (height) bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, dim.Y());
               }

               backColorImage = backColorImage.ApplyBorderButton(ButtonStyle.Default);
               hoverTexture = backColorImage.ApplyBorderButton(ButtonStyle.Hover);
               clickTexture = backColorImage.ApplyBorderButton(ButtonStyle.Click);

               if (backgroundImage != null) backgroundImage = backgroundImage.ApplyBorderButton(0);
               foregoundTexture = Drawing.FromText(text, font, foreColor, foregroundRectangle.Width, foregroundRectangle.Height, false, device);
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
                         if (LeftClick != null) LeftClick.DynamicInvoke(this, Mouse.GetState());
                         return;
                    case MouseClick.Right:
                         if (RightClick != null) RightClick.DynamicInvoke(this, Mouse.GetState());
                         return;
                    case MouseClick.Double:
                         if (DoubleClick != null) DoubleClick.DynamicInvoke(this, Mouse.GetState());
                         return;
               }
          }
     }
}
