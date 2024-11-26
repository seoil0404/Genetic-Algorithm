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
        Chromosome dominent;
        Chromosome recessive;

        Chromosome newChromosome = new Chromosome();

        if (chromosome1.fitness > chromosome2.fitness)
        {
            dominent = chromosome1;
            recessive= chromosome2;
        }
        else
        {
            dominent = chromosome2;
            recessive = chromosome1;
        }

        float dominantRate;
        dominantRate = (dominent.fitness - recessive.fitness) / GeneManager.speed * GeneManager.time * 7.5f + 50f;

        dominent.fitness = 0;
        recessive.fitness = 0;


        if(GeneManager.random.Next(0, 100) < 1)
        {
            newChromosome.Randomize();
            return newChromosome;
        }
        else if (GeneManager.random.Next(0, 100) < dominantRate)
        {
            newChromosome.direction = dominent.direction;
        }
        else
        {
            newChromosome.direction = recessive.direction;
        }
        return newChromosome;
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

        int randomIndex = GeneManager.random.Next(typeArrays.Length);

        type = typeArrays[randomIndex];
    }
}