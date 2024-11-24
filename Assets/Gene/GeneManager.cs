using System;
using UnityEditor.PackageManager;
using UnityEngine;

public static class GeneManager
{
    private static int _passScale = 15;
    private static int _size = 51;
    private static float _time = 5;
    private static float _speed = 3;

    public static float speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }
    public static int passScale
    {
        get
        {
            return _passScale;
        }
        set
        {
            _passScale = value;
        }
    }
    public static int size
    {
        get
        {
            return _size;
        }
    }
    public static float time
    {
        get
        {
            return _time;
        }
        set
        {
            _time = (float)Math.Floor(value);
            _size = (int)_time * 10 + 1;
        }
    }
}
