using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GeneController : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;

    public GameObject genePrefab;

    private int populationSize;
    private Gene[] genes;

    [SerializeField] private TextMeshProUGUI geneInfo;

    private void Start()
    {
        populationSize = (GeneManager.passScale * (GeneManager.passScale - 1))/2;
        genes = new Gene[populationSize];

        for(int index = 0; index < populationSize; index++)
        {
            genes[index] = Instantiate(genePrefab).GetComponent<Gene>();

            genes[index].transform.position = startPoint.transform.position;
            genes[index].endPosition = endPoint.transform.position;

            genes[index].population = new Population();
            genes[index].population.Randomize();
        }

        ProcessPopulation();
    }

    private float timeRate;
    private int chromosomeRate;

    private void ProcessPopulation()
    {
        SelectFittest(genes);
        Crossover(genes);

        // to debug
        PrintGeneInfo(genes, geneInfo);

        timeRate = 0;
        chromosomeRate = 0;
        StartCoroutine(ProcessChromosome());
    }
    
    IEnumerator ProcessChromosome()
    {
        yield return new WaitForSeconds(0.1f);

        foreach(Gene gene in genes)
        {
            if(gene != null) gene.chromosome = gene.population[chromosomeRate];
        }

        timeRate += 0.1f;
        chromosomeRate++;
        
        if (timeRate < GeneManager.time) StartCoroutine(ProcessChromosome());
        else ProcessPopulation();
    }

    private void SelectFittest(Gene[] target)
    {
        foreach (Gene population in target)
        {
            population.population.UpdateFitness();
        }

        Sort(target);
    }
    private void Crossover(Gene[] target)
    {
        Gene[] passGene = new Gene[GeneManager.passScale];

        for (int index = 0; index < passGene.Length; index++)
        {
            passGene[index] = genes[index];
        }

        for(int index = 0; index < genes.Length; index++)
        {
            Destroy(genes[index].gameObject);

            genes[index] = Instantiate(genePrefab).GetComponent<Gene>();

            genes[index].transform.position = startPoint.transform.position;
            genes[index].endPosition = endPoint.transform.position;

            genes[index].population = new Population();
        }

        int count = 0;
        for (int first = 0; first < passGene.Length; first++)
        {
            for (int second = first + 1; second < passGene.Length; second++)
            {
                target[count].population = Population.Crossover(passGene[first].population, passGene[second].population);
                count++;
            }
        }
    }
    private void Sort(Gene[] target)
    {
        foreach(Gene item in target)
        {
            for (int index = 0; index < target.Length - 1; index++)
            {
                if (target[index].population.fitness < target[index + 1].population.fitness)
                {
                    Gene temp = target[index];
                    target[index] = target[index + 1];
                    target[index + 1] = temp;
                }
            }
        }
        foreach(Gene item in target)
        {
            Debug.Log(item.population.fitness);
        }
    }

    // To Debug
    private void PrintGeneInfo(Gene[] target, TextMeshProUGUI infoTarget)
    {
        infoTarget.text = "";
        foreach(Gene item in target)
        {
            for (int index = 0; index < GeneManager.size; index++)
            {
                switch (item.population[index].direction)
                {
                    case Direction.Up:
                        infoTarget.text += "N";
                        break;
                    case Direction.Down:
                        infoTarget.text += "S";
                        break;
                    case Direction.Right:
                        infoTarget.text += "E";
                        break;
                    case Direction.Left:
                        infoTarget.text += "W";
                        break;
                }
            }
            infoTarget.text += "\n";
        }
    }
}
