using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocheScript : MonoBehaviour
{
    public float f_vel = 1.0f;
    public GameObject g_opp_truck;
    public Transform respawnPoint;
    public string s_tagWall;

    private Rigidbody2D rb;
    private Transform miTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        miTransform = transform;
        if (!gameObject.activeSelf)
        {
            miTransform.position = respawnPoint.position;
        }
    }

    private void FixedUpdate()
    {

        rb.velocity = Vector3.right * f_vel * Time.fixedDeltaTime + Vector3.up * 0;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == s_tagWall)
        {
            g_opp_truck.SetActive(true);
        }

        if (collision.tag == "borderRespawn")
        {
            if (f_vel > 0 && collision.transform.position.x < transform.position.x)
            {
                return;
            }

            if (f_vel < 0 && collision.transform.position.x > transform.position.x)
            {
                return;
            }

            gameObject.SetActive(false);
            miTransform.position = respawnPoint.position;
        }

    }
}
