using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace People;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Obtener la ruta local de la base de datos
        string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");

        // Registrar PersonRepository como un servicio singleton
        builder.Services.AddSingleton<PersonRepository>(s => ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));

        return builder.Build();
    }
}
