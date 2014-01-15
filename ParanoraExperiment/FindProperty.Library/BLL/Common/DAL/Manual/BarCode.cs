using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Controls;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class BarCode:IDAL.IBarCode
    {
        public byte[] CreateCode(string content)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = new QrCode();

            qrEncoder.TryEncode(content, out qrCode);

            Renderer renderer = new Renderer(2, Brushes.Black, Brushes.White);

            renderer.QuietZoneModules = QuietZoneModules.Two;

            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, ms, ImageFormat.Png);
                imageData = ms.ToArray();
            }

            return imageData;
        }

        public byte[] CreateCode(int size, Brush darkBrush, Brush lightBrush, string content)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = new QrCode();

            qrEncoder.TryEncode(content, out qrCode);

            Renderer renderer = new Renderer(size, darkBrush, lightBrush);

            renderer.QuietZoneModules = QuietZoneModules.Two;

            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, ms, ImageFormat.Png);
                imageData = ms.ToArray();
            }
            return imageData;
        }
    }
}
