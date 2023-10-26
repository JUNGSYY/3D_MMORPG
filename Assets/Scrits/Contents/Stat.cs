using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int _level;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;
    
    [SerializeField] protected int _attack;
    [SerializeField] protected int _defense;
    
    [SerializeField] protected int _MoveSpeed;
    
    public int level { get { return _level;} set { _level = value; } }
    public int Hp { get { return _hp;} set { _hp = value; } }
    public int MAXHP { get { return _maxHp;} set { _maxHp = value; } }
    public int ATTack { get { return _attack;} set { _attack = value; } }
    public int Defense { get { return _defense;} set { _defense = value; } }
    public int MoveSpeed { get { return _MoveSpeed;} set { _MoveSpeed = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _MoveSpeed = 5;
    }
}
