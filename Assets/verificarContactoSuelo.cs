using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verificarContactoSuelo : MonoBehaviour
{
    private Mario_controlar personaje;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
       personaje = GetComponentInParent<Mario_controlar>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.tag == "Plataforma")
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
            personaje.transform.parent = colision.transform;
            personaje.tocandoSuelo = true;
        }
    }

    void OnCollisionStay2D(Collision2D colision)
    {
        if (colision.gameObject.tag == "Suelo" | colision.gameObject.tag == "Plataforma")
        {
            personaje.tocandoSuelo = true;
        }

        if (colision.gameObject.tag == "Plataforma")
        {
            personaje.transform.parent = colision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D colision)
    {
        if (colision.gameObject.tag == "Suelo" | colision.gameObject.tag == "Plataforma")
        {
            personaje.tocandoSuelo = false;
        }

        if (colision.gameObject.tag == "Plataforma")
        {
            personaje.transform.parent = null;
        }
    }
}
