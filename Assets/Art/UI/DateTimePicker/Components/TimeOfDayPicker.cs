using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeOfDayPicker : VisualElement
{
    Action<TimeOfDayValue> _onChange = null;
    TimeOfDayValue _timeOfDay;

    IntegerField TimeHours;
    IntegerField TimeMinutes;
    IntegerField TimeSeconds;
    IntegerField TimeMilliseconds;

    public TimeOfDayPicker() : this(null)
    {
        
    }

    public TimeOfDayPicker(Action<TimeOfDayValue> onChange)
    {
        _onChange = onChange;
        _timeOfDay = new TimeOfDayValue { Hours = 0, Minutes = 0, Seconds = 0, Milliseconds = 0 };

        TimeHours = new IntegerField(label: "HH") { value = 0 };
        TimeMinutes = new IntegerField(label:"MM") { value = 0 };
        TimeSeconds = new IntegerField(label: "SS") { value = 0 };
        TimeMilliseconds = new IntegerField(label: "Millisecs") { value = 0 };


        TimeHours.RegisterCallback<FocusOutEvent>((evt) => {
            TimeHours.SetValueWithoutNotify(ValidateHours(TimeHours.value));

            if (!_timeOfDay.Hours.Equals(TimeHours.value))
            {
                _timeOfDay.Hours = TimeHours.value;
                onValueUpdated(_timeOfDay);
            }
        });

        TimeMinutes.RegisterCallback<FocusOutEvent>((evt) => {
            TimeMinutes.SetValueWithoutNotify(ValidateMinSecs(TimeMinutes.value));

            if(!_timeOfDay.Minutes.Equals(TimeMinutes.value))
            {
                _timeOfDay.Minutes = TimeMinutes.value;
                onValueUpdated(_timeOfDay);
            }
        });

        TimeSeconds.RegisterCallback<FocusOutEvent>((evt) => {
            TimeSeconds.SetValueWithoutNotify(ValidateMinSecs(TimeSeconds.value));

            if (!_timeOfDay.Seconds.Equals(TimeSeconds.value))
            {
                _timeOfDay.Seconds = TimeSeconds.value;
                onValueUpdated(_timeOfDay);
            }
        });

        TimeMilliseconds.RegisterCallback<FocusOutEvent>((milliSec) => {
            TimeMilliseconds.SetValueWithoutNotify(ValidateMillisecs(TimeMilliseconds.value));

            if (!_timeOfDay.Milliseconds.Equals(TimeMilliseconds.value))
            {
                _timeOfDay.Milliseconds = TimeMilliseconds.value;
                onValueUpdated(_timeOfDay);
            }
        });

        Add(TimeHours);
        Add(TimeMinutes);
        Add(TimeSeconds);
        Add(TimeMilliseconds);
    }

    private void onValueUpdated(TimeOfDayValue newValue)
    {
        _onChange?.Invoke(newValue);
    }

    private int ValidateHours(int hour)
    {
        return (hour < 0) ? 0 : (hour > 23) ? 23 : hour;
    }

    private int ValidateMinSecs(int minSec)
    {
        return (minSec < 0) ? 0 : (minSec > 59) ? 59 : minSec;
    }

    private int ValidateMillisecs(int millisecs)
    {
        return (millisecs < 0) ? 0 : (millisecs > 999) ? 999 : millisecs;
    }
}

public struct TimeOfDayValue
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public int Milliseconds { get; set; }
}
