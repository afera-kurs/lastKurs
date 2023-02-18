using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ASMaIoP.Model
{
    public static class FTPClientFactory
    {
        static string address, login, password;

        public static void SetupConnectionData(string address, string login, string password)
        {
            FTPClientFactory.address = address;
            FTPClientFactory.login = login;
            FTPClientFactory.password = password;
        }

        public static FTP_Client CreateClient()
        {
            return new FTP_Client(address, login, password);
        }

    }

    public class FTP_Client
    {
        string address, login, password;

        public FTP_Client(string address, string login, string password)
        {
            this.address = address;
            this.login = login;
            this.password = password;
        }


        public void UploadFile(string path, string serverPath)
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(login, password);
            var url = $"ftp://{address}/{serverPath}";
            client.UploadFile(url, path);
        }

        public void DownloadFile(string path, string serverPath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{address}/{serverPath}");
            // устанавливаем метод на загрузку файлов
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // если требуется логин и пароль, устанавливаем их
            request.Credentials = new NetworkCredential(login, password);
            //request.EnableSsl = true; // если используется ssl

            // получаем ответ от сервера в виде объекта FtpWebResponse
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // получаем поток ответа
            Stream responseStream = response.GetResponseStream();
            // сохраняем файл в дисковой системе
            // создаем поток для сохранения файла
            FileStream fs = new FileStream(path, FileMode.Create);

            //Буфер для считываемых данных
            List<byte> buffer = new List<byte>();
            byte[] oldbuffer = new byte[256];
            int size = 0;

            while ((size = responseStream.Read(oldbuffer, 0, oldbuffer.Length)) > 0)
            {
                fs.Write(oldbuffer, 0, size);

            }
            fs.Close();
            response.Close();
        }

        public byte[] DownloadFileAsBytes(string serverPath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{address}/{serverPath}");
            // устанавливаем метод на загрузку файлов
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // если требуется логин и пароль, устанавливаем их
            request.Credentials = new NetworkCredential(login, password);
            //request.EnableSsl = true; // если используется ssl

            // получаем ответ от сервера в виде объекта FtpWebResponse
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // получаем поток ответа
            Stream responseStream = response.GetResponseStream();
            // сохраняем файл в дисковой системе
            // создаем поток для сохранения файла
            MemoryStream fs = new MemoryStream();

            //Буфер для считываемых данных
            byte[] oldbuffer = new byte[256];
            int size = 0;

            while ((size = responseStream.Read(oldbuffer, 0, oldbuffer.Length)) > 0)
            {
                fs.Write(oldbuffer, 0, size);
            }
            response.Close();

            return fs.ToArray();
        }

    }

    public class ImageSystem
    {
        FTP_Client client;

        private Bitmap LoadImageFromResources()
        {
            System.Drawing.Bitmap image = Properties.Resources._default;
            return image;
        }

        public ImageSystem()
        {
            client = FTPClientFactory.CreateClient();
        }

        public Bitmap DownloadImage(string serverPath)
        {
            try
            {
                byte[] data = client.DownloadFileAsBytes(serverPath);

                Bitmap bmp;
                using (var ms = new MemoryStream(data))
                {
                    bmp = new Bitmap(ms);
                }

                return bmp;
            }
            catch(Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));

                return LoadImageFromResources();
            }
        }

        public void UploadFile(string localPath, string serverPath)
        {
            try
            {
                client.UploadFile(localPath, serverPath);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event($"Не получилось загрузить картинку{ex.Message}", EventPriority.Error));

            }
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
