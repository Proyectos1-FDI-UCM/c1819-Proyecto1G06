using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    bool _interactable = false;
    public bool interactable { get { return _interactable; } }
    public float interactableTime = 0.3f;

    float time = 0;

    private void Awake()
    {
        time = interactableTime;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
            _interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
            _interactable = true;
    }
}
