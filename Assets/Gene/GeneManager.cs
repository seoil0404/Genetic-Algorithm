using System;
using UnityEditor.PackageManager;
using UnityEngine;

public static class GeneManager
{
    public static System.Random random = new System.Random();
    private static int _passScale = 20;
    private static int _size = 81;
    private static float _time = 8;
    private static float _speed = 7;
    private static GeneController _controller;

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
    public static GeneController controller
    {
        get
        {
            return _controller;
        }
        set
        {
            _controller = value;
        }
    }
}
