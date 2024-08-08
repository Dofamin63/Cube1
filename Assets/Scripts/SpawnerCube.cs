using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
   
    private List<Cube> _cubes;

    public Action<List<Rigidbody>, Vector3> SpawnCub;

    private void OnEnable()
    {
        Cube.SplitCube += SpawnCube;
    }
    
    private void OnDisable()
    {
        Cube.SplitCube += SpawnCube;
    }

    private void SpawnCube(Vector3 position, int countCube)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        
        for (int i = 0; i < countCube; i++)
        {
            Cube newCube = Instantiate(_prefab, position, Random.rotation);
            newCube.Init();
            rigidbodies.Add(newCube.Rigidbody);
        }
        
        SpawnCub?.Invoke(rigidbodies, position);
    }
}