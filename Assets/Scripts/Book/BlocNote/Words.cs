using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    public LayerMask holeLayer;
    public GameObject hole;

    private Vector3 posIni;
    private Vector2 mousePosClick;
    private Collider[] hit;

    private Vector3 pos;

    void Start()
    {
        posIni = transform.localPosition;

        pos = new Vector3(transform.position.x + GetComponent<BoxCollider>().size.x / 8f, transform.position.y - GetComponent<BoxCollider>().size.y / 8f, transform.position.z);

        Debug.DrawLine(pos, pos + GetComponent<BoxCollider>().size / 8f, Color.red, 100f);
        Debug.DrawLine(pos, pos - GetComponent<BoxCollider>().size / 8f, Color.green, 100f);
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
        Debug.LogWarning(name);
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
        transform.localPosition += (Vector3)((Vector2)Input.mousePosition - mousePosClick) * BookManager.instance.screenRescaleCoef;
        mousePosClick = Input.mousePosition;
    }

    void OnMouseUp()
    {
        pos = new Vector3(transform.position.x + GetComponent<BoxCollider>().size.x / 8f, transform.position.y - GetComponent<BoxCollider>().size.y / 8f, transform.position.z);
        hit = Physics.OverlapBox(pos, GetComponent<BoxCollider>().size / 8f, Quaternion.identity, holeLayer);
        
        //Debug.DrawLine(transform.position, transform.position + GetComponent<BoxCollider>().size / 4f, Color.red, 100f);


        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log(hit[i].gameObject.name);

            if (hit[i].transform.parent != transform.parent)
            {
                Debug.Log("Well Played ! " + name + " // " + hit[i].name);

                hole = hit[i].gameObject;
                transform.position = hit[i].transform.position;
                hit[i].gameObject.SetActive(false);

                hit[i].transform.parent.GetComponent<Sentences>().FillHole(hit[i].GetComponent<Hole>().holeIndex, name);
                break;
            }
        }

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

    //private void OnTriggerStay(Collider collision)
    //{
    //    Debug.Log(collision.name);
    //    if (Input.GetMouseButtonUp(0) && hole == null && collision.transform.parent != transform.parent)
    //    {
    //        Debug.Log("Well Played ! " + name + " // " + collision.name);

    //        hole = collision.gameObject;
    //        transform.position = collision.transform.position;
    //        collision.gameObject.SetActive(false);

    //        collision.transform.parent.GetComponent<Sentences>().FillHole(collision.GetComponent<Hole>().holeIndex, name);
    //    }
    //}
}
