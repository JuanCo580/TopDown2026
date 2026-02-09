using System;
using UnityEngine;


public abstract class BaseCharacter : MonoBehaviour, IVisible2D
{
    [SerializeField] float LinearSpeed = 5f;
    [SerializeField] int priority = 0;
    [SerializeField] IVisible2D.Side side;

    Rigidbody2D rb2D;
    Animator animator;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("Horizontal", lastMoveDirection.x);
        animator.SetFloat("Vertical", lastMoveDirection.y);
    }

    Vector2 lastMoveDirection;
    protected void Move(Vector2 direction)
    {
        rb2D.position += direction * LinearSpeed * Time.deltaTime;
        lastMoveDirection = direction;

    }

    internal void NotifyPunch()
    {
        Destroy(gameObject);   
    }

    int IVisible2D.GetPriority()
    {
        return priority;
    }

    IVisible2D.Side IVisible2D.GetSide()
    {
        return side;
    }
}
