using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.SpawnedCubes += ExplosionCubes;
    }
    
    private void OnDisable()
    {
        _spawnerCube.SpawnedCubes -= ExplosionCubes;
    }

    private void ExplosionCubes(List<Cube> cubes, Vector3 position)
    {
        foreach (Cube cube in cubes)
        {
            cube.Rigidbody?.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
    }
}