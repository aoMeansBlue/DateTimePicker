using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YearOfDecadePicker : VisualElement
{
    Action<int> _onChange = null;
    DateDayValue _selectedMonth;

    int numRows = 4;
    int numCols = 3;

    public YearOfDecadePicker() : this(null)
    {

    }

    public YearOfDecadePicker(Action<int> onChange)
    {
        _onChange = onChange;
        _selectedMonth = new DateDayValue()
        {
            Day = 1,
            Month = 1,
            Year = 1990
        };

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {

            }
        }
    }

    private void onValueUpdated(int newValue)
    {
        _onChange?.Invoke(newValue);
    }
}
