using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minQuantityCube;
    [SerializeField] private int _maxQuantityCube;
    [SerializeField] private float _minScale;

    private Renderer _renderer;
    private float _splitChance = 1f;
    private int _divider = 2;

    public static Action<Vector3, int> SplitCube;
    
    public Rigidbody Rigidbody { get; private set;  }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnMouseUpAsButton()
    {
        if (transform.localScale.x > _minScale)
        {
            if (Random.value <= _splitChance)
            {
                _splitChance /= _divider;
                int countCube = Random.Range(_minQuantityCube, _maxQuantityCube);
                
                SplitCube?.Invoke(transform.position, countCube);
            }
        }

        Destroy(gameObject);
    }
    
    public void Init()
    {
        _splitChance /= _divider;
        transform.localScale /= _divider;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}