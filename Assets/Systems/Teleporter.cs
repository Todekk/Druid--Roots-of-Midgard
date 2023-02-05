using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform GetDestination()
    {
        return destination;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
