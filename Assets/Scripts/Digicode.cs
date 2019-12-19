using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Digicode : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Digicode";
    [SerializeField] private string clearTag = "DigicodeClear";
    [SerializeField] private string usableTag = "Usable";
    public GameObject selectionné;
    private Transform _selection;
    public int id, selectednumber;
    public int[] code = new int[4];
    public int[] codeattendu = new int[4];
    public Text num1, num2, num3, num4;
    public GameObject GameObject;
    public GameObject Code;

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (code[0] == codeattendu[0] && code[1] == codeattendu[1] && code[2] == codeattendu[2] && code[3] == codeattendu[3])
        {

            Destroy(GameObject);
            Destroy(Code);
            Debug.Log("Le coffre s'ouvre");
           

            // mettre l'animation du coffre ici 
        }

        if(_selection !=null)
        { 

        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AudioManager.PlaySound("button");
                    if (selectednumber == 0) {
                        code[0] = id;
                        num1.text = id.ToString();
                        selectednumber++;
                    }
                    else if (selectednumber == 1)
                    {
                        code[1] = id;
                        num2.text = id.ToString();
                        selectednumber++;
                    }
                    else if (selectednumber == 2)
                    {
                        code[2] = id;
                        num3.text = id.ToString();
                        selectednumber++;
                    }
                    else if (selectednumber == 3)
                    {
                        code[3] = id;
                        num4.text = id.ToString();
                        selectednumber++;
                    }

                }
                
                _selection = selection;
                selectionné = selection.gameObject;
                DigicodeButton controlscript = selectionné.GetComponent<DigicodeButton>();
                id = controlscript.id;
            }
            if (selection.CompareTag(clearTag))
            {
                if (Input.GetMouseButtonDown(0)) { 
                selectednumber = 0;
                Array.Clear(code, 0, code.Length);
                    num1.text = 0.ToString();
                    num2.text = 0.ToString();
                    num3.text = 0.ToString();
                    num4.text = 0.ToString();

                }
            }
            
            
        }

    }
}
