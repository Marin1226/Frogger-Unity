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


    // Start is called before the first frame update
    void Start()
    {
        mover = Vector3.zero; 
        miTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
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

        float movX = Mathf.Lerp(miTransform.position.x, miTransform.position.x + (mover.x * 64) * cantAmp, 64);
        float movY = Mathf.Lerp(miTransform.position.y, miTransform.position.y + (mover.y * 64) * cantAmp, 64);

        miTransform.position = new Vector3(movX, movY);

    }
}
