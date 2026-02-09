using UnityEngine;

public class BaseSkeleton : BaseCharacter
{

    Sight2D sight;

    protected override void Awake()
    {
        base.Awake();
        sight = GetComponent<Sight2D>();
    }

    protected override void Update()
    {
        base.Update();
        Transform closestTarget = sight.GetClosestTarget();
        if (closestTarget != null)
        {
            Move((closestTarget.position - transform.position).normalized);
        }
    }
}
