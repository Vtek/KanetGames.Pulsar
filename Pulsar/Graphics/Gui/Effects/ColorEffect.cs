﻿/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2013, Kanet Games (contact@kanetgames.com / www.kanetgames.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using Pulsar.Framework;
using Pulsar.Graphics.Gui.Effects.Interfaces;
using Pulsar.Graphics.Gui.Enums;

namespace Pulsar.Graphics.Gui.Effects
{
    /// <summary>
    /// Color interpolation effect
    /// </summary>
    public class ColorEffect : AbstractEffect
    {
        /// <summary>
        /// From color
        /// </summary>
        protected Color _from;

        /// <summary>
        /// To color
        /// </summary>
        protected Color _to;

        /// <summary>
        /// Amount to LERP
        /// </summary>
        protected float _amount;

        /// <summary>
        /// Create a new instance of a ColorEffect
        /// </summary>
        /// <param name="controle">Gui controle</param>
        /// <param name="speed">Speed of the Effect</param>
        /// <param name="from">From color of the Effect</param>
        /// <param name="to">To color of the Effect</param>
        /// <param name="loop">True if effect must loop</param>
        public ColorEffect(IColorCapable control, EffectSpeedEnum speed, Color from, Color to, bool loop = false)
            : base(control, speed, loop)
        {
            this._from = from;
            this._to = to;
            this._amount = 0;
        }

        /// <summary>
        /// Apply the effect on the control
        /// </summary>
        /// <param name="gameTime">Time elpase since last update</param>
        public override void Apply(GameTime gameTime)
        {
            if (this.Control != null)
            {
                IColorCapable colorControl = (IColorCapable)this.Control;

                this._amount =
                    (colorControl.ColorIn) ?
                    MathHelper.Min(this._amount + ((float)gameTime.ElapsedGameTime.TotalSeconds * (int)Speed), 1) :
                    MathHelper.Max(this._amount - ((float)gameTime.ElapsedGameTime.TotalSeconds * (int)(Speed)), 0);

                colorControl.BackgroundColor = Color.LerpRGB(this._from, this._to, this._amount);

                if (this.Loop && colorControl.BackgroundColor.B == this._to.B && colorControl.BackgroundColor.R == this._to.R && colorControl.BackgroundColor.G == this._to.G)//if we loop and control color RGB is equal than the color reach, set color to start value
                {
                    colorControl.BackgroundColor = this._from;
                }
            }
        }
    }
}
