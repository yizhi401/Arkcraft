﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScene : MonoBehaviour {

    public GameObject g;

	// Use this for initialization
	void Start () {
        //GameObject mainMenu = Instantiate(Resources.Load("PauseMenu")) as GameObject;
        //mainMenu.transform.SetParent(g.transform.parent);
        Debug.Log(System.Environment.Version);
        Debug.Log(g.transform.forward.ToString());
        Debug.Log(Math.Sign(-0.5));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
