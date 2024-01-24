using System;
using UnityEngine.UIElements;

public class CalendarItem : VisualElement
{
    public DateDayValue date;
    static readonly string ussClass = "calendarItem";
    static readonly string ussClassSelected = "calendarItem--selected";
    static readonly string ussClassOutsideMonth = "calendarItem--outside-month";

    public CalendarItem(DateDayValue date, CalendarItemType type)
    {
        focusable = true;
        this.date = date;
        
        string labelValue = type switch
        {
            CalendarItemType.YearItem => date.Year.ToString(),
            CalendarItemType.MonthItem => ((MonthNames)date.Month).ToString().Substring(0 ,3),
            _ => date.Day.ToString()
        };

        Label label = new Label(labelValue);

        Add(label);
        AddToClassList(ussClass);
    }

    public CalendarItem(int day, int month, int year, CalendarItemType type = CalendarItemType.DayItem) : this(new DateDayValue() { 
        Day = day, 
        Month = month, 
        Year = year }, 
        type
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

    public enum CalendarItemType
    {
        DayItem,
        MonthItem,
        YearItem
    }

    public enum MonthNames
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    
}

