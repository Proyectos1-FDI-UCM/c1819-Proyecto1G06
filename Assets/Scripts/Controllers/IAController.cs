using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour {

    public CelulaHealth[] celulas;
    public IAScreenHealth screen;

    IBossAttack1[] attacks;
    int numCelulas, curAttack = 0;
    float attackTimer = 0;

	enum BossPhase { First, Second, Third, Fourth } //Cuatro células, dos células, transición, final
    BossPhase phase = BossPhase.First;

    private void Start()
    {
        attacks = GetComponentsInChildren<IBossAttack1>();
        numCelulas = celulas.Length;
        SetVulnerable(celulas, screen);
        for(int i = 0; i < attacks.Length; i++)
        {
            attacks[i].ToggleAttack(false);
        }
    }

    void Update()
    {
        if (attackTimer <= 0)
        {
            ChangeAttack(attacks, ref curAttack);
        }
        else attackTimer -= Time.deltaTime;
    }

    void SetVulnerable(CelulaHealth[] celulas, IAScreenHealth screen)
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
                screen.Vulnerable = true;
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
            SetVulnerable(celulas, screen);
        } else if(numCelulas == 0)
        {
            phase = BossPhase.Third;
            SetVulnerable(celulas, screen);
        }
    }

    void DeactivateAttack(int index)
    {
        attacks[index].ToggleAttack(false);
    }

    void ChangeAttack(IBossAttack1[] attacks, ref int index)
    {
        DeactivateAttack((index + attacks.Length - 1) % attacks.Length);
        attacks[index].ToggleAttack(true);
        attackTimer = attacks[index].AttackTime;
        index++;
        index %= attacks.Length;
    }
}
