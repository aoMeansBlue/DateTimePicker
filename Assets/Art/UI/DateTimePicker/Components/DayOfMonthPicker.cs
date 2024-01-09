using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DayOfMonthPicker : VisualElement
{
    Action<DateDayValue> _onChange = null;
    DateDayValue _date;

    static readonly string ussCalendarRowClass = "calendarRow";

    int numRows = 6;
    int numCols = 7;

    public DayOfMonthPicker() : this(null)
    {

    }

    public DayOfMonthPicker(Action<DateDayValue> onChange)
    {
        _onChange = onChange;
        _date = new DateDayValue()
        {
            Day = 9,
            Month = 1,
            Year = 2024
        };

        DateTime beginningOfMonth = new DateTime(year: _date.Year, month: _date.Month, day: 1);

        DateTime beginningOfCalendar = beginningOfMonth.AddDays(-(int)beginningOfMonth.DayOfWeek);

        VisualElement monthOfYear = new VisualElement();

        monthOfYear.Add(new Label(beginningOfMonth.ToString("MMM-yyyy")));

        VisualElement calendarContainer = new VisualElement();


        for (int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for(int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;
                VisualElement calendarItem = new CalendarItem(beginningOfCalendar.AddDays(index));
                calendarRow.Add(calendarItem);
            }

            calendarRow.AddToClassList(ussCalendarRowClass);
            calendarContainer.Add(calendarRow);
        }

        Add(monthOfYear);
        Add(calendarContainer);

    }

    private void onDateSelected(DateTime date)
    {

    }

    private void onValueUpdated(DateDayValue newValue)
    {
        _onChange?.Invoke(newValue);
    }

    
}

public struct DateDayValue
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
