using System;
using UnityEngine.UIElements;

public class CalendarItem : VisualElement
{
    bool isSelected;
    public DateTime _date;
    static readonly string ussClass = "calendarItem";

    public CalendarItem(DateTime date)
    {
        _date = date;
        var label = new Label(date.Day.ToString());
        Add(label);
        AddToClassList(ussClass);
    }
}
