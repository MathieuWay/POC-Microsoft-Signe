using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    public GameObject hole;

    private Vector3 posIni;
    private Vector2 mousePosClick;

    void Start()
    {
        posIni = transform.localPosition;
    }

    public void ResetPos()
    {
        if (hole != null)
        {
            hole.transform.parent.GetComponent<Sentences>().EmptyHole(hole.GetComponent<Hole>().holeIndex);
            hole.SetActive(true);
            hole = null;
        }

        transform.localPosition = posIni;
    }

    void OnMouseDown()
    {
        mousePosClick = Input.mousePosition;

        if(hole != null)
        {
            hole.transform.parent.GetComponent<Sentences>().EmptyHole(hole.GetComponent<Hole>().holeIndex);
            hole.SetActive(true);
            hole = null;
        }
    }

    void OnMouseDrag()
    {
        //Debug.Log("Dragging " + (Vector3)((Vector2)Input.mousePosition - mousePosClick));
        transform.localPosition += (Vector3)((Vector2)Input.mousePosition - mousePosClick) * BlocNoteManager.instance.screenRescaleCoef;
        mousePosClick = Input.mousePosition;
    }

    void OnMouseUp()
    {
        if (hole == null)
            transform.localPosition = posIni;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.parent != transform.parent && collision.name.ToLower() == name.ToLower()) {
    //        Debug.Log("Well Played ! " + name);
    //        //collision.transform.parent.gameObject.GetComponent<Sentences>().FillWord(name);
    //        BlocNoteManager.instance.FillHole(collision.transform.parent.gameObject, collision.GetComponent<Hole>().wordIndex, collision.name);
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonUp(0) && hole == null && collision.transform.parent != transform.parent)
        {
            Debug.Log("Well Played ! " + name + " // " + collision.name);

            hole = collision.gameObject;
            transform.position = collision.transform.position;
            collision.gameObject.SetActive(false);

            collision.transform.parent.GetComponent<Sentences>().FillHole(collision.GetComponent<Hole>().holeIndex, name);
        }
    }
}
