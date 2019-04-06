using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSummonCompanion : MonoBehaviour, IItem
{
    public GameObject companion;

    public void PickEffect()
    {
        Transform.Instantiate(companion, GameManager.instance.player.transform.position, Quaternion.identity);
    }
}
