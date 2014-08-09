using System.Windows.Forms;

namespace ChatR.WinClient.Services
{
    public interface INavigationAware
    {
        void DoNavigate(Control controlToNavigateTo);
    }
}