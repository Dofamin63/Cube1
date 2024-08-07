using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _cube;
   
    private List<Cube> _cubes;
    private int _divider = 2;

    public Action<List<Rigidbody>, Vector3> SpawnCub;

    private void Awake()
    {
        _cubes = new List<Cube>(FindObjectsByType<Cube>(FindObjectsSortMode.None));
    }

    private void OnEnable()
    {
        _cubes.ForEach(cube => cube.SplitCube += SpawnCube);
    }
    
    private void OnDisable()
    {
        _cubes.ForEach(cube => cube.SplitCube -= SpawnCube);
    }

    private void SpawnCube(Vector3 position, int countCube)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        
        for (int i = 0; i < countCube; i++)
        {
            Cube newCube = Instantiate(_cube, position, Random.rotation);
            newCube.SplitChance();
            newCube.ReduceScale();
            newCube.SetColor();
            newCube.SplitCube += SpawnCube;
            
            rigidbodies.Add(newCube.Rigidbody);
        }
        
        SpawnCub?.Invoke(rigidbodies, position);
    }
}