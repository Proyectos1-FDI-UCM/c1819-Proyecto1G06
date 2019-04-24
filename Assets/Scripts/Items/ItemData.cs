using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ItemData : MonoBehaviour {

    public char tier;
    public char Tier { get { return char.ToUpper(tier); } }
    public Sprite sprite { get { return GetComponent<SpriteRenderer>().sprite; } }
    public IItem[] effects { get { return GetComponents<IItem>(); } }
    public ObjectType type;
    public string itemName, itemFlavor;
    public AudioClip pickClip;

    Interactable interactable;
    AudioSource audioSource;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GetComponent<AudioSource>().PlayDelayed(0.3f);
    }

    /// <summary>
    /// Al chocarse con el jugador, recoger el item
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (interactable.interactable)
        {
            if (other.gameObject == GameManager.instance.player)
            {
                transform.parent = ItemManager.instance.transform;
                transform.GetComponent<BoxCollider2D>().enabled = false;    //Desactiva los componentes innecesarios.
                transform.GetComponent<SpriteRenderer>().enabled = false;
                ItemManager.instance.AddItem(this);
                GameManager.instance.ui.DisplayItemText(itemName, itemFlavor);
                for (int i = 0; i < effects.Length; i++)
                    effects[i].PickEffect();
                audioSource.PlayOneShot(pickClip);
            }
        }
    }
}
