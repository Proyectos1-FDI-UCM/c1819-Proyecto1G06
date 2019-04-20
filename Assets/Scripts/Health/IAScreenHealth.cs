using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAScreenHealth : EnemyHealth {

    bool vulnerable = false;
    public bool Vulnerable { get { return vulnerable; } set { vulnerable = value; anim.SetBool("Vulnerable", value); print(vulnerable); } }

    public override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(float amount)
    {
        if (Vulnerable)
        {
            base.TakeDamage(amount);
            GetComponentInParent<IAController>().TakeDamage();
        }
    }

    public override void Die()
    {
        ItemManager.instance.DeleteItems();
        GameManager.instance.LoadScene("Menu");
    }
}
