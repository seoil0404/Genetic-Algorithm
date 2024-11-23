using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using UnityEngine;

public class Population
{
    private List<Chromosome> chromosomeData;
    private float _fitness = 0;

    public float fitness
    {
        get { return _fitness; }
    }
    
    public void Randomize()
    {
        foreach(Chromosome index in chromosomeData)
        {
            Debug.Log("Randomize");
            index.Randomize();
        }
    }

    public Population()
    {
        chromosomeData = new List<Chromosome>(GeneManager.size);
        for(int index = 0; index < GeneManager.size; index++)
        {
            chromosomeData.Add(new Chromosome());
        }
    }

    public static Population Crossover(in Population population1, in Population population2)
    {
        Population newPopulation = new Population();
        
        for(int index = 0; index < GeneManager.size; index++)
        {
            newPopulation.chromosomeData[index] = 
                Chromosome.CrossOver(
                    population1.chromosomeData[index]
                    , population2.chromosomeData[index]
                );
        }

        return newPopulation;
    }

    public void UpdateFitness()
    {
        foreach(Chromosome chromosome in chromosomeData)
        {
            _fitness += chromosome.fitness;
        }
    }

    public Chromosome this[int index]
    {
        get
        {
            Debug.Log(index);
            return chromosomeData[index];
        }
    }
}
