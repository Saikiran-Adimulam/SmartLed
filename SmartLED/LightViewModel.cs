using System;
using System.Windows.Input;
using SmartLED.TelitManager;
using Xamarin.Forms;

namespace SmartLED
{
    public class LightViewModel : BaseViewModel
    {
        private ITelitService telitService;

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        private TelitDevice _selectedLight;
        public TelitDevice SelectedLight
        {
            get => _selectedLight;
            set => SetProperty(ref _selectedLight, value);
        }

        public ICommand SendCommand { get; private set; }

        public LightViewModel()
        {
            telitService = DependencyService.Resolve<ITelitService>();
            SendCommand = new Command(OnSendTapped);
        }

        public void OnNavigatedTo(TelitDevice device)
        {
            SelectedLight = device;
        }

        private void OnSendTapped()
        {
            if(!string.IsNullOrWhiteSpace(InputText))
            {
                var bytesToSend = System.Text.Encoding.UTF8.GetBytes(InputText);
                telitService.SendData(bytesToSend, SelectedLight);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("", "Enter input", "Ok");
            }
        }
    }
}
