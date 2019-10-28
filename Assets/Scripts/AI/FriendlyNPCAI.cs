using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class FriendlyNPCAI : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private Vector2 randomClamp;

    private Vector2 offsetClamp;

    private float targetOffset;

    private Quaternion rotation;

    private bool oneLook;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        offsetClamp = new Vector2(0.2f, 0.7f);

        randomClamp = (randomClamp == Vector2.zero) ? new Vector2(-100, 100) : randomClamp;
    }

    // Update is called once per frame
    void Update()
    {
        RandomWalk();
    }

    void RandomWalk()
    {
        if (!agent.hasPath)
        {
            oneLook = false;
            agent.SetDestination(new Vector3(
                transform.position.x + Random.Range(randomClamp.x, randomClamp.y),
                transform.position.y,
                transform.position.z + Random.Range(randomClamp.x, randomClamp.y)));

            targetOffset = Mathf.Clamp(Random.Range(agent.baseOffset - 0.2f, agent.baseOffset + 0.2f), offsetClamp.x, offsetClamp.y);

        }

        agent.baseOffset = Mathf.MoveTowards(agent.baseOffset, targetOffset, Time.deltaTime * 0.01f);


        if (!oneLook)
        {
            transform.LookAt(new Vector3(agent.destination.x, (targetOffset * 100) + 6.45615f, agent.destination.z));
            rotation = transform.rotation;
            oneLook = true;
        }
        else
        {
            transform.rotation = rotation;
        }

        Debug.DrawLine(transform.position, new Vector3(agent.destination.x, (targetOffset * 100) + 6.45615f, agent.destination.z), Color.black);


    }
}
