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
        _spawnerCube.Cubes.ForEach(cube => cube.Destroyed -= ExplosionCubes);
    }

    private void ExplosionCubes(Transform transformTarget)
    {
        _spawnerCube.Cubes.ForEach(cube => cube.Destroyed += ExplosionCubes);
        
        float explosionRadiusByScale = _explosionForce / transformTarget.localScale.x;
        float explosionForceByScale = _explosionRadius / transformTarget.localScale.x;

        foreach (Rigidbody hit in GetExplodableObjects(transformTarget))
        {
            hit.AddExplosionForce(explosionForceByScale, transformTarget.position, explosionRadiusByScale);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Transform transformTarget)
    {
        Collider[] hits = Physics.OverlapSphere(transformTarget.position, _explosionRadius);
        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                rigidbodies.Add(hit.attachedRigidbody);
            }
        }

        return rigidbodies;
    }
}