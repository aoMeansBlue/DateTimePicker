using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CalendarItem : VisualElement
{
    bool isSelected;
    public DateTime _date;

    public CalendarItem(DateTime date)
    {
        _date = date;
        var label = new Label(date.Day.ToString());
        Add(label);
    }
}
