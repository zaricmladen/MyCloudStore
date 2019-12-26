using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyCloudStoreClient
{
    class FileManipulator
    {
        public static byte[] _ReadFile(string path, ref string ext, ref string fileName)
        {
            byte[] buff = null;

            using (FileStream fs = new FileStream(path,
                                           FileMode.Open,
                                           FileAccess.Read))
            {
                BinaryReader br = new BinaryReader(fs);

                FileInfo fi = new FileInfo(path);
                long numBytes = fi.Length;
                buff = br.ReadBytes((int)numBytes);

                ext = fi.Extension;
                fileName = fi.Name;

                br.Close();
                fs.Close();
            }
            return buff;
        }

        public static bool _WriteFile(string path, byte[] byteArray)
        {
            System.IO.FileStream _FileStream = null;

            try
            {
                _FileStream =
                   new System.IO.FileStream(path, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);

                _FileStream.Write(byteArray, 0, byteArray.Length);
                return true;
            }
            catch (IOException ioEx)
            {
                System.Windows.Forms.MessageBox.Show("Error in writing file:\n" + ioEx.Message + "\nInner exception: " + ioEx.InnerException);
                return false;
            }
            finally
            {
                if (_FileStream != null)
                {
                    _FileStream.Close();
                }
            }
        }

        public static byte[][] BufferSplit(byte[] buffer, int blockSize)
        {
            byte[][] blocks = new byte[(buffer.Length + blockSize - 1) / blockSize][];

            for (int i = 0, j = 0; i < blocks.Length; i++, j += blockSize)
            {
                blocks[i] = new byte[Math.Min(blockSize, buffer.Length - j)];
                Array.Copy(buffer, j, blocks[i], 0, blocks[i].Length);
            }

            return blocks;
        }
    }
}
