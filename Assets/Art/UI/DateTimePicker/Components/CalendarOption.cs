using System;
using UnityEngine.UIElements;

public class CalendarOption : VisualElement
{
    public string optionLabel;
    static readonly string ussClass = "calendarOption";
    static readonly string ussClassFocused = "calendarOption--focused";

    public CalendarOption(string optionLabel)
    {
        focusable = true;
        this.optionLabel = optionLabel;
        var label = new Label(optionLabel);
        Add(label);
        AddToClassList(ussClass);
    }

    public void SetFocused(bool isFocused = false)
    {
        if(isFocused)
        {
            Focus();
        } else
        {
            Blur();
        }
    }
}
