using UnityEngine;

public class CollidingZone : MonoBehaviour
{
    public Numbers refPoints;

    public int correct, incorrect;

    private Manager manager;

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();
    }

    public void Start()
    {
        correct = incorrect = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent = gameObject.transform;
        collision.transform.localPosition = Vector3.zero;

        manager.SetAllNumbersDragable(false);
       
        if (refPoints.representingNumber == collision.GetComponent<Numbers>().representingNumber)
        {
            manager.AddToAndShowCorrectTasks();
        }
        else
        {
            manager.AddToAndShowIncorrectTasks();
        }
    }
}
