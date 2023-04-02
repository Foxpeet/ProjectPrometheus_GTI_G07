using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgenteNav : MonoBehaviour
{
    public Transform[] checkPoints;
    public Transform Player;
    NavMeshAgent agent;
    public float time;
    private bool bChase = false;
    private int currentCheckpoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Player.position);
    }

    private void Update()
    {
        if (time != 0)
        {
            StartCoroutine("timer");
        }
        if (bChase)
        {
            if(agent.remainingDistance == 0)
            {
                currentCheckpoint++;
                agent.SetDestination(checkPoints[currentCheckpoint % checkPoints.Length].position);
            }
        }
        if(time == 0 && !bChase)
        {
            agent.SetDestination(Player.position);
        }
    }

    public void disable()
    {
        agent.enabled = false;
    }
    public void enable()
    {
        agent.enabled = true;
    }
    public void patrol()
    {
        bChase = true;
        agent.SetDestination(checkPoints[0].position);
    }
    public void chase()
    {
        bChase = false;
        agent.SetDestination(Player.position);
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        time = 0;
    }
}
