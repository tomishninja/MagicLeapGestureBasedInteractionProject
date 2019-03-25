using UnityEngine;

public class UpdatePosition : MonoBehaviour
{

    // must be filled
    public GameObject ThingToMove = null;

    private Quaternion newRotation = new Quaternion();

    private Vector3 newPosition = new Vector3();

    public static object obj = new object();

    void Start()
    {
        lock (obj)
        {
            Quaternion newRotation = ThingToMove.transform.rotation;

            Vector3 newPosition = ThingToMove.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lock (obj)
        {
            if (newRotation != ThingToMove.transform.rotation)
            {
                ThingToMove.transform.rotation = newRotation;
            }

            if (newPosition != ThingToMove.transform.position)
            {
                ThingToMove.transform.position = newPosition;
            }
        }
    }

    public void SetNewPos(Vector3 newpos)
    {
        lock (obj)
        {
            this.newPosition = newpos;
        }
    }

    public void SetNewRotation(Quaternion newRota)
    {
        lock (obj)
        {
            this.newRotation = newRota;
        }
    }
}
