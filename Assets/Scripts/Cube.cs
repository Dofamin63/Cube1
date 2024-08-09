using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minScale;

    private Renderer _renderer;
    private int _divider = 2;

    public event Action<Transform, float> Splitting;

    public Rigidbody Rigidbody { get; private set; }
    public float SplitChance { get; private set; } = 1f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        if (Random.value <= SplitChance)
        {
            Splitting?.Invoke(transform, SplitChance);
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