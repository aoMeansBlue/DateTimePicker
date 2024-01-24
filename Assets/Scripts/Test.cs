using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DateTime testDate = new DateTime(year: 2024, month: 2, day: 29);
        Debug.Log(testDate.AddYears(-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
