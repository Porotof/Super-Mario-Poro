using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaInestable : MonoBehaviour
{
    public float delayCaida = 1f;
    public float delayReaparecer = 5f;

    private Rigidbody2D rb2d;
    private Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if(colision.gameObject.CompareTag("Player")){
            Invoke("caer", delayCaida);
            Invoke("reaparecer", delayCaida + delayReaparecer);
        }
    }

    void caer()
    {
        rb2d.isKinematic = false; //Al ser falso queda por defecto Dinamic
    }

    void reaparecer()
    {
        transform.position = posicionInicial;
        rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero; //Vector3.zerolo mismo que Vector3(0f, 0f, 0f)
    }
}
