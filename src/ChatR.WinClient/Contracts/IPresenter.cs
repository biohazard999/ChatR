using System.Windows.Forms;

namespace ChatR.WinClient.Contracts
{
    public interface IPresenter
    {
        Control GetControl();
    }
}