using Tempus.Core.Entities.Settings;
using Tempus.Core.Interfaces;

namespace Tempus.AppLayer.Settings.Services;
public class SettingsService
{
    #region Fields

    private IRepository<Setting> settings;

    #endregion

    #region Consts

    public const string THEME_KEY = "theme";

    #endregion

    #region Constructor

    public SettingsService(IRepository<Setting> settings)
    {
        this.settings = settings;
    }

    #endregion

    #region Properties

    #endregion

    public async Task SetSetting(string key, string value)
    {
        if (settings.Any(x => x.Key == key))
        {
            var setting = settings.FirstOrDefault(x => x.Key == key);
            setting.Value = value;
            settings.Update(setting);
        }
        else
        {
            settings.Add(new Setting { Key = key, Value = value });
        }

        await settings.SaveChangesAsync();
    }

    public async Task<string?> GetSetting(string key)
    {
        if (settings.Any(x => x.Key == key))
        {
            var setting = settings.FirstOrDefault(x => x.Key == key);
            return setting.Value;
        }
        else
            return null;

    }
}
