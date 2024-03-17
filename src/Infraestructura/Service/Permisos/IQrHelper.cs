//using QRCoder;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Infraestructura.Service.Permisos
{
   public  interface IQrHelper
    {
        byte[] CreateQR(string texto);

    }
    public class QrHelper: IQrHelper
    {

        public byte[] CreateQR(string texto) {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
             QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
             QRCode qrCode = new QRCode(qrCodeData);
             Bitmap qrCodeImage = qrCode.GetGraphic(20);


            return BitmapToBytes(qrCodeImage);
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
