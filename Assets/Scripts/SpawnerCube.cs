using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private int _minQuantityCube;
    [SerializeField] private int _maxQuantityCube;

    public event Action<List<Cube>, Vector3> SpawnedCubes;

    public List<Cube> Cubes => new (_cubes.RemoveAll(cube => cube == null));

    private void OnEnable()
    {
        _cubes.ForEach(cube => cube.Splitting += Spawn);
    }
    
    private void OnDisable()
    {
        _cubes.ForEach(cube => cube.Splitting -= Spawn);
    }

    private void Spawn(Transform transform, float splitChance)
    {
        int countCube = Random.Range(_minQuantityCube, _maxQuantityCube);
            
        for (int i = 0; i < countCube; i++)
        {
            Cube newCube = Instantiate(_prefab, transform.position, Random.rotation);
            newCube.Init(transform, splitChance);
            newCube.Splitting += Spawn;
            _cubes.Add(newCube);
        }

        SpawnedCubes?.Invoke(Cubes, transform.position);
    }
}