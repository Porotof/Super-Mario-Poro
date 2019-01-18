using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxMenuInicio : MonoBehaviour
{
    public float velocidad;
    private Vector2 posicionFondo;

    // Start is called before the first frame update
    void Start()
    {
        velocidad = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        moverFondo(); 
    }

    void moverFondo()
    {
        posicionFondo += new Vector2(Time.deltaTime * velocidad, 0);
        GetComponent<Renderer>().material.mainTextureOffset = posicionFondo;
        if(posicionFondo.x > 1) //Cuando atraviesa toda la pantalla su posición es 1.
        {
            posicionFondo = new Vector2(-1, 0); //Con -1 queda oculto por la derecha, para poder empezar el ciclo nuevamente.
        }
        //Debug.Log(posicionFondo);
    }
}
