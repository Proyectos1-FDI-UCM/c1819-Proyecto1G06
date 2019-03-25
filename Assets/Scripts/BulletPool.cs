using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

	void Awake ()
    {
        GameManager.instance.bulletPool = transform;	
	}
}
