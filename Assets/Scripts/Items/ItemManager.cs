using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    /// <summary>
    /// Lista de todos los items con sus propiedades. 
    /// </summary>
    struct ItemList
    {
        public List<ItemData> items;
        public BulletEffects effect;
        public Weapons weapon;
    }

    public static ItemManager instance;

    ItemList itemList;
    GameObject player { get { return GameManager.instance.player; } }

    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            itemList = new ItemList();
            itemList.items = new List<ItemData>();
            itemList.effect = BulletEffects.None;
            itemList.weapon = Weapons.Default;
            GameManager.instance.onEffectChanged += ChangeEffect;
            GameManager.instance.onWeaponChanged += ChangeWeapon;
        }
        else Destroy(gameObject);       
    }

    /// <summary>
    /// Añade item a la lista de items después de asegurarse de que cabe
    /// </summary>
    public void AddItem(ItemData item)
    {
        switch (item.type)
        {
            case (ObjectType.Item):
                GameManager.instance.ui.AddItem(item.sprite);
                break;
        }

        itemList.items.Add(item);
    }

    public void ChangeEffect(BulletEffects effect, ItemData item, Sprite sprite)
    {
        if (CurrentEfect() != BulletEffects.Illegal)
        {
            itemList.effect = effect;
        }
    }

    public void ChangeWeapon(Weapons weaponNew, ItemData item, Sprite weaponSprite)
    {
        itemList.weapon = weaponNew;
    }

    /// <summary>
    /// ¡YAY, sé usar eventos! Al cargar una escena, aplica los efectos de los objetos
    /// </summary>
    public void OnSceneLoaded()
    {
        ApplyItemEffects();
        GameManager.instance.ui.UpdateItems(GetItemSprites(itemList));       
    }

    /// <summary>
    /// Devuelve el array de sprites de los items que tiene el jugador
    /// </summary>
    /// <param name="list">La lista de items</param>
    Sprite[] GetItemSprites (ItemList list)
    {
        if (list.items == null) return new Sprite[0];

        Sprite[] sprites = new Sprite[list.items.Count];

        for (int i = 0; i < list.items.Count; i++)
        {
            if (list.items[i].type == ObjectType.Item)
                sprites[i] = list.items[i].sprite;
        }

        return sprites;
    }

    void ApplyItem(IItem[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].PickEffect();
        }
    }

    /// <summary>
    /// Aplica el efecto de todos los items que tiene el jugador, utilizar únicamente cuando se cambia de escena y se tienen que reaplicar los efectos al jugador
    /// </summary>
    public void ApplyItemEffects()
    {
        if (itemList.items == null) return;
        foreach(ItemData item in itemList.items)
        {            
            ApplyItem(item.effects);
        }
    }

    /// <summary>
    /// Elimina los objetos cogidos
    /// </summary>
    public void DeleteItems()
    {
        for(int i = 0; i < itemList.items.Count; i++)
        {
            Destroy(itemList.items[i].gameObject);
        }

        itemList = new ItemList();
        itemList.items = new List<ItemData>();
        itemList.effect = BulletEffects.None;
        itemList.weapon = Weapons.Default;
    }

    public BulletEffects CurrentEfect()
    {
        return itemList.effect;
    }

    /// <summary>
    /// Sobreescribe el efecto, en concreto para facilitar el Ilegal
    /// </summary>
    public void OverrideEffect(BulletEffects effect)
    {
        itemList.effect = effect;
    }
}
