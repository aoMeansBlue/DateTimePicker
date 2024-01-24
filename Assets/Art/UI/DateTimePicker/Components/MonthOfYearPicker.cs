using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonthOfYearPicker : VisualElement
{
    Action<int> _onChange = null;
    DateDayValue _selectedMonth;

    static readonly string ussCalendarRowClass = "calendarRow";

    int numRows = 3;
    int numCols = 4;

    public MonthOfYearPicker() : this(null, DateTime.Now)
    {
        
    }

    public MonthOfYearPicker(Action<int> onChange, DateTime? defaultDate = null)
    {
        DateTime defaultDateValue = (defaultDate == null) ? DateTime.UnixEpoch : (DateTime)defaultDate;

        _onChange = onChange;
        _selectedMonth = new DateDayValue()
        {
            Day = 1,
            Month = defaultDateValue.Month,
            Year = defaultDateValue.Year
        };

        for(int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for (int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;

                CalendarItem calendarItem = new CalendarItem(1, 1 + index, _selectedMonth.Year, CalendarItem.CalendarItemType.MonthItem);
                
                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    Debug.Log(calendarItem.date.Month);
                });

                calendarRow.Add(calendarItem);
            }

            calendarRow.AddToClassList(ussCalendarRowClass);
            Add(calendarRow);
        }
    }

    private void onValueUpdated(int newValue)
    {
        _onChange?.Invoke(newValue);
    }
}
