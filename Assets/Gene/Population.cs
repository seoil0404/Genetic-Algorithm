using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using UnityEngine;

public class Population
{
    private List<Chromosome> chromosomeData;
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
}
