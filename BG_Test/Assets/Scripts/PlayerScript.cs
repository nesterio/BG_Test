using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private Transform destination = null;
	UnityEngine.AI.NavMeshAgent agent;

    public void SetTarget(Transform trans)
    {
        destination = trans;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.SetDestination(destination.position);
    }

}
