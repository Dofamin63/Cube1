using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private List<Cube> _cubes;

    private List<Cube> _newCubes;
    
    public event Action<List<Cube>, Vector3> SpawnCub;

    public List<Cube> Cubes => new (_newCubes);

    private void OnEnable()
    {
        _cubes.ForEach(cube => cube.SplittingCube += SpawnCube);
    }
    
    private void OnDisable()
    {
        _cubes.ForEach(cube => cube.SplittingCube -= SpawnCube);
        _newCubes.ForEach(cube => cube.SplittingCube -= SpawnCube);
    }

    private void SpawnCube(Transform transform, int countCube, float splitChance)
    {
        List<Cube> tempCubes = new List<Cube>(); 
            
        for (int i = 0; i < countCube; i++)
        {
            Cube newCube = Instantiate(_prefab, transform.position, Random.rotation);
            newCube.Init(transform, splitChance);
            newCube.SplittingCube += SpawnCube;
            tempCubes.Add(newCube);
        }

        _newCubes = tempCubes;
        SpawnCub?.Invoke(Cubes, transform.position);
    }
}