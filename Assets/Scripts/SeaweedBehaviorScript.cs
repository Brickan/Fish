using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedBehaviorScript : MonoBehaviour
{
    #region Global variables
    [Tooltip("Use this variable to override all of the masses in the hierarcy. This will affect the gravitational pull upwards.")]
    [SerializeField] private float mass = 0.01f;

    [Tooltip("Define the power of the oceanic streams. Kinda.")]
    [SerializeField] private float stream = 1.0f;

    [SerializeField] private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    #endregion


    void Start()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.useGravity = false;
            rb.mass = mass;


        }

        stream /= 100;
        rigidbodies[rigidbodies.Count - 1].AddForce(Vector3.forward * stream, ForceMode.Force);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        CustomGravity();
    }


    /// <summary>
    /// Handles the custom reversed gravity.
    /// </summary>
    private void CustomGravity ()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddForce(-Physics.gravity * (rb.mass * rb.mass), ForceMode.Force);
        }
    }
}