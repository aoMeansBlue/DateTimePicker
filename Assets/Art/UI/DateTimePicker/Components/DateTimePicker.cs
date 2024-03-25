using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DateTimePicker : VisualElement
{ 
    Action<DateTime> _onChange = null;

    TimeOfDayValue _time = new TimeOfDayValue() { Hours = 0, Minutes = 0, Seconds = 0, Milliseconds = 0 };
    DateDayValue _date = new DateDayValue() { Day = 1, Month = 1, Year = 1990};


    TimeOfDayPicker _timeOfDayPicker;
    DayOfMonthPicker _dayOfMonthPicker;
    MonthOfYearPicker _monthOfYearPicker;
    YearOfDecadePicker _yearOfDecadePicker;
    VisualElement _valueDisplayContainer;

    public DateTimePicker() : this(DateTime.Now, null)
    {
        
    }

    public DateTimePicker(DateTime defaultDateTime, Action<DateTime> onChange = null)
    {
        _onChange = onChange;

        TimeOfDayPicker timeOfDayPicker = new TimeOfDayPicker((time) => { SetTime(time); });
        DayOfMonthPicker dayOfMonthPicker = new DayOfMonthPicker((day) => { SetDay(day); }, defaultDateTime);

        Add(timeOfDayPicker);
        Add(dayOfMonthPicker);
    }

    public void RegisterOnChangeAction(Action<DateTime> onChange)
    {
        _onChange = onChange;
    }

    private void ChangePicker(VisualElement oldPicker, VisualElement newPicker)
    {
        Remove(oldPicker);
        Add(newPicker);
    }

    private void SetTime(TimeOfDayValue timeOfDay)
    {
        _time = timeOfDay;
        DateTime dateTime = new DateTime(year: _date.Year, month: _date.Month, day: _date.Day, hour: _time.Hours, minute: _time.Minutes, second: _time.Seconds, millisecond: _time.Milliseconds);
        onValueUpdated(dateTime);
    }

    private void SetDay(DateDayValue day)
    {
        _date = day;
        DateTime dateTime = new DateTime(year: _date.Year, month: _date.Month, day: _date.Day, hour: _time.Hours, minute: _time.Minutes, second: _time.Seconds, millisecond: _time.Milliseconds);
        onValueUpdated(dateTime);
    }

    private void SetMonth(int month)
    {
        DateTime dateTime = new DateTime();
        onValueUpdated(dateTime);
    }

    private void SetYear(int year)
    {
        DateTime dateTime = new DateTime();
        onValueUpdated(dateTime);
    }

    
    private void onValueUpdated(DateTime newValue)
    {
        _onChange?.Invoke(newValue);
    }
}
