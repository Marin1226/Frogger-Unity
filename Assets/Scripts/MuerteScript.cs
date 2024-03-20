using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteScript : MonoBehaviour
{
    private float timer = 3;
    public bool muerto = false;
    public GameObject mov;
    
    void Update()
    {
        muerto = mov.GetComponent<MovimientoFrogger>().muerto;
        if (muerto)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                mov.transform.position = new Vector3(0.219999999f, -4.51000023f, 0);
                mov.GetComponent<MovimientoFrogger>().muerto = false;
                muerto = false;
                timer = 3f;
                mov.SetActive(true);
            }
        }
    }
}
