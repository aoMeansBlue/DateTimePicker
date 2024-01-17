using System;
using UnityEngine.UIElements;

public class CalendarItem : VisualElement
{
    public DateDayValue date;
    static readonly string ussClass = "calendarItem";
    static readonly string ussClassSelected = "calendarItem--selected";
    static readonly string ussClassOutsideMonth = "calendarItem--outside-month";

    public CalendarItem(DateDayValue date)
    {
        focusable = true;
        this.date = date;
        var label = new Label(date.Day.ToString());
        Add(label);
        AddToClassList(ussClass);
    }

    public CalendarItem(int day, int month, int year) : this(new DateDayValue() { 
        Day = day, 
        Month = month, 
        Year = year }
    ){ }

    public void SetSelected(bool isSelected = false)
    {
        if(isSelected)
        {
            AddToClassList(ussClassSelected);
            
        } else
        {
            RemoveFromClassList(ussClassSelected);
        }
    }

    public void SetFocused(bool isFocused = false)
    {
        if (isFocused)
        {
            Focus();
        }
        else
        {
            Blur();
        }
    }

    public void SetOutsideMonth(bool isOutsideMonth = false)
    {
        if (isOutsideMonth)
        {
            AddToClassList(ussClassOutsideMonth);
        } else
        {
            RemoveFromClassList(ussClassOutsideMonth);
        }
    }
}
