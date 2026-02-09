using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference punch;

    [Header("Punch Data")] 
    [SerializeField] float punchRadius = 0.3f;
    [SerializeField] float punchRange = 1f;

    Life life;

    protected override void Awake()
    {
        base.Awake();
        life = GetComponent<Life>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        punch.action.Enable();
        punch.action.performed += OnPunch;

    }


    protected override void Update()
    {
        base.Update();
        //Leer inputs
        Move(rawMove);
        //Responder inputs
        if (mustPunch)
        {
            mustPunch = false;
            PerformPunch();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Drop drop = other.GetComponent<Drop>();
        if (drop)
        {
            
            life.RecoverHealth(drop.dropDefinition.healthRecovery);
            drop.NotifyPickeUp();
        }
    }

    Vector2 punchDirection = Vector2.down;
    void PerformPunch()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, punchRadius, punchDirection * punchRange);
       
        foreach (RaycastHit2D hit in hits) 
        { 
            BaseCharacter otherbaseCharacter = hit.collider.GetComponent<BaseCharacter>();
            if(otherbaseCharacter != this) 
            { 
                otherbaseCharacter?.NotifyPunch();            
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, punchDirection * punchRange);
    }

    private void OnDisable()
    {
        move.action.Disable();
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        punch.action.Disable();
        punch.action.performed -= OnPunch;
    }

    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.action.ReadValue<Vector2>();
        if (rawMove.magnitude > 0f)
        {
            punchDirection = rawMove.normalized;
        }
    } 
    
    bool mustPunch;
    private void OnPunch(InputAction.CallbackContext context)
    {
        mustPunch = true;
    }
}
