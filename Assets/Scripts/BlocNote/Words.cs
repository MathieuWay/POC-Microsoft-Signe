using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    private Vector3 posIni;
    private Vector2 mousePosClick;

    void Start()
    {
        posIni = transform.localPosition;
    }

    void OnMouseDown()
    {
        mousePosClick = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        //Debug.Log("Dragging " + (Vector3)((Vector2)Input.mousePosition - mousePosClick));
        transform.localPosition += (Vector3)((Vector2)Input.mousePosition - mousePosClick) * BlocNoteManager.instance.screenRescaleCoef;
        mousePosClick = Input.mousePosition;
    }

    void OnMouseUp()
    {
        transform.localPosition = posIni;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != transform.parent && collision.name.ToLower() == name.ToLower()) {
            Debug.Log("Well Played ! " + name);
            //collision.transform.parent.gameObject.GetComponent<Sentences>().FillWord(name);
            BlocNoteManager.instance.FillHole(collision.transform.parent.gameObject, collision.GetComponent<Hole>().wordIndex, collision.name);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
