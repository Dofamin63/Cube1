using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{ 
    [SerializeField] private SpawnerCube _spawnerCube;
    
    private const float _explosionForce = 50f;
    private const float _explosionRadius = 80;

    private void OnEnable()
    {
        _spawnerCube.Cubes.ForEach(cube => cube.Destroyed += GetExplodableObjects);
    }

    private void OnDisable()
    {
        _spawnerCube.Cubes.ForEach(cube => cube.Destroyed -= GetExplodableObjects);
    }

    public void ExplosionCubes(List<Cube> cubes, Transform transformTarget, float explosionForce = _explosionForce, float explosionRadius = _explosionRadius)
    {
        foreach (Cube cube in cubes)
        {
            cube?.Rigidbody.AddExplosionForce(explosionForce, transformTarget.position, explosionRadius);
            cube.Destroyed += GetExplodableObjects;
        }
    }

    private void GetExplodableObjects(Transform transformTarget)
    {
        float explosionRadiusByScale = _explosionForce / transformTarget.localScale.x;
        float explosionForceByScale = _explosionRadius / transformTarget.localScale.x;
        
        Collider[] hits = Physics.OverlapSphere(transformTarget.position, _explosionRadius);
        List<Cube> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Cube cube))
            {
                cubes.Add(cube);
            }
        }
        
        ExplosionCubes(cubes, transformTarget, explosionRadiusByScale, explosionForceByScale);
    }
}