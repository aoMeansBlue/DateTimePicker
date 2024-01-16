using System;
using UnityEngine.UIElements;

public class CalendarOption : VisualElement
{
    public string optionLabel;
    static readonly string ussClass = "calendarOption";
    static readonly string ussClassHover = "calendarOption--hover";
    static readonly string ussClassFocused = "calendarOption--focused";

    public CalendarOption(string optionLabel)
    {
        this.optionLabel = optionLabel;
        var label = new Label(optionLabel);
        Add(label);
        AddToClassList(ussClass);
    }

    public void SetHover(bool isHover = false)
    {
        if (isHover)
        {
            AddToClassList(ussClassHover);
        } else
        {
            RemoveFromClassList(ussClassHover);
        }
        
    }

    public void SetFocused(bool isFocused = false)
    {
        if(isFocused)
        {
            AddToClassList(ussClassFocused);
        } else
        {
            RemoveFromClassList(ussClassFocused);
        }
    }
}
