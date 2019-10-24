using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerMirage : MonoBehaviour
{

    public float lookRadius = 10f;

    public EspritController esprit;
    public NavMeshAgent agent;
    public Transform _mirage;

    void Start()
    {
        _mirage = PlayerManager.instance.mirage.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_mirage.position, transform.position);


        if (distance <= lookRadius)
        {
            Debug.Log("istrigger");
            agent.SetDestination(_mirage.position);
        }

    }   
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
