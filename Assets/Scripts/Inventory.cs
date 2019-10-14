using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct Struct
    {
        int Id;
    }

    public List<Struct> _inventory;

    private void Start()
    {
        _inventory = new List<Struct>();
    }

    #region UtilityFunction
    public void Show()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public void ShowOrHide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Add(Struct item)
    {
        _inventory.Add(item);
    }

    public void Remove(int id)
    {
        _inventory.RemoveAt(id);
    }
    #endregion
}