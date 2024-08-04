using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private int _minQuantityCube; 
    [SerializeField] private int _maxQuantityCube;
    [SerializeField] private float _explosionForce; 
    [SerializeField] private float _explosionRadius;
    private float _splitChance = 1f; 
    private readonly float _minScale = 0.3f; 

    private void OnMouseDown()
    {
        if (transform.localScale.x <= _minScale)
        {
            Destroy(gameObject);
            return;
        }

        if (Random.value <= _splitChance)
        {
            _splitChance /= 2f;

            int cubesToSpawn = Random.Range(_minQuantityCube, _maxQuantityCube);
            
            for (int i = 0; i < cubesToSpawn; i++)
            {
                SpawnCube();
            }
        }

        Destroy(gameObject);
    }

    private void SpawnCube()
    {
        CubeManager newCube = Instantiate(this, transform.position, Random.rotation);

        newCube.transform.localScale = transform.localScale / 2f;
        Renderer renderer = newCube.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.value, Random.value, Random.value);

        Rigidbody rb = newCube.GetComponent<Rigidbody>();
        rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}