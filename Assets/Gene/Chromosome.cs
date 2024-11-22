using System;
using System.Linq;

public enum Direction
{ 
    Left, Right
}

public enum MoveState
{
    Move, Stop
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

        Random random = new Random();
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
    private MoveState _moveState;

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
    public MoveState moveState
    {
        get
        {
            return _moveState;
        }
        set
        {
            _moveState = value;
        }
    }

    public void Randomize()
    {
        Randomize(ref _direction);
        Randomize(ref _moveState);
    }

    private void Randomize<T>(ref T type) where T : Enum
    {
        T[] typeArrays = Enum.GetValues(typeof(T))
                                    .Cast<T>()
                                    .ToArray();

        int randomIndex = new Random().Next(typeArrays.Length);

        type = typeArrays[randomIndex];
    }

    
}