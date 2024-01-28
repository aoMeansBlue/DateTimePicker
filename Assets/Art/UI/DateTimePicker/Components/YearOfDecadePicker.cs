using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YearOfDecadePicker : VisualElement
{
    Action<int> _onChange = null;
    DateDayValue _selectedMonth;

    static readonly string ussCalendarRowClass = "calendarRow";

    int numRows = 3;
    int numCols = 4;

    public YearOfDecadePicker() : this(null, DateTime.Now)
    {

    }

    public YearOfDecadePicker(Action<int> onChange, DateTime? defaultDate = null)
    {
        DateTime defaultDateValue = (defaultDate == null) ? DateTime.UnixEpoch : (DateTime)defaultDate;

        _onChange = onChange;
        _selectedMonth = new DateDayValue()
        {
            Day = 1,
            Month = defaultDateValue.Month,
            Year = defaultDateValue.Year
        };

        Debug.Log(_selectedMonth.Year);

        int startingYear = _selectedMonth.Year - (_selectedMonth.Year % 10) - 1;

        for (int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for (int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;

                CalendarItem calendarItem = new CalendarItem(1, 1, startingYear + index, CalendarItem.CalendarItemType.YearItem);

                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    Debug.Log(calendarItem.date.Year);
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
