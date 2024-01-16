using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CalendarHeaderItem : VisualElement
{
    static readonly string ussClass = "calendarHeaderItem";

    public CalendarHeaderItem(string dayOfWeek)
    {
        var label = new Label(dayOfWeek);
        Add(label);
        AddToClassList(ussClass);
    }
}
