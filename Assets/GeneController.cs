using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GeneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI geneInfo;

    public GameObject startPoint;
    public GameObject endPoint;

    public GameObject genePrefab;

    private int populationSize;
    private Gene[] genes;

    private float timeRate;
    private int chromosomeRate;

    private void Awake()
    {
        GeneManager.Controller = this;
    }
    public void StartEvolve()
    {
        if (genes != null && genes.Length != 0)
        {
            foreach (var gene in genes) Destroy(gene.gameObject);
        }

        populationSize = (GeneManager.PassScale * (GeneManager.PassScale - 1))/2;
        genes = new Gene[populationSize];

        for(int index = 0; index < populationSize; index++)
        {
            genes[index] = Instantiate(genePrefab).GetComponent<Gene>();

            genes[index].transform.position = startPoint.transform.position;
            genes[index].endPosition = endPoint.transform.position;

            genes[index].population = new Population();
            genes[index].population.Randomize();
        }

        ProcessPopulation(genes);
    }
    private void ProcessPopulation(Gene[] target)
    {
        SelectFittest(target);
        Crossover(target);

        timeRate = 0;
        chromosomeRate = 0;
        StartCoroutine(ProcessChromosome(target));

        // to debug
        PrintGeneInfo(target, geneInfo);
    }
    IEnumerator ProcessChromosome(Gene[] target)
    {
        yield return new WaitForSeconds(0.1f);

        foreach(Gene gene in target)
        {
            if(gene != null) gene.chromosome = gene.population.chromosomes[chromosomeRate];
        }

        timeRate += 0.1f;
        chromosomeRate++;
        
        if (timeRate < GeneManager.Time) StartCoroutine(ProcessChromosome(target));
        else ProcessPopulation(target);
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
        Population[] passPopulation = new Population[GeneManager.PassScale];

        for (int index = 0; index < passPopulation.Length; index++)
        {
            passPopulation[index] = target[index].population.Clone();
        }

        for(int index = 0; index < target.Length; index++)
        {
            target[index].Initialize();
        }

        int count = 0;
        for (int first = 0; first < passPopulation.Length; first++)
        {
            for (int second = first + 1; second < passPopulation.Length; second++)
            {
                target[count].population = Population.Crossover(passPopulation[first], passPopulation[second]);
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
    }
    private void PrintGeneInfo(Gene[] target, TextMeshProUGUI infoTarget)
    {
        infoTarget.text = "";
        foreach(Gene item in target)
        {
            for (int index = 0; index < GeneManager.Size; index++)
            {
                if (item.population.chromosomes[index] == null) Debug.LogError("The Chromosome didn't allocated");
                switch (item.population.chromosomes[index].direction)
                {
                    case Direction.Up:
                        infoTarget.text += "Up, ";
                        break;
                    case Direction.Down:
                        infoTarget.text += "Down, ";
                        break;
                    case Direction.Right:
                        infoTarget.text += "Right, ";
                        break;
                    case Direction.Left:
                        infoTarget.text += "Left, ";
                        break;
                    case Direction.Stop:
                        infoTarget.text += "Stop, ";
                        break;
                        
                }
            }
            infoTarget.text += "\n\n";
        }
    }
}
