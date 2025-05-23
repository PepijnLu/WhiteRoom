using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class KeyAction
{
    public string key; // You could use char instead
    public UnityEvent onKeyPressed;
}