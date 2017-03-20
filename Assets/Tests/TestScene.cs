using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour {

    public GameObject g;

	// Use this for initialization
	void Start () {
        GameObject mainMenu = Instantiate(Resources.Load("PauseMenu")) as GameObject;
        mainMenu.transform.SetParent(g.transform.parent);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
