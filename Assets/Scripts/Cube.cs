using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minQuantityCube;
    [SerializeField] private int _maxQuantityCube;
    [SerializeField] private float _minScale;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    private float _splitChance = 1f;
    private int _divider = 2;

    private void OnMouseUpAsButton()
    {
        if (transform.localScale.x <= _minScale)
        {
            Destroy(gameObject);
            return;
        }

        if (Random.value <= _splitChance)
        {
            _splitChance /= _divider;
            int countCube = Random.Range(_minQuantityCube, _maxQuantityCube);

            for (int i = 0; i < countCube; i++)
            {
                SpawnCube();
            }
        }

        Destroy(gameObject);
    }

    private void SpawnCube()
    {
        Cube newCube = Instantiate(this, transform.position, Random.rotation);
        newCube.transform.localScale = transform.localScale / _divider;
        newCube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        newCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}