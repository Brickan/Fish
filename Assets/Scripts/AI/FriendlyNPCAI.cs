using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class FriendlyNPCAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private Vector3[] positions;

    [SerializeField]
    private int posNum;
    [SerializeField]
    private Vector2 randomClamp;

    private Vector2 offsetClamp;

    private float newOffset;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        offsetClamp = new Vector2(0.1f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        RandomWalk();
    }

    void RandomWalk()
    {
        if(!agent.hasPath)
        {
            agent.SetDestination(new Vector3(
                transform.position.x + Random.Range(randomClamp.x, randomClamp.y),
                transform.position.y,
                transform.position.z + Random.Range(randomClamp.x, randomClamp.y)));

            newOffset = Mathf.Clamp(Random.Range(agent.baseOffset - 0.2f, agent.baseOffset + 0.2f),offsetClamp.x,offsetClamp.y);
            Debug.Log(agent.destination);
        }
            agent.baseOffset = Mathf.MoveTowards(agent.baseOffset, newOffset, Time.deltaTime * 0.1f);
        transform.LookAt(new Vector3(agent.destination.x, (newOffset * 10) + 1.08f, agent.destination.z));

        Debug.DrawLine(this.transform.position, new Vector3(agent.destination.x, (newOffset * 10) + 1.08f, agent.destination.z), Color.black);

    }
}
