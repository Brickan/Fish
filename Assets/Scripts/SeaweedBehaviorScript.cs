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

    private enum streamDir { left, right, forward, backward};
    [Tooltip("Define the direction of the waves.")]
    [SerializeField] private streamDir streamDirection;
    #endregion


    void Start()
    {
        // Setting up the rigidbodies.
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.useGravity = false;
            rb.mass = mass;


        }


        // Setting up the 'streams' direction.
        Vector3 forceDirection = Vector3.zero;
        switch (streamDirection)
        {
            case streamDir.left:
                forceDirection = Vector3.left;
                break;

            case streamDir.right:
                forceDirection = Vector3.right;
                break;

            case streamDir.forward:
                forceDirection = Vector3.forward;
                break;

            case streamDir.backward:
                forceDirection = Vector3.back;
                break;

            default:
                forceDirection = Vector3.right;
                break;
        }

        // I'm making the original value smaller, because it allows for larger (and therefore) prettier values in the inspector.
        stream /= 100;

        // Applying the force to start the cool wave effect.
        rigidbodies[rigidbodies.Count - 1].AddForce(forceDirection * stream, ForceMode.Force);
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