using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ASMaIoP.ViewModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для DataAnalyze.xaml
    /// </summary>
    public partial class DataAnalyze : Window
    {
        DataAnalyzeVM vm;
        int[] values;
        public DataAnalyze(EmployeeData data)
        {
            InitializeComponent();

            vm = new DataAnalyzeVM(data);
            DataContext = vm;
            
            LoadData();
        }

        void LoadData()
        {
            values = vm.LoadData();
            
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            
            general.Series.Add(new PieSeries { Title = "Небыло", DataLabels = true, LabelPoint = labelPoint, Fill = Brushes.Blue, StrokeThickness = 1, Values = new ChartValues<int> { values[0] } });
            general.Series.Add(new PieSeries { Title = "Отпуск", DataLabels = true, LabelPoint = labelPoint, Fill = Brushes.MediumSeaGreen, StrokeThickness = 1, Values = new ChartValues<int> { values[1] } });
            general.Series.Add(new PieSeries { Title = "Больничный", DataLabels = true, LabelPoint = labelPoint, Fill = Brushes.OrangeRed, StrokeThickness = 1, Values = new ChartValues<int> { values[2] } });
            general.Series.Add(new PieSeries { Title = "Выходной", DataLabels = true, LabelPoint = labelPoint, Fill = Brushes.MediumVioletRed, StrokeThickness = 1, Values = new ChartValues<int> { values[3] } });
            general.Series.Add(new PieSeries { Title = "Смена", DataLabels = true, LabelPoint = labelPoint, Fill = Brushes.Green, StrokeThickness = 1, Values = new ChartValues<int> { values[4] } });
        }
    }
}
