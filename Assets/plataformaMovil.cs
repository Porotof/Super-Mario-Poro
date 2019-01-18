using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaMovil : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad;

    private Vector3 inicio, final;
    
    // Start is called before the first frame update
    void Start()
    {
        if (objetivo != null)
        {
            objetivo.parent = null;
            inicio = transform.position;
            final = objetivo.position;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(objetivo != null)
        {
            float fixedVelocidad = velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, fixedVelocidad);
        }

        if (transform.position == objetivo.position)
        {
            objetivo.position = (objetivo.position == inicio) ? final : inicio;//Si la posición del objetivo es la de la posición inicial e la plataforma, la iguala a la final. De lo contrario la iguala a la inicial.
        }
    }
}
