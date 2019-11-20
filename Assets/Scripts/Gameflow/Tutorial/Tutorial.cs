using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    /*Ne pas utilisé directement ce script !*/

    /* Pour développer un nouveau type de trigger pour le tutorial, crée un nouveau script faite le hérité de ce script puis définir la fonction CheckIfHappening qui est appeler a chaque update*/

    public int order;
    [TextArea(2, 10)]
    public string explanation;

    private void Awake()
    {
        TutorialManager.Instance.Tutorials.Add(this);
    }

    public virtual void CheckIfHappening()
    {

    }

    public int GetOrder()
    {
        return order;
    }
}