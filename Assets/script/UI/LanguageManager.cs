using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// The language definitions.
/// </summary>
public enum Language
{
    ENGLISH,
    CHINESE
}

/// <summary>
/// The LanguageManager class.
/// </summary>
public class LanguageManager : Singleton<LanguageManager>
{
    /// <summary>
    /// The language setting.
    /// </summary>
    public LanguageSetting LanguageSetting;

    /// <summary>
    /// The language settings change callback.
    /// </summary>
    public Action OnChangeLanguageHandler = () => { };

    /// <summary>
    /// The multi-language texts.
    /// </summary>
    private Dictionary<string, Dictionary<string, string>> multiLanguageTexts;

    /// <summary>
    /// Gets multi-language text.
    /// </summary>
    /// <param name="text">The ui text enum.</param>
    /// <returns>The text string.</returns>
    public string GetText(UIText text)
    {
        return this.multiLanguageTexts[text.ToString()][this.LanguageSetting.Value.ToString()];
    }

    public void switchLanguage(int value)
    {
        LanguageSetting.setLanguage( (Language) value );
    }

    /// <summary>
    /// Executes when gameObject instantiates..
    /// </summary>
    protected override void AwakeInternal()
    {
#if UNITY_EDITOR
        var content = File.ReadAllText($"{Application.dataPath}\\GameData\\GameText.json");
#else
            var content = File.ReadAllText($"{Application.streamingAssetsPath}\\Data\\GameText.json");
#endif
        this.multiLanguageTexts = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(content);
    }
}