using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputH;
    private float inputV;
    private bool moviendo;
    private Vector3 puntoDestino;
    private Vector3 puntoInteraccion;
    private Vector3 ultimoInput;
    private Collider2D colliderDelante; //Indica el collider de adelante.
    private Animator anim;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float radioInteraccion;
    private bool interactuando;

    public bool Interactuando { get => interactuando; set => interactuando = value; }

    //[SerializeField] private LayerMask queEsColisionable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LecturaInputs();
        MovimientoYAnimaciones();
    }

    private void MovimientoYAnimaciones()
    {
        //Ejecuto el movimiento solo si estoy en una casilla y solo si hay input.
        if (!interactuando && !moviendo && (inputH != 0 || inputV != 0))
        {
            anim.SetBool("andando", true);
            anim.SetFloat("inputH", inputH);
            anim.SetFloat("inputV", inputV);
            ultimoInput = new Vector3(inputH, inputV, 0);
            puntoDestino = transform.position + ultimoInput;
            puntoInteraccion = puntoDestino;

            colliderDelante = LanzarCheck();
            if (!colliderDelante)
            {
                StartCoroutine(Mover());
            }

        }
        else if (inputH == 0 && inputV == 0)
        {
            anim.SetBool("andando", false);
        }
    }

    private void LecturaInputs()
    {
        if (inputV == 0)
        {
            inputH = Input.GetAxisRaw("Horizontal");
        }
        if (inputH == 0)
        {
            inputV = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LanzarInteracion();
        }
    }

    private void LanzarInteracion()
    {
       colliderDelante = LanzarCheck();
        if (colliderDelante != null)
        {
            if (colliderDelante.gameObject.CompareTag("NPC"))
            {
                NPC npcScript = colliderDelante.gameObject.GetComponent<NPC>();
                npcScript.Interactuar();
            }
        }
    }
    IEnumerator Mover()
    {
        moviendo = true;
        while (transform.position != puntoDestino)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoDestino, velocidadMovimiento * Time.deltaTime);
            yield return null;
        }
        puntoInteraccion = transform.position + ultimoInput;
        moviendo = false;
    }
    private Collider2D LanzarCheck()
    {
        return Physics2D.OverlapCircle(puntoInteraccion, radioInteraccion/*, queEsColisionable*/);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoInteraccion, radioInteraccion);
    }
}
