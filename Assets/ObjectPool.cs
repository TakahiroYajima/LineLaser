using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    private List<PoolingObject> poolList = new List<PoolingObject>();
    [SerializeField] private PoolingObject poolObjectPref = null;

	public GameObject GetObject()
    {
        if (poolList.Count > 0)
        {
            foreach (var obj in poolList)
            {
                if (!obj.IsActive())
                {
                    return obj.gameObject;
                }
            }
        }

        PoolingObject newInstanceObj = CreateNewInstance();
        poolList.Add(newInstanceObj);
        return newInstanceObj.gameObject;
    }
    
    private PoolingObject CreateNewInstance()
    {
        PoolingObject newObj = Instantiate(poolObjectPref, this.transform);
        newObj.name = poolObjectPref.name + (poolList.Count + 1);
        //newObj.Initialize();
        return newObj;
    }

    public void ClearPool()
    {
        foreach(var obj in poolList)
        {
            Destroy(obj);
        }
        poolList.Clear();
    }
}
