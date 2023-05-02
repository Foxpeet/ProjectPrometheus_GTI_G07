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

    public Transform Monster;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Player.position);
    }

    private void Update()
    {
        if (bChase)
        {
            //Debug.Log("HOLA2");
            if (agent.remainingDistance == 0)
            {
                //Debug.Log("HOLA3");
                currentCheckpoint++;
                agent.SetDestination(checkPoints[currentCheckpoint % checkPoints.Length].position);
            }
        }
        if (time != 0)
        {
            StartCoroutine("timer");
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
        //Debug.Log("HOLA");
        enable();
        agent.SetDestination(checkPoints[0].position);
        bChase = true;
    }
    public void chase()
    {
        bChase = false;
        agent.SetDestination(Player.position);
    }
    public void pause()
    {
        agent.Stop();
    }
    public void resume()
    {
        agent.Resume();
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        time = 0;
    }
}
