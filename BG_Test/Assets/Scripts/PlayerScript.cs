using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private Transform destination = null;
	UnityEngine.AI.NavMeshAgent agent;

	private Vector3 defaultPosition;

	[Header("Shield configuration")]
	[SerializeField]private Color defaultColor;
	[SerializeField]private Color shieldedColor;

	[SerializeField]private static bool isShielded;

	[SerializeField]private float defaultShieldTime;
	private float shieldTimeLeft;

	[Space]

	[SerializeField]private float TimeBeforeStart;


	public void StartGame(Transform trans)
	{
		defaultPosition = transform.localPosition;

		shieldTimeLeft = defaultShieldTime;

		gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = defaultColor;

		destination = trans;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        StartCoroutine(StartMovement());
	}

    IEnumerator StartMovement()
    {
        yield return new WaitForSeconds(TimeBeforeStart);

        agent.SetDestination(destination.position);
    }

    public void EnableShield()
    {
    	if(shieldTimeLeft >= 0)
    	{
    		isShielded = true;
    		gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = shieldedColor;
    	}	
    }
    public void DisableShield()
    {
    		isShielded = false;
    		gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = defaultColor;
    }

    void OnTriggerEnter(Collider col)
    {
    	if(col.CompareTag("Trap") && isShielded == false)
    	{
    		agent.enabled = false;
    		transform.localPosition = defaultPosition;
    		shieldTimeLeft = defaultShieldTime;
    		agent.enabled = true;
    		agent.SetDestination(destination.position);
    	}

    	if(col.CompareTag("Destination"))
    	{
    		GetComponentInChildren<ParticleSystem>().Play();
    		GameObject.FindWithTag("Manager").GetComponent<ManagerScript>().FadeIn();
    	}
    }

    void Update()
    {
    	if(isShielded && shieldTimeLeft > 0)
    	{
    		shieldTimeLeft -= Time.deltaTime;
    	}

    	if(shieldTimeLeft <= 0 && isShielded == true)
    	{
    		isShielded = false;
    		gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = defaultColor;
    	}
    }

}
