using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private int _divider = 2;

    public event Action<Transform, float> Splitting;
    public event Action<Transform> Destroyed;

    public float SplitChance { get; private set; } = 1f;
    public Rigidbody Rigidbody { get; private set; }

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
        else
        {
            Destroyed?.Invoke(transform);
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