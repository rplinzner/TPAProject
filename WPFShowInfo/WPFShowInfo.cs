using System.ComponentModel.Composition;
using System.Windows.Forms;
using Interfaces;

namespace WPFShowInfo
{
    [Export(typeof(IShowInfo))]
    public class WPFShowInfo : IShowInfo
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }
    }
}