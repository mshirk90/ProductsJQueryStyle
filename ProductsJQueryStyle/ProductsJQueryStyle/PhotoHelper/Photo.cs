using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace PhotoHelper
{
    public class Photo
    {
        public static byte[] ImageToByteArray(string imageFileName)
        {
            byte[] result = null;
            try
            {
                Image img = Image.FromFile(imageFileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = ms.ToArray();
                }
                img.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                System.IO.File.Delete(imageFileName);
               
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;

        }

        public static void ByteArrayToFile(byte[] ba, string filename)
        {
            File.WriteAllBytes(filename, ba);
        }

        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }


    }
}