using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour
{
    public LayerMask holeLayer;
    public GameObject hole;

    public GameObject ghost;

    private Vector3 posIni;
    private Vector2 mousePosClick;
    private Collider[] hit;

    private Vector3 pos;

    void Start()
    {
        posIni = transform.localPosition;

        CreateGhost();

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

        ghost.SetActive(false);

        transform.localPosition = posIni;
        GetComponent<Rigidbody>().position = transform.position;
    }

    public void CalculPos()
    {
        posIni = transform.localPosition;
        ghost.transform.position = transform.position;
        GetComponent<Rigidbody>().position = transform.position;

        if (hole != null)
            MoveToHole(hole.transform.position);
    }

    public void CreateGhost()
    {
        ghost = Instantiate(ghost, BookManager.instance.blocNote.ghosts);
        ghost.transform.position = transform.position;
        ghost.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        ghost.name = name;
        ghost.transform.GetChild(0).GetComponent<Text>().text = name;
        ghost.SetActive(false);
    }

    public void Shake(float time)
    {
        StartCoroutine("Shaking", time);
    }

    IEnumerator Shaking(float time)
    {
        int temp = 1;
        for (int i = 0; i < 16; i += 1)
        {
            transform.localPosition += new Vector3(0.3f, 0, 0) * temp;
            if (i == 4 || i == 12)
                temp *= -1;

            yield return new WaitForSecondsRealtime(time / 16f);
        }

        transform.localPosition = posIni;
        GetComponent<Rigidbody>().position = transform.position;
    }

    void OnMouseDown()
    {
        //Debug.LogWarning(name);
        mousePosClick = Input.mousePosition;

        //ResetPos();
        if (hole != null)
        {
            hole.transform.parent.GetComponent<Sentences>().EmptyHole(hole.GetComponent<Hole>().holeIndex);
            hole.SetActive(true);
            hole = null;
        }
    }

    void OnMouseDrag()
    {
        ghost.SetActive(true);
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
            //Debug.Log(hit[i].gameObject.name);

            if (hit[i].transform.parent != transform.parent)
            {
                //Debug.Log("Well Played ! " + name + " // " + hit[i].name);

                hole = hit[i].gameObject;
                hit[i].gameObject.SetActive(false);

                MoveToHole(hole.transform.position);

                hit[i].transform.parent.GetComponent<Sentences>().FillHole(hit[i].GetComponent<Hole>().holeIndex, gameObject);
                break;
            }
        }

        if (hole == null)
            ResetPos();
    }

    private void OnDestroy()
    {
        Destroy(ghost);
    }

    private void MoveToHole(Vector3 holePos)
    {
        transform.position = holePos;
        GetComponent<Rigidbody>().position = transform.position;
    }
}
