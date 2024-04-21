using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlasticGenerator : MonoBehaviour
{
    public           GameObject[]                   plasticInstances = new GameObject[15];
    public           Transform                      plasticHolder;
    private readonly Dictionary<string, GameObject> _spawnObjects      = new Dictionary<string, GameObject>();
    private readonly bool[]                         _positionIndexSlot = new bool[6]
                                                                         {
                                                                             false,
                                                                             false,
                                                                             false,
                                                                             false,
                                                                             false,
                                                                             false
                                                                         };

    private readonly Vector3[] _positionForEachIndex = new Vector3[6]
                                                       {
                                                           new Vector3(-8, 3, 9),
                                                           new Vector3(0,4,9),
                                                           new Vector3(8, 3, 9),
                                                           new Vector3(-8, -3, 9),
                                                           new Vector3(0, -4, 9),
                                                           new Vector3(8, -3, 9)
                                                       };
    
    public GameObject panel;

    private void Awake()
    {
        foreach (var obj in plasticInstances)
        {
            GameObject newObj = Instantiate(obj, plasticHolder);
            newObj.name = obj.name;
            newObj.SetActive(false);
            
            _spawnObjects.Add(newObj.name, newObj);
        }
    }
    
    private void Update()
    {
        KeepPositions();
    }

    public void GeneratePlastics()
    {
        for (int i = 0; i < _positionIndexSlot.Length; i++)
        {
            if (_positionIndexSlot[i] == false)
            {
                GameObject obj = FindRandomInstance();
                _spawnObjects[obj.name].transform.localPosition                      = _positionForEachIndex[i];
                _spawnObjects[obj.name].GetComponent<PrefabMetaData>().positionIndex = i;
                _positionIndexSlot[i]                                                = true;
                _spawnObjects[obj.name].SetActive(true);
            }
        }
    }

    private void KeepPositions()
    {
        foreach (var obj in _spawnObjects)
        {
            if (obj.Value.activeSelf)
            {
                int positionIndex = obj.Value.GetComponent<PrefabMetaData>().positionIndex;
                obj.Value.transform.localPosition = _positionForEachIndex[positionIndex];
            }
        }
    }

    private GameObject FindRandomInstance()
    {
        GameObject ret = plasticInstances[Random.Range(0, plasticInstances.Length)];
        if (_spawnObjects[ret.name].activeSelf)
        {
            return FindRandomInstance();
        }
        else
        {
            return ret;
        }
    }

    public void RemoveObjectsWithTag(string ttag, string firstTag)
    {
        foreach (GameObject obj in plasticInstances)
        {
            if (obj.CompareTag(ttag) && _spawnObjects[obj.name].activeSelf && obj.CompareTag(firstTag))
            {
                //Debug.Log("Removed object with tag : " + ttag);
                int curObjectPositionIndex = _spawnObjects[obj.name].GetComponent<PrefabMetaData>().positionIndex;
                _positionIndexSlot[curObjectPositionIndex] = false;
                _spawnObjects[obj.name].SetActive(false);
                
                // Score
                GameManager.sScore++;
                GameManager.sExtraScoreCount++;
                //Debug.Log("New Score plused with plastic : "+ ttag + "Current Score : " + GameManager.sScore);
                DisplayInformation(ttag);
            }
        }
 
    }
    
    private void DisplayInformation(string nname)
    {
        panel.SetActive(true);
        StartCoroutine(DisplayAndWaitSeconds(nname));
    }

    IEnumerator DisplayAndWaitSeconds(string nname)
    {
        GameObject info = panel.transform.Find(nname).gameObject;
        info.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        info.SetActive(false);
        panel.SetActive(false);
    }
}
