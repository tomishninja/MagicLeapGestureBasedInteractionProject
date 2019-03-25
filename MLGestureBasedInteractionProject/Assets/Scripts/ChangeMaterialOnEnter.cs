using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnEnter : MonoBehaviour {
    
    public Material oldMaterial = null;

    public Material newMaterial = null;

    private LinkedList<GameObject> objectsWithin = new LinkedList<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnEnterTrigger");

        this.objectsWithin.AddLast(other.gameObject);

        if (newMaterial != null)
        {
            transform.GetComponent<Renderer>().material = newMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnExitTrigger");

        this.objectsWithin.Remove(other.gameObject);

        if (oldMaterial != null)
        {
            transform.GetComponent<Renderer>().material = oldMaterial;
        }
    }

    public int CountObjectsWithin()
    {
        return this.objectsWithin.Count;
    }

    public bool Contains(GameObject gameObject)
    {
        return this.objectsWithin.Contains(gameObject);
    }
}
