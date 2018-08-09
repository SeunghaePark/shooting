using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPool : MonoBehaviour {

    [SerializeField]
    BoltPool BossBoltPool;

    [SerializeField]
    private BossController Boss;

    private List<BossController> BossList;

    // Use this for initialization
    void Start()
    {
        BossList = new List<BossController>();
    }

    public BossController GetFromPool()
    {
        for (int i = 0; i < BossList.Count; i++)
        {
            if (!BossList[i].gameObject.activeInHierarchy)
            {
                return BossList[i];
            }
        }
        BossController newBoss = Instantiate(Boss);
        newBoss.SetBoltPoop(BossBoltPool);
        BossList.Add(newBoss);
        return newBoss;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

