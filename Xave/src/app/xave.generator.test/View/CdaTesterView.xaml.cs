using System;
using System.Collections.Generic;
using System.IO;
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

namespace xave.generator.test.View
{
    /// <summary>
    /// CdaTesterView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CdaTesterView : Window
    {
        public CdaTesterView()
        {
            InitializeComponent();

            System.Drawing.Icon icon = xave.generator.test.Properties.Resources.Costumization;
            MemoryStream iconStream = new MemoryStream();
            icon.Save(iconStream);
            iconStream.Seek(0, SeekOrigin.Begin);
            BitmapFrame newIcon = BitmapFrame.Create(iconStream);
            this.Icon = newIcon;
        }
    }
}
