using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossAttack1
{
    float AttackTime { get; }

    void ToggleAttack(bool active);
}
