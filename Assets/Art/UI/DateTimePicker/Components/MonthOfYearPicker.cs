using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonthOfYearPicker : VisualElement
{
    Action<int> _onChange = null;

    public MonthOfYearPicker() : this(null)
    {
        
    }

    public MonthOfYearPicker(Action<int> onChange)
    {
        _onChange = onChange;
    }

    private void onValueUpdated(int newValue)
    {
        _onChange?.Invoke(newValue);
    }
}
