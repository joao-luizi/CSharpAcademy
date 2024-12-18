using MathGame.CSharAcademy.Data;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace MathGame.CSharAcademy
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("CaveatBrush-Regular.ttf", "CaveatBrush-Regular");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");
            

            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
