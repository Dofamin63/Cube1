using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looks : MonoBehaviour
{
    [SerializeField] private float _movementSpeed; 
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float growthSpeed; 

    void Update()
    {
        transform.position += transform.forward * (_movementSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        transform.localScale += Vector3.one * (growthSpeed * Time.deltaTime);
    }
}
