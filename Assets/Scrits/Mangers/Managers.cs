using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Managers : MonoBehaviour
{
    static Managers Instance;  //유일성
    public static Managers GetInstance( )
    {
        Init();
        return Instance;
    }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject() { name = "@Managers"};
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            Instance = go.GetComponent<Managers>();
        }
    }
}