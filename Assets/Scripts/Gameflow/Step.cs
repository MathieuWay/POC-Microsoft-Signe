using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Step : MonoBehaviour
{
    /*Ne pas utilisé directement ce script !*/

    /* Pour développer un nouveau type de trigger pour le tutorial, crée un nouveau script faite le hérité de ce script puis définir la fonction update*/

    public int order;
    [TextArea(2, 10)]
    public string explanation;

    private void Awake()
    {
        GameflowManager.Instance.steps.Add(this);
    }

    public virtual void update()
    {

    }

    public int GetOrder()
    {
        return order;
    }
}