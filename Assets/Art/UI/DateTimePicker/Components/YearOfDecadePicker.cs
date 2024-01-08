using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YearOfDecadePicker : VisualElement
{
    Action<int> _onChange = null;

    public YearOfDecadePicker() : this(null)
    {

    }

    public YearOfDecadePicker(Action<int> onChange)
    {
        _onChange = onChange;
    }

    private void onValueUpdated(int newValue)
    {
        _onChange?.Invoke(newValue);
    }
}
