using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mentula.GuiItems.Core;

namespace Mentula.GuiItems.Items
{
     /// <summary>
     /// A progres bar used for displaying progres.
     /// </summary>
     public class ProgresBar : GuiItem
     {
          /// <summary>
          /// Gets or sets the direction of the bar.
          /// Refresh required after change!
          /// </summary>
          public virtual bool Inverted { get; set; }
          /// <summary>
          /// Gets or sets the type of border given to the progressBar.
          /// Refresh required after change!
          /// </summary>
          public virtual BorderStyle BorderStyle { get; set; }
          /// <summary>
          /// Gets the maximum value of the progresBar.
          /// </summary>
          public virtual int MaxiumValue { get { return data.Maximum; } }
          /// <summary>
          /// Gets the minimum value of the progresBar.
          /// </summary>
          public virtual int MinimumValue { get { return data.Minimum; } }
          /// <summary>
          /// Gets or sets the current value of the progresBar.
          /// </summary>
          public virtual int Value { get { return data.Value; } set { ValueChanged.DynamicInvoke(this, value); } }

          protected ProgresData data;

          /// <summary>
          /// Occurs when the value of the XnaGuiItem.Items.ProgresBar.Value propery is changed.
          /// </summary>
          public event ValueChangedEventHandler<int> ValueChanged;

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.ProgresBar class with default settings.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.ProgresBar to. </param>
          public ProgresBar(GraphicsDevice device)
               : base(device)
          {
               InitEvents();

               Bounds = new Rectangle(0, 0, 100, 25);
               data = new ProgresData(0);
               BorderStyle = BorderStyle.FixedSingle;
               foreColor = Color.LimeGreen;

               Refresh();
          }

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.ProgresBar class with a specified size.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.ProgresBar to. </param>
          /// <param name="bounds"> The size of the progresBar in pixels. </param>
          public ProgresBar(GraphicsDevice device, Rectangle bounds)
               : base(device, bounds)
          {
               InitEvents();

               data = new ProgresData(0);
               BorderStyle = BorderStyle.FixedSingle;
               ForeColor = Color.LimeGreen;

               Refresh();
          }

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.ProgresBar class as a child.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.ProgresBar to. </param>
          /// <param name="parent"> The XnaMentula.GuiItems.Core.GuiItem to be the parent of the progresbar. </param>
          public ProgresBar(GraphicsDevice device, GuiItem parent)
               : base(device, parent)
          {
               InitEvents();

               Bounds = new Rectangle(0, 0, 100, 25);
               data = new ProgresData(0);
               BorderStyle = BorderStyle.FixedSingle;
               ForeColor = Color.LimeGreen;

               Refresh();
          }

          /// <summary>
          /// Initializes a new instance of the XnaMentula.GuiItems.Items.ProgresBar class as a child with a specified size.
          /// </summary>
          /// <param name="device"> The device to display the XnaMentula.GuiItems.Items.ProgresBar to. </param>
          /// <param name="parent"> The XnaMentula.GuiItems.Core.GuiItem to be the parent of the progresbar. </param>
          /// <param name="bounds"> The size of the progresBar in pixels. </param>
          public ProgresBar(GraphicsDevice device, GuiItem parent, Rectangle bounds)
               : base(device, parent, bounds)
          {
               InitEvents();

               data = new ProgresData(0);
               BorderStyle = BorderStyle.FixedSingle;
               ForeColor = Color.LimeGreen;

               Refresh();
          }

          /// <summary>
          /// Draws the XnaMentula.GuiItems.Items.ProgresBar and its childs to the specified spritebatch.
          /// </summary>
          public override void Draw(SpriteBatch spriteBatch)
          {
               base.Draw(spriteBatch);

               if (visible)
               {
                    if (parent != null) spriteBatch.Draw(foregoundTexture, parent.Position + Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                    else spriteBatch.Draw(foregoundTexture, Position, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
               }
          }

          /// <summary>
          /// Recalculates the background and the foreground.
          /// </summary>
          public void Refresh()
          {
               float ppp = (float)bounds.Width / 100;
               int width = (int)(ppp * data.Value);

               foregoundTexture = Drawing.FromColor(foreColor, bounds.Width, bounds.Height, Inverted ? new Rectangle(bounds.Width - width, 0, width, bounds.Height) : new Rectangle(0, 0, width, bounds.Height), device);
               backColorImage = Drawing.FromColor(backColor, bounds.Width, bounds.Height, device).ApplyBorderLabel(BorderStyle);
          }

          protected virtual void OnValueChanged(object sender, int newVal) { data.Value = newVal; Refresh(); }

          private void InitEvents()
          {
               ValueChanged += OnValueChanged;
          }
     }
}