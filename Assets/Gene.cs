using System;
using Unity.VisualScripting;
using UnityEngine;

public class Gene : MonoBehaviour
{
    public Population population;
    private Chromosome _chromosome;
    private Rigidbody2D _rigidbody;

    private Vector3 pastPosition;
    private Vector3 currentPosition;

    private Vector3 _endPosition;

    private int enterStack = 0;

    private bool isEnd = false;

    public void Initialize()
    {
        transform.position = GeneManager.Controller.startPoint.transform.position;
        
        pastPosition = gameObject.transform.position;
        currentPosition = gameObject.transform.position;

        _endPosition = GeneManager.Controller.endPoint.transform.position;
        
        _rigidbody.linearVelocity = Vector2.zero;

        gameObject.SetActive(true);
    }
    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Initialize();
    }
    public Chromosome chromosome
    {
        set
        {
            if(enterStack > 0)
            {
                
                UpdateFitness();
                _chromosome = value;
                UpdateBehavior();
            }
        }
    }
    public Vector3 endPosition
    { 
        set 
        { 
            _endPosition = value; 
        }
    }
    private void UpdateBehavior()
    {
        if (_chromosome == null) Debug.LogError("The Chromosome didn't allocated");

        switch (_chromosome.direction)
        {
            case Direction.Left:
                _rigidbody.linearVelocity = Vector2.left * GeneManager.Speed;
                break;
            case Direction.Right:
                _rigidbody.linearVelocity = Vector2.right * GeneManager.Speed;
                break;
            case Direction.Down:
                _rigidbody.linearVelocity = Vector2.down * GeneManager.Speed;
                break;
            case Direction.Up:
                _rigidbody.linearVelocity = Vector2.up * GeneManager.Speed;
                break;
            case Direction.Stop:
                _rigidbody.linearVelocity = Vector2.zero;
                break;
        }
    }
    private void UpdateFitness()
    {
        currentPosition = gameObject.transform.position;

        if (_chromosome != null)
        {
            _chromosome.fitness += Vector3.Distance(pastPosition, _endPosition) - Vector3.Distance(currentPosition, _endPosition);
        }
        
        pastPosition = currentPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterStack++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enterStack--;
        if (enterStack == 0)
        {
            if(!isEnd)
            {
                
                Out();
            }
            else enterStack++;
        }
    }
    private void Out()
    {
        for(int index = 0; index < GeneManager.Size; index++)
        {
            population.chromosomes[index].fitness -= GeneManager.Speed * 0.1f;
        }
        _rigidbody.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}