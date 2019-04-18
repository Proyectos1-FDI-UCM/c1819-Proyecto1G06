using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPosAdd : MonoBehaviour {

    private void Awake()
    {
        RandomItemPlacer.instance.AddPosition(transform);
    }
}
