using System;
using UnityEditor.PackageManager;
using UnityEngine;

public static class GeneManager
{
    public static System.Random random = new System.Random();
    private static int _passScale = 10;
    private static int _size = 101;
    private static float _time = 10;
    private static float _speed = 5;
    private static float _mutationRate = 1;
    private static GeneController _controller;

    public static float MutationRate
    {
        get { return _mutationRate; }
        set { _mutationRate = value; }
    }
    public static float Speed
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
    public static int PassScale
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
    public static int Size
    {
        get
        {
            return _size;
        }
    }
    public static float Time
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
    public static GeneController Controller
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
