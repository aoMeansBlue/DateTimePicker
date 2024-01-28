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
    CalendarItem _currentlySelectedMonth = null;
    CalendarItem _currentlySelectedYear = null;

    static readonly string ussCalendarRowClass = "calendarRow";

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
        clearCalendar();

        int numRows = 6;
        int numCols = 7;

        DateTime beginningOfMonth = new DateTime(year: year, month: month, day: 1);

        int daysBeforeMonth = (int)beginningOfMonth.DayOfWeek;
        int daysBeforeNextMonth = daysBeforeMonth + DateTime.DaysInMonth(year, month);

        DateTime beginningOfCalendar = beginningOfMonth.AddDays(-daysBeforeMonth);

        VisualElement calendarOptions = new VisualElement();

        CalendarOption prevYear = new CalendarOption("<<");
        prevYear.RegisterCallback<MouseDownEvent>((evt) => { prevYear.SetFocused(true); });
        prevYear.RegisterCallback<MouseUpEvent>((evt) => { prevYear.SetFocused(false); });
        prevYear.RegisterCallback<ClickEvent>((evt) => {
            beginningOfMonth = beginningOfMonth.AddYears(-1);
            buildCalendar(beginningOfMonth.Year, beginningOfMonth.Month);
        });

        CalendarOption prevMonth = new CalendarOption("<");
        prevMonth.RegisterCallback<MouseDownEvent>((evt) => { prevMonth.SetFocused(true); });
        prevMonth.RegisterCallback<MouseUpEvent>((evt) => { prevMonth.SetFocused(false); });
        prevMonth.RegisterCallback<ClickEvent>((evt) => {
            beginningOfMonth = beginningOfMonth.AddMonths(-1);
            buildCalendar(beginningOfMonth.Year, beginningOfMonth.Month);
        });

        CalendarOption monthOfYearSelection = new CalendarOption(beginningOfMonth.ToString("MMM-yyyy"));
        monthOfYearSelection.RegisterCallback<MouseDownEvent>((evt) => { monthOfYearSelection.SetFocused(true); });
        monthOfYearSelection.RegisterCallback<MouseUpEvent>((evt) => { monthOfYearSelection.SetFocused(false); });
        monthOfYearSelection.RegisterCallback<ClickEvent>((evt) => {
            buildMonthSelector(beginningOfMonth.Year);
        });

        CalendarOption nextMonth = new CalendarOption(">");
        nextMonth.RegisterCallback<MouseDownEvent>((evt) => { nextMonth.SetFocused(true); });
        nextMonth.RegisterCallback<MouseUpEvent>((evt) => { nextMonth.SetFocused(false); });
        nextMonth.RegisterCallback<ClickEvent>((evt) => {
            beginningOfMonth = beginningOfMonth.AddMonths(1);
            buildCalendar(beginningOfMonth.Year, beginningOfMonth.Month);
        });

        CalendarOption nextYear = new CalendarOption(">>");
        nextYear.RegisterCallback<MouseDownEvent>((evt) => { nextYear.SetFocused(true); });
        nextYear.RegisterCallback<MouseUpEvent>((evt) => { nextYear.SetFocused(false); });
        nextYear.RegisterCallback<ClickEvent>((evt) => {
            beginningOfMonth = beginningOfMonth.AddYears(1);
            buildCalendar(beginningOfMonth.Year, beginningOfMonth.Month);
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
                CalendarItem calendarItem = new CalendarItem(calendarDate.Day, calendarDate.Month, calendarDate.Year, CalendarItem.CalendarItemType.DayItem);

                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    if (!_date.Equals(calendarItem.date))
                    {
                        onDateSelected(calendarItem.date);

                        if (index < daysBeforeMonth || index >= daysBeforeNextMonth)
                        {
                            buildCalendar(calendarItem.date.Year, calendarItem.date.Month);
                        } else
                        {
                            _currentlySelectedItem?.SetSelected(false);
                            _currentlySelectedItem = calendarItem;
                            _currentlySelectedItem.SetSelected(true);
                        }
                    }

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

    private void buildMonthSelector(int year)
    {
        clearCalendar();

        int numRows = 3;
        int numCols = 4;

        VisualElement calendarOptions = new VisualElement();

        CalendarOption prevYear = new CalendarOption("<");
        prevYear.RegisterCallback<MouseDownEvent>((evt) => { prevYear.SetFocused(true); });
        prevYear.RegisterCallback<MouseUpEvent>((evt) => { prevYear.SetFocused(false); });
        prevYear.RegisterCallback<ClickEvent>((evt) => {
            buildMonthSelector(year - 1);
        });

        CalendarOption yearOfDecadeSelection = new CalendarOption(year.ToString());
        yearOfDecadeSelection.RegisterCallback<MouseDownEvent>((evt) => { yearOfDecadeSelection.SetFocused(true); });
        yearOfDecadeSelection.RegisterCallback<MouseUpEvent>((evt) => { yearOfDecadeSelection.SetFocused(false); });
        yearOfDecadeSelection.RegisterCallback<ClickEvent>((evt) => {
            buildYearSelector(year);
        });

        CalendarOption nextYear = new CalendarOption(">");
        nextYear.RegisterCallback<MouseDownEvent>((evt) => { nextYear.SetFocused(true); });
        nextYear.RegisterCallback<MouseUpEvent>((evt) => { nextYear.SetFocused(false); });
        nextYear.RegisterCallback<ClickEvent>((evt) => {
            buildMonthSelector(year + 1);
        });

        calendarOptions.Add(prevYear);
        calendarOptions.Add(yearOfDecadeSelection);
        calendarOptions.Add(nextYear);

        calendarOptions.AddToClassList(ussCalendarRowClass);

        VisualElement calendarContainer = new VisualElement();

        for (int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for (int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;

                CalendarItem calendarItem = new CalendarItem(1, 1 + index, year, CalendarItem.CalendarItemType.MonthItem);

                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    buildCalendar(year, calendarItem.date.Month);
                });

                calendarRow.Add(calendarItem);
            }

            calendarRow.AddToClassList(ussCalendarRowClass);
            calendarContainer.Add(calendarRow);
        }

        Add(calendarOptions);
        Add(calendarContainer);
    }
    private void buildYearSelector(int year)
    {
        clearCalendar();

        int numRows = 3;
        int numCols = 4;
        int startOfDecade = year - (year % 10);
        int endofDecade = startOfDecade + 9;
        int startOfYearSelection = startOfDecade - 1;

        VisualElement calendarOptions = new VisualElement();

        CalendarOption prevDecade = new CalendarOption("<");
        prevDecade.RegisterCallback<MouseDownEvent>((evt) => { prevDecade.SetFocused(true); });
        prevDecade.RegisterCallback<MouseUpEvent>((evt) => { prevDecade.SetFocused(false); });
        prevDecade.RegisterCallback<ClickEvent>((evt) => {
            buildYearSelector(startOfDecade - 10);
        });

        CalendarOption yearRange = new CalendarOption($"{startOfDecade} - {endofDecade}");

        CalendarOption nextDecade = new CalendarOption(">");
        nextDecade.RegisterCallback<MouseDownEvent>((evt) => { nextDecade.SetFocused(true); });
        nextDecade.RegisterCallback<MouseUpEvent>((evt) => { nextDecade.SetFocused(false); });
        nextDecade.RegisterCallback<ClickEvent>((evt) => {
            buildYearSelector(startOfDecade + 10);
        });

        calendarOptions.Add(prevDecade);
        calendarOptions.Add(yearRange);
        calendarOptions.Add(nextDecade);

        calendarOptions.AddToClassList(ussCalendarRowClass);

        VisualElement calendarContainer = new VisualElement();

        for (int i = 0; i < numRows; i++)
        {
            VisualElement calendarRow = new VisualElement();

            for (int j = 0; j < numCols; j++)
            {
                int index = i * numCols + j;

                CalendarItem calendarItem = new CalendarItem(1, 1, startOfYearSelection + index, CalendarItem.CalendarItemType.YearItem);

                calendarItem.RegisterCallback<ClickEvent>((evt) =>
                {
                    buildMonthSelector(calendarItem.date.Year);
                });

                calendarRow.Add(calendarItem);
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
    
}

public struct DateDayValue
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
