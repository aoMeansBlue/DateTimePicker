using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DayOfMonthPicker : VisualElement
{
    Action<DateDayValue> _onChange = null;
    DateDayValue _date;

    CalendarItem _currentlySelectedItem = null;

    static readonly string ussCalendarRowClass = "calendarRow";

    int numRows = 6;
    int numCols = 7;

    public DayOfMonthPicker() : this(null, DateTime.Now)
    {

    }

    public DayOfMonthPicker(Action<DateDayValue> onChange, DateTime? defaultDate = null)
    {
        DateTime defaultDateValue = (defaultDate == null) ? DateTime.UnixEpoch : (DateTime) defaultDate;

        _onChange = onChange;
        _date = new DateDayValue()
        {
            Day = defaultDateValue.Day,
            Month = defaultDateValue.Month,
            Year = defaultDateValue.Year
        };

        buildCalendar();
    }

    private void buildCalendar()
    {
        DateTime beginningOfMonth = new DateTime(year: _date.Year, month: _date.Month, day: 1);

        int daysBeforeMonth = (int)beginningOfMonth.DayOfWeek;
        int daysBeforeNextMonth = daysBeforeMonth + DateTime.DaysInMonth(_date.Year, _date.Month);

        DateTime beginningOfCalendar = beginningOfMonth.AddDays(-daysBeforeMonth);

        VisualElement monthOfYear = new VisualElement();

        monthOfYear.Add(new Label(beginningOfMonth.ToString("MMM-yyyy")));

        VisualElement calendarContainer = new VisualElement();


        for (int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for (int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;

                CalendarItem calendarItem = new CalendarItem(beginningOfCalendar.AddDays(index));

                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    if (!(_date.Day.Equals(calendarItem.date.Day) && _date.Month.Equals(calendarItem.date.Month) && _date.Year.Equals(calendarItem.date.Year)))
                    {
                        onDateSelected(calendarItem.date);

                        if (index < daysBeforeMonth || index >= daysBeforeNextMonth)
                        {
                            clearCalendar();
                            buildCalendar();
                        } else
                        {
                            _currentlySelectedItem?.SetSelected(false);
                            _currentlySelectedItem = calendarItem;
                            _currentlySelectedItem.SetSelected(true);
                        }
                    }

                });
                calendarItem.RegisterCallback<MouseOverEvent>((evt) =>
                {
                    calendarItem.SetHover(true);
                });
                calendarItem.RegisterCallback<MouseOutEvent>((evt) =>
                {
                    calendarItem.SetHover(false);
                });
                calendarRow.Add(calendarItem);

                if ((_date.Day.Equals(calendarItem.date.Day) && _date.Month.Equals(calendarItem.date.Month) && _date.Year.Equals(calendarItem.date.Year)))
                {
                    _currentlySelectedItem = calendarItem;
                    _currentlySelectedItem.SetSelected(true);
                }

                if (index < daysBeforeMonth || index >= daysBeforeNextMonth)
                {
                    calendarItem.SetOutsideMonth(true);
                }
            }


            calendarRow.AddToClassList(ussCalendarRowClass);
            calendarContainer.Add(calendarRow);
        }

        Add(monthOfYear);
        Add(calendarContainer);
    }

    private void clearCalendar()
    {
        Clear();
    }

    private void onDateSelected(DateTime date)
    {
        _date.Year = date.Year;
        _date.Month = date.Month;
        _date.Day = date.Day;

        onValueUpdated(_date);
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
