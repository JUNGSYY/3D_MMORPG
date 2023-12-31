using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
    }
    
    public enum Layer
    {
        Monster = 6,
        Ground = 7,
        Block = 8,
    }
    public enum Scene
    {
        Unknow,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    
    public enum CameraMode
    {
        QuaterView
    }
    
    public enum MouseEvent
    {
        Press, 
        PointerDown,
        PointerUp,
        Click
    }

    public enum UIEvent
    {
        Click,
        Drag
    }
}