﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private void Start()
    {
        
    }

    void Update()
    {
        if (code[0] == codeattendu[0] && code[1] == codeattendu[1] && code[2] == codeattendu[2] && code[3] == codeattendu[3])
        {
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
                    if (selectednumber == 0) {
                    code[0] = id;
                    selectednumber++;
                    }
                    else if (selectednumber == 1)
                    {
                        code[1] = id;
                        selectednumber++;
                    }
                    else if (selectednumber == 2)
                    {
                        code[2] = id;
                        selectednumber++;
                    }
                    else if (selectednumber == 3)
                    {
                        code[3] = id;
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
                }
            }
            if (selection.CompareTag(usableTag))
            {
                 _selection = selection;
                selectionné = selection.gameObject;
                DisplayName displayScript = selectionné.GetComponent<DisplayName>();
                displayScript.ShowText();
            }
            
            
        }

    }
}
