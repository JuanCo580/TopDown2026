using System;
using Unity.Mathematics;
using UnityEngine;

public class Sight2D : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float checkFrecuency = 5f;
    [Space]
    [SerializeField] IVisible2D.Side[] perceivedSides;

    Transform closestTarget;
    float distanceToClosestTarget;
    int priorityOfClosestTarget;

    float lastCheckTime;
    Collider2D[] colliders;
    private void Update()
    {
        if((Time.time - lastCheckTime) > (1f/ checkFrecuency))
        {
            lastCheckTime = Time.time;

            Debug.Log("Checking sight");
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            closestTarget = null;
            distanceToClosestTarget = Mathf.Infinity;
            priorityOfClosestTarget = -1;

            for (int i = 0; i < colliders.Length; i++)
            {
                IVisible2D visible = colliders[i].GetComponent<IVisible2D>();
                if ((visible != null) && (CanSee(visible)))
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, colliders[i].transform.position);
                    if(
                        (visible.GetPriority() > priorityOfClosestTarget) ||
                        ((visible.GetPriority() == priorityOfClosestTarget) && (distanceToPlayer < distanceToClosestTarget)) 
                      )
                        
                    {
                        closestTarget = colliders[i].transform;
                        distanceToClosestTarget = distanceToPlayer;
                        priorityOfClosestTarget = visible.GetPriority();
                    }
                }
            }
        }
    }

    bool CanSee(IVisible2D visible)
    {
        bool canSee = false;
        for(int i = 0; !canSee && (i < perceivedSides.Length); i++)
        {
            canSee = visible.GetSide() == perceivedSides[i];
        }
        return canSee;
    }

    public Transform GetClosestTarget()
    {
        return closestTarget;
    }

    public bool IsPlayerInSight()
    {
        bool isPlayerInSight = false;
        for(int i = 0; !isPlayerInSight && (i < colliders.Length); i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerInSight = true;
            }
        }
        return isPlayerInSight;
    
    }
}