using System;
using UnityEditor.PackageManager;
using UnityEngine;

public static class GeneManager
{
    private static int _passScale = 10;
    private static int _size = 100;
    private static float _time = 10;
    private static float _speed = 1;

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
            _size = (int)_time * 10;
            Debug.Log(_time);
            Debug.Log(_size);
        }
    }
}
