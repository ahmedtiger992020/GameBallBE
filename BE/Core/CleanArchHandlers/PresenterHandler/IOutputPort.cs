namespace Core
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void HandlePresenter(TUseCaseResponse response);
    }
}
