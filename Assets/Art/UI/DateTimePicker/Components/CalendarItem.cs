using System;
using UnityEngine.UIElements;

public class CalendarItem : VisualElement
{
    public DateTime date;
    static readonly string ussClass = "calendarItem";
    static readonly string ussClassHover = "calendarItem--hover";
    static readonly string ussClassSelected = "calendarItem--selected";
    static readonly string ussClassOutsideMonth = "calendarItem--outside-month";

    public CalendarItem(DateTime date)
    {
        this.date = date;
        var label = new Label(date.Day.ToString());
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
