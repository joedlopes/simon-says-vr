using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public ItemCode itemCode;
    public Renderer baseTable;
    public Material defaultColor;

    public List<CubeItem> attachedItems;
    
    void Start()
    {
        baseTable.material = defaultColor;
    }

    public void ClearTable()
    {
        attachedItems.Clear();
        baseTable.material = defaultColor;
    }

    public void SetCode(ItemCode newCode)
    {
        itemCode = newCode;
    }

    public void SetColor(Material color)
    {
        baseTable.material = color;
    }

    public void OnItemAttached(CubeItem item)
    {
        if (attachedItems.Contains(item) == false)
        {
            attachedItems.Add(item);
        }
        GameManager.Instance.CheckSequence();
    }

    public void OnItemRemoved(CubeItem item)
    {
        if (attachedItems.Contains(item))
        {
            attachedItems.Remove(item);
        }
    }

    public bool HasTheCorrectItem()
    {

        foreach(CubeItem item in attachedItems)
        {
            if (item.code == itemCode)
            {
                return true;
            }
        }

        return false;
    }

}


