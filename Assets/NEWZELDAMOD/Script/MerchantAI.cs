using UnityEngine;
using UnityEngine.AI;

public class MerchantAI : MonoBehaviour
{
    public Transform[] interestPoints;
    public GameObject uniqueItem;
    public string requiredItem = "SpecialResource";

    private NavMeshAgent agent;
    private int currentPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextPoint();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        currentPoint = (currentPoint + 1) % interestPoints.Length;
        agent.SetDestination(interestPoints[currentPoint].position);
    }

    public void Trade(string playerItem)
    {
        if (playerItem == requiredItem)
        {
            GiveReward();
        }
        else
        {
            Debug.Log("O comerciante não quer esse item.");
        }
    }

    void GiveReward()
    {
        Instantiate(uniqueItem, transform.position + Vector3.up, Quaternion.identity);
    }
}
