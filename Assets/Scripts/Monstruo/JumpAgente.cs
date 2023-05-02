using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpAgente : MonoBehaviour
{
    public Transform destino;

    NavMeshAgent agent;

    bool inOffMeshLink = false;

    public bool InOffMeshLink
    {
        get => inOffMeshLink;
        set
        {
            if (value == inOffMeshLink) return;
            inOffMeshLink = value;
            if (inOffMeshLink) StartCoroutine(Parabola(2f, 1f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        agent.SetDestination(destino.position);
    }

    // Update is called once per frame
    void Update()
    {
        InOffMeshLink = agent.isOnOffMeshLink;
    }

    IEnumerator Parabola(float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime <= 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        agent.CompleteOffMeshLink();
    }
}
