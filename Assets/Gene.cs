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

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        pastPosition = gameObject.transform.position;
        currentPosition = gameObject.transform.position;
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

        switch (_chromosome.direction)
        {
            case Direction.Left:
                _rigidbody.linearVelocity = Vector2.left * GeneManager.speed;
                break;
            case Direction.Right:
                _rigidbody.linearVelocity = Vector2.right * GeneManager.speed;
                break;
            case Direction.Down:
                _rigidbody.linearVelocity = Vector2.down * GeneManager.speed;
                break;
            case Direction.Up:
                _rigidbody.linearVelocity = Vector2.up * GeneManager.speed;
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
            Out();
        }
    }

    private void Out()
    {
        for(int index = 0; index < GeneManager.size; index++)
        {
            population[index].fitness -= GeneManager.speed * 0.1f;
        }
        _rigidbody.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
