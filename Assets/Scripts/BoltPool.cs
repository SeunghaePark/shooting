using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool : MonoBehaviour {

    [SerializeField]
    private Bolt bolt;
    private List<Bolt> boltList;

	// Use this for initialization
	void Awake () {

        boltList = new List<Bolt>();
	}

    public Bolt GetFromPool()
    {
        for(int i=0; i<boltList.Count; i++)
        {
            if(!boltList[i].gameObject.activeInHierarchy)
            {
                return boltList[i];
            }
        }
        Bolt temp = Instantiate(bolt);
        boltList.Add(temp);
        return temp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
