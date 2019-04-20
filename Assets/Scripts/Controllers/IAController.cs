using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour {

    public CelulaHealth[] celulas;  // top, top, bot, bot
    public IAScreenHealth screen;

    IBossAttack1[] attacks;
    float combinedMaxHealth = 0, combinedCurHealth = 0;
    int numCelulas, curAttack = 0;
    float attackTimer = 0;

	enum BossPhase { First, Second, Third, Fourth } //Cuatro células, dos células, transición, final
    BossPhase phase = BossPhase.First;

    private void Start()
    {
        attacks = GetComponentsInChildren<IBossAttack1>();
        numCelulas = celulas.Length;
        SetVulnerable(celulas, screen);
        combinedMaxHealth = celulas[2].maxHealth + celulas[3].maxHealth;
        for(int i = 0; i < attacks.Length; i++)
        {
            attacks[i].ToggleAttack(false);
        }
    }

    void Update()
    {
        if (attackTimer <= 0)
        {
            DeactivateAttack(curAttack);
            int num = Random.Range(0, attacks.Length);
            while (num == curAttack) num = Random.Range(0, attacks.Length);
            curAttack = num;
            ChangeAttack(attacks, curAttack);
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
            combinedMaxHealth = celulas[0].maxHealth + celulas[1].maxHealth;
            SetVulnerable(celulas, screen);
        } else if(numCelulas == 0)
        {
            phase = BossPhase.Third;
            combinedMaxHealth = screen.maxHealth;
            SetVulnerable(celulas, screen);
        }
    }

    void DeactivateAttack(int index)
    {
        attacks[index].ToggleAttack(false);
    }

    void ChangeAttack(IBossAttack1[] attacks, int index)
    {
        attacks[index].ToggleAttack(true);
        attackTimer = attacks[index].AttackTime;
    }

    public void TakeDamage()
    {
        switch (phase)
        {
            case BossPhase.First:
                combinedCurHealth = celulas[2].CurHealth + celulas[3].CurHealth;
                break;
            case BossPhase.Second:
                combinedCurHealth = celulas[0].CurHealth + celulas[1].CurHealth;
                break;
            case BossPhase.Third:
                combinedCurHealth = screen.CurHealth;
                break;
        }
        GameManager.instance.ui.UpdateBossHealth(combinedCurHealth, combinedMaxHealth);
    }
}
