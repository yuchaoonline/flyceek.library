
/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using ComLib;


namespace ComLib.CaptchaSupport
{
    /// <summary>
    /// Generates an Captcha image.
    /// </summary>
    public class CaptchaGenerator : ICaptchaGenerator
    {
        private Random _random = new Random();


        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaImageGenerator"/> class.
        /// </summary>
        public CaptchaGenerator()
        {
            Settings = new CaptchaGeneratorSettings();
            Settings.Width = 200;
            Settings.Height = 50;
            Settings.Font = "Arial";
        }


        #region ICaptchaImageGenerator Members
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public CaptchaGeneratorSettings Settings { get; set; }


        /// <summary>
        /// Generates the bitmap using a random text using the random text generator.
        /// </summary>
        /// <returns></returns>
        public Bitmap Generate(string randomText)
        {
            // Check.
            Guard.IsTrue(Settings.Width > 0, "Width of captcha must be greater than 0");
            Guard.IsTrue(Settings.Height > 0, "Height of captch must be greater than 0");
            
            // Get the settings to local variables.
            int minWidth = Settings.Width;
            int minHeight = Settings.Height;
            string fontName = Settings.Font;

            // Create instance of bitmap object
            Bitmap bitmap = new Bitmap(minWidth, minHeight, PixelFormat.Format32bppArgb);

            // Create instance of graphics object
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = new Rectangle(0, 0, minWidth, minHeight);

            // Fill the background in a light gray pattern
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.DiagonalCross, Color.LightGray, Color.White);
            graphics.FillRectangle(hatchBrush, rectangle);

            // At this point need to figure out fontsize.
            SizeF sizeF;
            float fontSize = rectangle.Height + 1;
            Font font;            
            do	
            {
                // Adjust font size since it needs to fit on screen.
                fontSize--;
                font = new Font(fontName, fontSize, FontStyle.Bold);
                sizeF = graphics.MeasureString(randomText, font);
            } while ( sizeF.Width > rectangle.Width );

            //Format the text
            StringFormat objStringFormat = new StringFormat();
            objStringFormat.Alignment = StringAlignment.Center;
            objStringFormat.LineAlignment = StringAlignment.Center;

            //Create a path using the text and randomly warp it
            GraphicsPath objGraphicsPath = new GraphicsPath();
            objGraphicsPath.AddString(randomText, font.FontFamily, (int)font.Style, font.Size, rectangle, objStringFormat);
            float flV = 4F;

            //Create a parallelogram for the text to draw into
            PointF[] arrPoints =
			{
				new PointF(_random.Next(rectangle.Width) / flV, _random.Next(rectangle.Height) / flV),
				new PointF(rectangle.Width - _random.Next(rectangle.Width) / flV, _random.Next(rectangle.Height) / flV),
				new PointF(_random.Next(rectangle.Width) / flV, rectangle.Height - _random.Next(rectangle.Height) / flV),
				new PointF(rectangle.Width - _random.Next(rectangle.Width) / flV, rectangle.Height - _random.Next(rectangle.Height) / flV)
			};

            //Create the warped parallelogram for the text
            Matrix objMatrix = new Matrix();
            objMatrix.Translate(0F, 0F);
            objGraphicsPath.Warp(arrPoints, rectangle, objMatrix, WarpMode.Perspective, 0F);

            //Add the text to the shape
            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.DarkGray, Color.Black);
            graphics.FillPath(hatchBrush, objGraphicsPath);

            //Add some random noise
            int intMax = Math.Max(rectangle.Width, rectangle.Height);
            int total = (int)(rectangle.Width * rectangle.Height / 30F);

            for (int i = 0; i < total; i++)
            {
                int x = _random.Next(rectangle.Width);
                int y = _random.Next(rectangle.Height);
                int w = _random.Next(intMax / 15);
                int h = _random.Next(intMax / 70);
                graphics.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Dispose the graphics objects.
            font.Dispose();
            hatchBrush.Dispose();
            graphics.Dispose();
            
            return bitmap;
        }

        #endregion
    }
}
