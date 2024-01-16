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

        buildCalendar(_date.Year, _date.Month);
    }

    private void buildOptions()
    {
        
    }

    private void buildCalendar(int year, int month)
    {
        DateTime beginningOfMonth = new DateTime(year: year, month: month, day: 1);

        int daysBeforeMonth = (int)beginningOfMonth.DayOfWeek;
        int daysBeforeNextMonth = daysBeforeMonth + DateTime.DaysInMonth(year, month);

        DateTime beginningOfCalendar = beginningOfMonth.AddDays(-daysBeforeMonth);

        VisualElement calendarOptions = new VisualElement();

        CalendarOption prevYear = new CalendarOption("<<");
        prevYear.RegisterCallback<MouseOverEvent>((evt) => { prevYear.SetHover(true); });
        prevYear.RegisterCallback<MouseDownEvent>((evt) => { prevYear.SetFocused(true); });
        prevYear.RegisterCallback<MouseUpEvent>((evt) => { prevYear.SetFocused(false); });
        prevYear.RegisterCallback<MouseOutEvent>((evt) => { prevYear.SetHover(false); });
        prevYear.RegisterCallback<ClickEvent>((evt) => {
            clearCalendar();
            buildCalendar(Math.Max(year - 1, 0) , month);
        });

        CalendarOption prevMonth = new CalendarOption("<");
        prevMonth.RegisterCallback<MouseOverEvent>((evt) => { prevMonth.SetHover(true); });
        prevMonth.RegisterCallback<MouseDownEvent>((evt) => { prevMonth.SetFocused(true); });
        prevMonth.RegisterCallback<MouseUpEvent>((evt) => { prevMonth.SetFocused(false); });
        prevMonth.RegisterCallback<MouseOutEvent>((evt) => { prevMonth.SetHover(false); });
        prevMonth.RegisterCallback<ClickEvent>((evt) => {
            clearCalendar();
            buildCalendar(year, ((11 + month) % 12) + 1);
        });

        CalendarOption monthOfYearSelection = new CalendarOption(beginningOfMonth.ToString("MMM-yyyy"));
        monthOfYearSelection.RegisterCallback<MouseOverEvent>((evt) => { monthOfYearSelection.SetHover(true); });
        monthOfYearSelection.RegisterCallback<MouseDownEvent>((evt) => { monthOfYearSelection.SetFocused(true); });
        monthOfYearSelection.RegisterCallback<MouseUpEvent>((evt) => { monthOfYearSelection.SetFocused(false); });
        monthOfYearSelection.RegisterCallback<MouseOutEvent>((evt) => { monthOfYearSelection.SetHover(false); });
        monthOfYearSelection.RegisterCallback<ClickEvent>((evt) => { });

        CalendarOption nextMonth = new CalendarOption(">");
        nextMonth.RegisterCallback<MouseOverEvent>((evt) => { nextMonth.SetHover(true); });
        nextMonth.RegisterCallback<MouseDownEvent>((evt) => { nextMonth.SetFocused(true); });
        nextMonth.RegisterCallback<MouseUpEvent>((evt) => { nextMonth.SetFocused(false); });
        nextMonth.RegisterCallback<MouseOutEvent>((evt) => { nextMonth.SetHover(false); });
        nextMonth.RegisterCallback<ClickEvent>((evt) => {
            clearCalendar();
            buildCalendar(year, ((1 + month) % 12) + 1);
        });

        CalendarOption nextYear = new CalendarOption(">>");
        nextYear.RegisterCallback<MouseOverEvent>((evt) => { nextYear.SetHover(true); });
        nextYear.RegisterCallback<MouseDownEvent>((evt) => { nextYear.SetFocused(true); });
        nextYear.RegisterCallback<MouseUpEvent>((evt) => { nextYear.SetFocused(false); });
        nextYear.RegisterCallback<MouseOutEvent>((evt) => { nextYear.SetHover(false); });
        nextYear.RegisterCallback<ClickEvent>((evt) => {
            clearCalendar();
            buildCalendar(Math.Max(year + 1, 0), month);
        });

        calendarOptions.Add(prevYear);
        calendarOptions.Add(prevMonth);
        calendarOptions.Add(monthOfYearSelection);
        calendarOptions.Add(nextMonth);
        calendarOptions.Add(nextYear);

        calendarOptions.AddToClassList(ussCalendarRowClass);

        VisualElement calendarContainer = new VisualElement();
        VisualElement dayOfWeekRow = new VisualElement();

        for(int k = 0; k < numCols; k++)
        {
            CalendarHeaderItem calendarHeader = new CalendarHeaderItem(beginningOfCalendar.AddDays(k).DayOfWeek.ToString().Substring(0,2));
            dayOfWeekRow.Add(calendarHeader);
        }

        dayOfWeekRow.AddToClassList(ussCalendarRowClass);
        calendarContainer.Add(dayOfWeekRow);

        for (int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for (int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;

                DateTime calendarDate = beginningOfCalendar.AddDays(index);
                CalendarItem calendarItem = new CalendarItem(calendarDate.Day, calendarDate.Month, calendarDate.Year);

                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    if (!_date.Equals(calendarItem.date))
                    {
                        onDateSelected(calendarItem.date);

                        if (index < daysBeforeMonth || index >= daysBeforeNextMonth)
                        {
                            clearCalendar();
                            buildCalendar(calendarItem.date.Year, calendarItem.date.Month);
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

        Add(calendarOptions);
        Add(calendarContainer);
    }

    private void clearCalendar()
    {
        Clear();
    }

    private void onDateSelected(DateDayValue date)
    {
        _date = date;

        onValueUpdated(_date);
    }

    private void onValueUpdated(DateDayValue newValue)
    {
        _onChange?.Invoke(newValue);
    }

    private (int, int) CalculateYearAndMonth((int, int) initYearAndMonth, int monthsToAdd )
    {
        int year = initYearAndMonth.Item1;
        int month = initYearAndMonth.Item2;

        int yearsToAdd = monthsToAdd / 12;
        int moduloMonthsToAdd = (monthsToAdd - (yearsToAdd * 12)) % 12;

        return (2012, 1);
    }
    
}

public struct DateDayValue
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
