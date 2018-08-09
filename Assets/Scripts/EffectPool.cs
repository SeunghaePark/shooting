﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    expAsteroid,
    expEnemy,
    expPlayer
};
public class EffectPool : MonoBehaviour {
    [SerializeField]
    private GameObject[] effect;
    private List<GameObject>[] effectList;

    // Use this for initialization
    void Start()
    {

        effectList = new List<GameObject>[effect.Length];
        for (int i = 0; i < effectList.Length; i++)
        {
            effectList[i] = new List<GameObject>();
        }
    }
    //asteroidList 는 Boltpool과 다르게 하나가 아니라 3개의 객체 각각의 리스트가 필요함으로 
    //객체에대한 length와 3개의 객체에대한 length 까지두번이 들어가야한다. 

    public GameObject GetFromPool(eEffectType input)

    {
        int index = (int)input;
        for (int i = 0; i < effectList[index].Count; i++)
        {
            if (!effectList[index][i].gameObject.activeInHierarchy)
            {
                return effectList[index][i];
            }
        }

        GameObject temp = Instantiate(effect[index]);
        effectList[index].Add(temp);
        return temp;
    }
    public GameObject GetFromPool(eItemType input)

    {
        int index = (int)input;
        for (int i = 0; i < effectList[index].Count; i++)
        {
            if (!effectList[index][i].gameObject.activeInHierarchy)
            {
                return effectList[index][i];
            }
        }

        GameObject temp = Instantiate(effect[index]);
        effectList[index].Add(temp);
        return temp;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
