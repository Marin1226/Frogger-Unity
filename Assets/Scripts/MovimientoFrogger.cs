using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoFrogger : MonoBehaviour
{
    public float salto; 
    public float velocidad;
    public bool montado = false;
    public LayerMask notFrogger;
    public bool muerto = false;

    private float cantAmp = 0.01f;
    private Vector3 mover;

    private Rigidbody2D rb;
    private Transform miTransform;
    private Animator miAnimator;
    private float timer = 0f;
    private float timerMuerto = 3f;
    private Vector3 posFin;
    private float longitudViaje;
    private float fraccionDelViaje;
    private bool puedeMorir = true;

    // Start is called before the first frame update
    void Start()
    {
        mover = Vector3.zero; 
        miTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timerMuerto < 0f)
        {
            muerto = false;
            timerMuerto = 3f;
            this.gameObject.SetActive(true);
            miTransform.position = new Vector3(0.219999999f, -4.51000023f, 0);
        }

        if (timer < 0f)
        {
            Mover();
        }


        if (mover != Vector3.zero)
        {
            miTransform.position = Vector3.Lerp(miTransform.position, posFin, fraccionDelViaje);
            miTransform.parent = null;
            montado = false;
            rb.velocity = Vector3.zero;
            puedeMorir = false;
        } else
        {
            puedeMorir = true;
        }

        if (montado && miTransform.parent != null)
        {
            rb.velocity = miTransform.parent.GetComponent<Rigidbody2D>().velocity;
            if (!miTransform.parent.GetComponent<SpriteRenderer>().enabled)
            {
                miTransform.parent = null;
                montado = false;
            }
        }


        if (puedeMorir)
        {
            ComprobarColision();
        }
        
    }

    private void FixedUpdate()
    {

        miAnimator.SetFloat("velocidad_x", mover.x);
        miAnimator.SetFloat("velocidad_y", mover.y);
    }


    public void Mover()
    {
        mover = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            mover = new Vector3(-1f, mover.y);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            mover = new Vector3(1f, mover.y);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mover = new Vector3(mover.x, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mover = new Vector3(mover.x, -1f);
        }

        if (mover != Vector3.zero)
        {

            float movX = miTransform.position.x + (mover.x * 64) * cantAmp;
            float movY = miTransform.position.y + (mover.y * 64) * cantAmp;

            posFin = new Vector3(movX, movY);

            longitudViaje = Vector3.Distance(miTransform.position, posFin);

            fraccionDelViaje = 0.02f / longitudViaje;

            timer = .25f;
        }        
    }

    public void ComprobarColision()
    {
        if (muerto)
        {
            return;
        }
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        Vector3 topRight = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y);
        Vector3 bottomLeft = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
        var colisiones = Physics2D.OverlapAreaAll(topRight, bottomLeft, notFrogger);
        foreach (var colision in colisiones)
        {
            if (colision.CompareTag("ganar"))
            {
                SceneManager.LoadScene(2);
            }

            if (colision.CompareTag("muerte"))
            {
                if (montado || miTransform.parent != null)
                {
                    return;
                }
                miAnimator.SetTrigger("morir");
                muerto = true;
            }

            Debug.Log(colision.tag);
            if (colision.CompareTag("carretera"))
            {
                montado = false;
                miTransform.parent = null;
                return;
            }
            if (colision.CompareTag("tortuga"))
            {
                if (colision.gameObject.GetComponent<SpriteRenderer>().enabled)
                {
                    montado = true;
                    miTransform.parent = colision.gameObject.transform;    
                }
                return;
            }

        }
    }


    public void Morir()
    {
        gameObject.SetActive(false);
    }
}
