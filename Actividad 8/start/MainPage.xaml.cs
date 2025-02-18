namespace WeatherClient;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void btnRefresh_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPostalCode.Text))
            return; // Evita buscar si el campo está vacío.

        btnRefresh.IsEnabled = false;
        actIsBusy.IsRunning = true;

        try
        {
            BindingContext = await Services.WeatherServer.GetWeather(txtPostalCode.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener el clima: {ex.Message}", "OK");
        }

        btnRefresh.IsEnabled = true;
        actIsBusy.IsRunning = false;
    }
}
