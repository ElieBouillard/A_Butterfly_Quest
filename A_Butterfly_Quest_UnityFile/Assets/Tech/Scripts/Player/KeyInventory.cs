using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    public static KeyInventory instance;

    private int _numberOfKey;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        HudUpdate();
    }

    private void HudUpdate()
    {
        UIManager.instance.KeyCountTxt.text = _numberOfKey.ToString();
    }

    [ContextMenu("AddKeyToInvetory")]
    public void AddKey()
    {
        _numberOfKey++;
    }

    public void AddKeyToInvetory(int value = 1)
    {
        _numberOfKey += value;
    }

    public void RemoveKeyFromInventory(int value = 1)
    {
        _numberOfKey -= value;
    }

    public int GetKeyCount()
    {
        return _numberOfKey;
    }
}
