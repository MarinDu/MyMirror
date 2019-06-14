
namespace TramWidget.ViewModel
{
    using global::TramWidget.Model;
    using WingetContract.ViewModel;

    public class TramVM : ViewModelBase
    {
        public TramModel TramModel { get; set; }

        public TramVM()
        {
            TramModel = new TramModel();
        }

        public void Initialize()
        {
            TramModel.Initialize();
        }
    }
}
