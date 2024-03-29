using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoFrogger : MonoBehaviour
{
    public float salto; 
    public float velocidad;

    private float cantAmp = 0.01f;
    private Vector3 mover;

    private Rigidbody2D rb;
    private Transform miTransform;
    private Animator miAnimator;

    private float timer = 0f;
    private Vector3 posFin;
    private float longitudViaje;
    private float fraccionDelViaje;

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
        if (timer < 0f)
        {
            Mover();
        }

        miTransform.position = Vector3.Lerp(miTransform.position, posFin, fraccionDelViaje);

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

            Debug.Log(fraccionDelViaje);

            timer = .25f;
        }        
    }
}
