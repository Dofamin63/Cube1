using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{ 
    [SerializeField] private SpawnerCube _spawnerCube;
    
    private const float ExplosionForce = 50f;
    private const float ExplosionRadius = 80;

    private void OnEnable()
    {
        _spawnerCube.Cubes.ForEach(cube => cube.Destroyed += GetExplodableObjects);
    }

    private void OnDisable()
    {
        _spawnerCube.Cubes.ForEach(cube => cube.Destroyed -= GetExplodableObjects);
    }

    public void ExplosionCubes(List<Cube> cubes, Transform transformTarget, float explosionForce = ExplosionForce, float explosionRadius = ExplosionRadius)
    {
        foreach (Cube cube in cubes)
        {
            cube?.Rigidbody.AddExplosionForce(explosionForce, transformTarget.position, explosionRadius);
            cube.Destroyed += GetExplodableObjects;
        }
    }

    private void GetExplodableObjects(Transform transformTarget)
    {
        float explosionForceByScale = ExplosionForce / transformTarget.localScale.x;
        float explosionRadiusByScale = ExplosionRadius / transformTarget.localScale.x;
        
        Collider[] hits = Physics.OverlapSphere(transformTarget.position, ExplosionRadius);
        List<Cube> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Cube cube))
            {
                cubes.Add(cube);
            }
        }
        
        ExplosionCubes(cubes, transformTarget, explosionForceByScale, explosionRadiusByScale);
    }
}