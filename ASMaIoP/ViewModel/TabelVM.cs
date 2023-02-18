using ASMaIoP.Model;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using ASMaIoP.Model;
using System.Threading;

namespace ASMaIoP.ViewModel
{
    public class TabelVM : INotifyPropertyChanged
    {
        TabelLoader loadder;

        public TabelVM()
        {
            loadder = new TabelLoader();
        }

        public int year = 2022;
        public int month = 12;

        public delegate void UploadDataToCefSharpDel(string html);
        public string GetHtmlCode()
        {
            loadder.LoadDataFromBD(DateTime.Parse($"{year}-{month}"));
            WorkTimeEmployee data;

            Random rnd = new Random();

            WorkTimeTabelGenerator tabel = new WorkTimeTabelGenerator(year, month);

            loadder.LoadAllIntoBuilder(tabel);

            string html = tabel.GenCode();

            int Size = html.Length;
            return html;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
