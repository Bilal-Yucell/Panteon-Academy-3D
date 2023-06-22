using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    public NavMeshAgent OpponentAgent;
    public GameObject Target;
    Vector3 OpponentStartPos;
    public GameObject speedBoosterIcon;

    void Start()
    {
        OpponentAgent = GetComponent<NavMeshAgent>();
        OpponentStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        speedBoosterIcon.SetActive(false);
    }

    void Update()
    {
        OpponentAgent.SetDestination(Target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Touched Obstacle!");
            transform.position = OpponentStartPos;
        }

        if (collision.gameObject.CompareTag("BumberObs"))
        {
            Debug.Log("Touched BumberObs!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Speedboost"))
        {
            OpponentAgent.speed = OpponentAgent.speed + 3f;
            speedBoosterIcon.SetActive(true);
            StartCoroutine(SlowAfterAWhileCoroutine());
        }
    }

    private IEnumerator SlowAfterAWhileCoroutine()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        OpponentAgent.speed = OpponentAgent.speed - 3f;
        speedBoosterIcon.SetActive(false);
    }

}
