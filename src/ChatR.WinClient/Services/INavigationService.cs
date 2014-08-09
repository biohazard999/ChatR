using ChatR.WinClient.Contracts;

namespace ChatR.WinClient.Services
{
    public interface INavigationService
    {
        void NavigateTo(IPresenter presenter);

        void NavigateTo<T>() where T : IPresenter;

        void NavigateTo(string name);
    }
}