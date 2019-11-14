using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    public Material border;
    public Material nonBorder;
    void OnMouseOver()
    {
        GetComponent<Renderer>().material = border;
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material = nonBorder;
    }
}
