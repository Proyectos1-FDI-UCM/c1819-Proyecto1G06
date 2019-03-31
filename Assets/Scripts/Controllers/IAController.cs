using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour {

    public CelulaIAController[] celulas;
    int numCelulas;

	enum BossState { FirstPhase, SecondPhase }
    BossState phase = BossState.FirstPhase;

    private void Start()
    {
        int num = 0;
        while (num < celulas.Length && celulas[num] != null)
        {
            num++;
        }

        numCelulas = num;
    }

    /// <summary>
    /// Si su array de células está vacío.
    /// </summary>
    public void CelulaDestroyed()
    {
        numCelulas--;
        if (numCelulas == 0)
        {
            phase++;
        }
    }
}
