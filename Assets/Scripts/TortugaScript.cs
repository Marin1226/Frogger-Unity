using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortugaScript : CocheScript
{
    public float tiempoHundir;
    public float tiempoHundirIni; 
    public bool seHunde;
    private bool hundido = false;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private string tagTortuga = "tortuga";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!seHunde)
        {
            return;
        }
        if (tiempoHundir <= 0 && !hundido)
        {
            animator.SetTrigger("Hundirse");
            tiempoHundir = tiempoHundirIni;
            hundido = true;
        }
        else if(tiempoHundir <= 0 && hundido)
        {
            animator.SetTrigger("Flotar");
            tiempoHundir = tiempoHundirIni;
            hundido = false;
        }
        else 
        {
            tiempoHundir -= Time.deltaTime;
        }
        g_opp_truck.GetComponent<TortugaScript>().tiempoHundir = tiempoHundir;
    }

    void Desaparecer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.tag = "Untagged";
    }

    void Aparecer()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        gameObject.tag = tagTortuga;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == s_tagWall)
        {
            g_opp_truck.SetActive(true);
            g_opp_truck.GetComponent<TortugaScript>().hundido = hundido;
            g_opp_truck.GetComponent<SpriteRenderer>().enabled = gameObject.GetComponent<SpriteRenderer>().enabled;
            var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            var tiempo = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            g_opp_truck.GetComponent<Animator>().Play(clip.name, 0, tiempo);
        }

        if (collision.tag == "borderRespawn")
        {
            gameObject.SetActive(false);
            transform.position = respawnPoint.position;
        }

    }
}
