using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Population
{
    private List<Chromosome> chromosomeData;
    private float _fitness = 0;

    public Population Clone()
    {
        Population clone = new Population();

        clone.chromosomeData.Clear();
        foreach(Chromosome chromosome in chromosomeData)
        {
            clone.chromosomeData.Add(chromosome.Clone());
        }
        
        clone._fitness = _fitness;

        return clone;
    }

    public float fitness
    {
        get { return _fitness; }
    }
    
    public void Randomize()
    {
        foreach(Chromosome index in chromosomeData)
        {
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

    public List<Chromosome> chromosomes
    {
        get
        {
            return chromosomeData;
        }
    }

}
