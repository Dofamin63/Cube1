using System;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    [SerializeField] private int _mouseButton;
    public Action OnClickMouse;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(_mouseButton))
        {
           OnClickMouse?.Invoke();
        }
    }
}