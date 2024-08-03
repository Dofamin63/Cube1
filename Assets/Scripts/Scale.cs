using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private float _speed;
    void Update()
    {
        transform.localScale += Vector3.one * (_speed * Time.deltaTime);
    }
}
