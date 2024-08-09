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
    private int _divider = 2;

    public event Action<Transform, int, float> SplittingCube;
    
    public Rigidbody Rigidbody { get; private set; }
    public float SplitChance { get; private set; } = 1f; 

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnMouseUpAsButton()
    {
        if (transform.localScale.x > _minScale)
        {
            if (Random.value <= SplitChance)
            {
                SplitChance /= _divider;
                int countCube = Random.Range(_minQuantityCube, _maxQuantityCube);
                
                SplittingCube?.Invoke(transform, countCube, SplitChance);
            }
        }

        Destroy(gameObject);
    }
    
    public void Init(Transform transformParent, float splitChanceParent)
    {
        SplitChance = splitChanceParent / _divider;
        transform.localScale = transformParent.localScale / _divider;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}