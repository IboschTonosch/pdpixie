using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Internal;
using System.IO;

namespace pdpixie
{
    public class PdConvert
    {
        public void ConvertFiles(PdfDocument document, Dictionary<FileType, HashSet<string>> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                if (fileName.Key == FileType.IMAGE)
                {
                    foreach (string imageFileName in fileName.Value)
                    {
                        PdfPage page = page = document.AddPage();
                        page.Tag = imageFileName.Substring(0, imageFileName.IndexOf('.')) + ".pdf";
                        if (File.Exists(imageFileName))
                        {
                            using (XImage img = XImage.FromFile(imageFileName))
                            {
                                var height = (int)((600.0 / img.PixelWidth) * img.PixelHeight);

                                page.Width = 600.0;
                                page.Height = height;

                                var gfx = XGraphics.FromPdfPage(page);
                                gfx.DrawImage(img, 0, 0, 600, height);
                            }
                        }
                    }
                }
            }
        }
    }
}
