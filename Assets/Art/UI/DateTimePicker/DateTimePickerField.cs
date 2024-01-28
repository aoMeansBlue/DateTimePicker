using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DateTimePickerField : BaseField<DateTime>
{
    #region UIEditor

    public new class UxmlFactory : UxmlFactory<DateTimePickerField, UxmlTraits> { }

    public new class UxmlTraits : BaseField<DateTime>.UxmlTraits
    {
        UxmlStringAttributeDescription labelString = new UxmlStringAttributeDescription { name = "mabel" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            ((DateTimePickerField)ve).labelText = labelString.GetValueFromBag(bag, cc);
        }
    }

    #endregion

    public string labelText { get; set; }

    public DateTime defaultDateTime;

    public DateTimePickerField(): this("DateTime", new DateTimePicker())
    {
        DateTimePicker dateTimePicker = this.Q<DateTimePicker>();
        dateTimePicker.RegisterOnChangeAction((dateTime) => { 
            value = dateTime;
            Debug.Log(value);
            Debug.Log(labelText);
        });
    }

    public DateTimePickerField(string label, VisualElement visualInput) : base(label, visualInput)
    {

    }
}
