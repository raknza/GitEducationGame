using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TextWrapper : Text
{

    /// <summary>
    /// The UIText enum.
    /// </summary>
    public UIText uiText;

    /// <summary>
    /// Executes when the languate settings changed.
    /// </summary>
    public void OnChange()
    {
        if (LanguageManager.inst != null)
        {
            if (LanguageManager.inst != null)
            {
                this.text = LanguageManager.inst.GetText(this.uiText) ?? "None";
            }
        }

    }

    /// <summary>
    /// Sets the text with string format.
    /// </summary>
    /// <param name="text">The ui text.</param>
    public void SetGlobalText(UIText text)
    {
        this.OnChange();
    }

    /// <summary>
    /// Executes after Awake.
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();

        if (LanguageManager.inst != null)
        {
            LanguageManager.inst.OnChangeLanguageHandler += OnChange;
            this.PreProcessText();
        }
    }

    protected override void Start()
    {
        base.Start();
        GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        GetComponent<Text>().color = Color.black;
        this.OnChange();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (LanguageManager.inst != null)
        {
            LanguageManager.inst.OnChangeLanguageHandler -= OnChange;
        }
    }

    /// <summary>
    /// Converts text from UIText enum to string.
    /// </summary>
    private void PreProcessText()
    {
        if (this.text.StartsWith("UITEXT.") && (Enum.TryParse<UIText>(this.text.Substring(7), true, out UIText result)))
        {
            this.uiText = result;
        }

        this.OnChange();
    }
}
