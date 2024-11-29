using UnityEngine;
using UnityEngine.InputSystem;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private GameObject wayPrefab;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private GameObject startButton;

    private bool isFinish = false;

    public void FinishDraw()
    {
        isFinish = true;
        GeneManager.controller.StartEvolve();
        Destroy(startButton);
    }

    private void Update()
    {
        if(!isFinish)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            startPoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            endPoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        }

        if (Input.GetMouseButton(1))
        {
            Instantiate(wayPrefab).transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        }
    }
}
