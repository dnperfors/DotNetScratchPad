using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Xunit;

namespace DotNetTests.System.Drawing
{
    public class ImageMerging
    {
        [Fact]
        public void Should_merge_two_images_together_to_one()
        {
            using (Image bottomImage = Image.FromStream(GetType().Assembly.GetManifestResourceStream("DotNetTests.System.Drawing.glyphicons_folder_open.png")))
            using (Image topImage = Image.FromStream(GetType().Assembly.GetManifestResourceStream("DotNetTests.System.Drawing.glyphicons_ban.png")))
            using (Image expectedImage = Image.FromStream(GetType().Assembly.GetManifestResourceStream("DotNetTests.System.Drawing.glyphicons_combined.png")))
            using (Image composedImage = new Bitmap(bottomImage.Width, bottomImage.Height, bottomImage.PixelFormat))
            {
                using (Graphics graphic = Graphics.FromImage(composedImage))
                {
                    graphic.CompositingMode = global::System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    graphic.DrawImage(bottomImage, new Rectangle(0, 0, bottomImage.Width, bottomImage.Height), new Rectangle(0, 0, bottomImage.Width, bottomImage.Height), GraphicsUnit.Pixel);
                    graphic.DrawImage(topImage, new Rectangle(0, 0, topImage.Width, topImage.Height), new Rectangle(0, 0, topImage.Width, topImage.Height), GraphicsUnit.Pixel);
                }

                for (int x = 0; x < expectedImage.Width; x++)
                    for (int y = 0; y < expectedImage.Height; y++)
                        Assert.Equal(((Bitmap)expectedImage).GetPixel(x, y), ((Bitmap)composedImage).GetPixel(x, y));
            }
        }
    }
}
