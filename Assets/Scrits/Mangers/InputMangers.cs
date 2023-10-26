using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMangers : MonoBehaviour
{
    public Action<string> printAllEvent;

    private void Start()
    {
        printAllEvent += PrintA;
        printAllEvent += PrintB;
        
        printAllEvent?.Invoke("print out!");
    }

    void PrintA(string s)
    {
        Debug.Log("A is" + s);
    }

    void PrintB(string s)
    {
        Debug.Log("B is" + s);
    }
}
