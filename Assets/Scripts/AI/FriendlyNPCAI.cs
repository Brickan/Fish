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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //YPos();
        //WalkingLoop();

        RandomWalk();

    }

    void YPos()
    {
        positions[posNum].y = transform.position.y;
    }

    void WalkingLoop()
    {
        if (transform.position != positions[posNum])
        {
            agent.SetDestination(positions[posNum]);
        }
        else if(transform.position == positions[posNum])
        {
            posNum++;
        }
        
        if(posNum == positions.Length && !agent.hasPath)
        {
            posNum = 0;
        }
    }

    void RandomWalk()
    {
        if(!agent.hasPath)
        {
            agent.SetDestination(new Vector3(
                transform.position.x + Random.Range(randomClamp.x, randomClamp.y),
                transform.position.y + Random.Range(randomClamp.x, randomClamp.y),
                transform.position.z + Random.Range(randomClamp.x, randomClamp.y)));
            Debug.Log(agent.destination);
        }
    }
}
