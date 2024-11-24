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
    public static Chromosome CrossOver(in Chromosome chromosome1, in Chromosome chromosome2)
    {
        Chromosome dominent = new Chromosome();
        Chromosome recessive = new Chromosome();

        if (chromosome1.fitness > chromosome2.fitness)
        {
            dominent.direction = chromosome1.direction;
            recessive.direction = chromosome2.direction;
        }
        else
        {
            dominent.direction = chromosome2.direction;
            recessive.direction = chromosome1.direction;
        }

        float dominantRate;
        dominantRate = (dominent.fitness - recessive.fitness) / GeneManager.speed * 20 * 50f + 50f;

        System.Random random = new System.Random();
        if(random.Next(0, 100) < 1)
        {
            Chromosome mutation = new Chromosome();
            mutation.Randomize();
            return mutation;
        }
        else if (random.Next(0, 100) < dominantRate)
        {
            return dominent;
        }
        else
        {
            return recessive;
        }
    }

    private Direction _direction;

    private float _fitness = 0;

    public float fitness
    {
        get
        {
            return _fitness;
        }
        set
        {
            _fitness = value;
        }
    }

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