using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DateTimePickerField : BaseField<DateTime>
{

    #region UxmlFactory and UxmlTraits

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
            ((DateTimePickerField)ve).defaultDateString = labelString.GetValueFromBag(bag, cc);
        }
    }

    #endregion



    private VisualElement rootVisualElement;

    //The visual input element containing the clickable value text element that displays the value and will trigger the dropdown
    public VisualElement inputElement;

    public TextElement valueText { get; private set; }

    private VisualElement calendarIcon;

    //An overlay element to attach to the root, this element should contain the dropdown menu as well as the clickable background to dismiss the dropdown
    private VisualElement overlay;

    //Clickable background element, dismiss the dropdown when clicked
    private VisualElement background;

    public static readonly string ussBaseClassName = "unity-base-field" + "__input";

    public static readonly string uiDocumentRootElementClassName = "unity-ui-document__root";

    public static readonly string builderRootElementClassName = "unity-builder-canvas__container";

    public string defaultDateString
    {
        get {
            return defaultDateTime.ToString();
        }
        
        set
        {   
            DateTime.TryParse(value, out defaultDateTime);
        }
    }

    public DateTime defaultDateTime;

    public DateTimePickerField(): this("DateTime", null)
    {
        /*
        DateTimePicker dateTimePicker = this.Q<DateTimePicker>();
        dateTimePicker.RegisterOnChangeAction((dateTime) => { 
            value = dateTime;
            Debug.Log(value);
        });
        */

    }

    public DateTimePickerField(string label, VisualElement root) : base(label, null)
    {
        valueText = new PopupTextElement();

        rootVisualElement = root ?? AttemptRetrieveRootVE();

        overlay = new VisualElement();  
        background = new VisualElement();
        DateTimePicker dateTimePicker = new DateTimePicker();
        valueText = new PopupTextElement();

        valueText.RegisterCallback<MouseDownEvent>(evt =>
        {
            AttemptRetrieveRootVE();
        });

        var visualInput = this.Q(ussBaseClassName);

        if(visualInput != null)
        {
            visualInput.hierarchy.Add(valueText);
            //visualInput.hierarchy.Add(dateTimePicker);
        }

        valueText.text = "hello";   

        hierarchy.Add(valueText);
        //hierarchy.Add(dateTimePicker);
    }

    private class PopupTextElement : TextElement
    {
        new Vector2 DoMeasure(float desiredWidth, MeasureMode widthMode, float desiredHeight, MeasureMode heightMode)
        {
            string text = this.text;
            if (string.IsNullOrEmpty(text))
            {
                text = " ";
            }

            return MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
        }
    }

    public VisualElement AttemptRetrieveRootVE()
    {
        VisualElement currentElement = this;

        while(currentElement.hierarchy.parent != null)
        {
            currentElement = currentElement.hierarchy.parent;

            string ussClasses = "";

            foreach(string ussClass in currentElement.GetClasses())
            {
                ussClasses += $"{ussClass}, ";
            }

            Debug.Log(ussClasses);
        }

        return currentElement;
    }
}
