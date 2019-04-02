using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour {

    public CelulaHealth[] celulas;
    int numCelulas;
    //IAScreen

	enum BossPhase { First, Second, Third, Fourth } //Cuatro células, dos células, transición, final
    BossPhase phase = BossPhase.First;

    private void Start()
    {
        numCelulas = celulas.Length;
        SetVulnerable(celulas);
    }

    void SetVulnerable(CelulaHealth[] celulas/*, IAScreen*/)
    {
        switch (phase)
        {
            case BossPhase.First:
                for (int i = celulas.Length / 2; i < celulas.Length; i++) celulas[i].Vulnerable = true;
                break;
            case BossPhase.Second:
                for (int i = 0; i < celulas.Length / 2; i++) celulas[i].Vulnerable = true;
                break;
            case BossPhase.Third:
                //IAScreen
                break;
        }
    }

    /// <summary>
    /// Si su array de células está vacío.
    /// </summary>
    public void CelulaDestroyed()
    {
        numCelulas--;
        if(numCelulas == celulas.Length / 2)
        {
            phase = BossPhase.Second;
            SetVulnerable(celulas);
        } else if(numCelulas == 0)
        {
            phase = BossPhase.Third;
            SetVulnerable(celulas);
        }
        print(phase);
    }
}
