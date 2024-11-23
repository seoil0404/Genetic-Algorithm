using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public enum Direction
{ 
    Left, Right, Up, Down
}

// A piece of Population
public class Chromosome
{
    // Initialize when defined Start, End Points
    public static float maxFitness;

    public static Chromosome CrossOver(in Chromosome chromosome1, in Chromosome chromosome2)
    {
        Chromosome dominent;
        Chromosome recessive;

        if (chromosome1.fitness > chromosome2.fitness)
        {
            dominent = chromosome1;
            recessive = chromosome2;
        }
        else
        {
            dominent = chromosome2;
            recessive = chromosome1;
        }

        float dominantRate;
        dominantRate = (dominent.fitness - recessive.fitness) / maxFitness * 50f + 50f;

        System.Random random = new System.Random();
        if (random.Next(0, 100) < dominantRate)
        {
            return dominent;
        }
        else
        {
            return recessive;
        }
    }

    private Direction _direction;

    public float fitness = 0;
    
    public Direction direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }

    public void Randomize()
    {
        Randomize(ref _direction);
    }

    private void Randomize<T>(ref T type) where T : Enum
    {
        T[] typeArrays = Enum.GetValues(typeof(T))
                                    .Cast<T>()
                                    .ToArray();

        int randomIndex = new System.Random().Next(typeArrays.Length);

        type = typeArrays[randomIndex];
    }
}