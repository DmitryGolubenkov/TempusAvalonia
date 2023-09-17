namespace Tempus.AppLayer.Settings.Services;

public static class SettingsServiceExtensions
{
    #region Theme

    public static async Task SetTheme(this SettingsService settingsService, string theme)
    {
        await settingsService.SetSetting(SettingsService.THEME_KEY, theme);
    }

    public static async Task<string?> GetTheme(this SettingsService settingsService)
    {
        var theme = await settingsService.GetSetting(SettingsService.THEME_KEY);

        if (theme is null)
        {
            await settingsService.SetTheme("light");
            return "light";
        }
        else
            return theme;
    }

    #endregion
}