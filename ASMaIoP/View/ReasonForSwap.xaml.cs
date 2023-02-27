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

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для ReasonForSwap.xaml
    /// </summary>
    public partial class ReasonForSwap : Window
    {
        string reason;
        public ReasonForSwap(ref string reason)
        {
            InitializeComponent();
            this.reason = reason;   
        }

        private void CardRead_Click(object sender, RoutedEventArgs e)
        {
            reason = ReasonTxb.Text;
        }
    }
}
