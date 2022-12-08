using System.Windows.Input;
using WpfApp.Utils;

namespace WpfApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly DelegateCommand _changeStationCommand;
    public ICommand ChangeStationCommand => _changeStationCommand;

    private string _station = default!;
    public string Station
    {
        get => _station;
        set => SetField(ref _station, value);
    }

    public MainWindowViewModel(AppSettings settings)
    {
        Station = settings.Station;

        _changeStationCommand = new DelegateCommand(OnChangeStation, CanChangeStation);
    }

    private bool CanChangeStation(object? arg)
    {
        return Station is not "103";
    }

    private void OnChangeStation(object? parameters)
    {
        Station = "103";
        _changeStationCommand.InvokeCanExecuteChanged();
    }
}