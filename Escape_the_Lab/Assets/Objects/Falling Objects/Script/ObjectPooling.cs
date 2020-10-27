using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    public PoolItems[] itemsToPool;    
    private List<GameObject> pulledObjects;

    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;

        pulledObjects = new List<GameObject>();

        foreach(PoolItems item in itemsToPool)
        {
            for(int i = 0; i < item.poolAmount; i++)
            {
                GameObject obj = Instantiate(item.poolObject);
                obj.name = item.name;
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pulledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name)
    {
        //look for inactive objects to use
        for(int i = 0; i < pulledObjects.Count; i++)
        {
            if (pulledObjects[i].transform.position.y < (Camera.main.transform.position.y - Camera.main.orthographicSize))//camera to be changed
                pulledObjects[i].SetActive(false);

            if (pulledObjects[i].activeInHierarchy == false && pulledObjects[i].name == name)
                return pulledObjects[i];
        }

        //If all the pooled objects are active, then recreate more
        foreach (PoolItems item in itemsToPool)
        {
            if(item.poolObject.name == name)
            {
                GameObject obj = Instantiate(item.poolObject);
                obj.name = item.name;
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pulledObjects.Add(obj);
                return obj;
            } 
        }
        return null;
    }


}

[System.Serializable]
public class PoolItems
{
    public string name;
    public int poolAmount;
    public GameObject poolObject;
}