using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EspritController : MonoBehaviour
{

    public float lookRadius = 10f;

  // public Transform Mirage;
    Transform _target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        _target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(_target.position);

            if(distance <= agent.stoppingDistance)
            {
                FaceTarget(); 
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
      
    }

    //void OnTriggerEnter(Collider other)
   // {
    //    if (other.tag == "Esprit")
     //   {
     //       Debug.Log("istrigger");
     //       agent.SetDestination(Mirage.position);
     //   }
   // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
