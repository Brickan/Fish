using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedBehaviorScript : MonoBehaviour
{
    #region Global variables
    [SerializeField] private List<Rigidbody> rigidbodies;
    #endregion


    void Start()
    {
        Setup();
    }


    void Update()
    {
        
    }


    /// <summary>
    /// Sets everything up for hot seaweed action.
    /// </summary>
    private void Setup ()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();

        // Gets all of the rigidbodies. 
        while (true)
        {
            rigidbodies.Add(rb);

            if (rb.GetComponentInChildren<Rigidbody>())
                rb = rb.GetComponentInChildren<Rigidbody>();
            else
                break;
        }

    }
}
