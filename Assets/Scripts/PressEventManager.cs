using System.Collections.Generic;
using UnityEngine;

public class PressEventManager : MonoBehaviour
{
    Dictionary<string, GameObject> spawnableObjects;
    public static PressEventManager instance;
    [Header("Spawnable Objects")]
    [SerializeField] GameObject piano;
    List<GameObject> spawnedObjects = new();

    [Header("Sounds")]
    [SerializeField] List<AudioSource> pianoNotes;
    Dictionary<string, AudioSource> soundDict;

    void Awake()
    {
        instance = this;   
    }

    void Start()
    {
        spawnableObjects = new()
        {
            ["piano"] = piano
        };

        soundDict = new()
        {

        };

        foreach(AudioSource _audio in pianoNotes)
        {
            soundDict.Add(_audio.gameObject.name, _audio);
        }
    }

    public void DebugLogSomething(string _debug)
    {
        Debug.Log(_debug);
    }

    public void SpawnObject(string _object)
    {
        GameObject objectToSpawn = spawnableObjects[_object];
        if(!spawnedObjects.Contains(objectToSpawn))
        {
            Instantiate(spawnableObjects[_object], transform.position + (transform.forward * 2), Quaternion.identity);
            spawnedObjects.Add(objectToSpawn);
        }
    }

    public void PlaySound(string _audio)
    {
        soundDict[_audio].Play();
    }
}
