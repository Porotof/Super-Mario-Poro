using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dibujarLineaEscena : MonoBehaviour
{
    public Transform desde;
    public Transform hasta;

    private void OnDrawGizmosSelected()
    {
        if(desde != null & hasta != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(desde.position, hasta.position);
            Gizmos.DrawSphere(desde.position, 0.15f);
            Gizmos.DrawSphere(hasta.position, 0.15f);
        }
    }
}
