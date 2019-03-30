using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour {

    /// <summary>
    /// Lista de todos los items con sus propiedades. 
    /// </summary>
    struct ItemList
    {
        public ItemData[] itemData;
        public int firstSpace;     // Primer hueco libre de la lista, o su tamaño

        public ItemList(int tam)
        {
            itemData = new ItemData[tam];
            firstSpace = 0;
        }
    }

    public static ItemManager instance;

    ItemList itemList = new ItemList(15);
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
        }
        else Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Añade item a la lista de items después de asegurarse de que cabe
    /// </summary>
    public void AddItem(ItemData item)
    {
        if (itemList.firstSpace == itemList.itemData.Length) AddSpace(ref itemList, 15);

        itemList.itemData[itemList.firstSpace] = item;
        itemList.firstSpace++;
        GameManager.instance.ui.AddItem(item.sprite);
    }

    /// <summary>
    /// ¡YAY, sé usar eventos! Al cargar una escena, aplica los efectos de los objetos
    /// </summary>
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Minimap.instance.InitializeMap();
        ApplyItemEffects();
        GameManager.instance.ui.UpdateItems(GetItemSprites(itemList));
    }

    /// <summary>
    /// Añade amount de espacio a itemList
    /// </summary>
    void AddSpace(ref ItemList list, int amount)
    {
        ItemData[] newList = new ItemData[list.itemData.Length + amount];

        for (int i = 0; i < list.firstSpace; i++)
        {
            newList[i] = list.itemData[i];
        }

        list.itemData = newList;
    }

    /// <summary>
    /// Devuelve el array de sprites de los items que tiene el jugador
    /// </summary>
    /// <param name="list">La lista de items</param>
    Sprite[] GetItemSprites (ItemList list)
    {
        Sprite[] sprites = new Sprite[list.firstSpace];

        for (int i = 0; i < list.firstSpace; i++)
        {
            sprites[i] = list.itemData[i].sprite;
        }

        return sprites;
    }

    /// <summary>
    /// Aplica el efecto de todos los items que tiene el jugador, utilizar únicamente cuando se cambia de escena y se tienen que reaplicar los efectos al jugador
    /// </summary>
    public void ApplyItemEffects()
    {
        for(int i = 0; i < itemList.firstSpace; i++)
        {
            IItem[] effects = itemList.itemData[i].effects;
            foreach(IItem effect in effects)
            {
                effect.PickEffect();
            }
        }
    }

    /// <summary>
    /// Elimina los objetos cogidos
    /// </summary>
    public void DeleteItems()
    {
        for(int i = 0; i < itemList.firstSpace; i++)
        {
            Destroy(itemList.itemData[i].gameObject);
        }

        itemList.itemData = new ItemData[15];
    }

    /// <summary>
    /// Avisa al jugador que aumente su vida
    /// </summary>
    public void AddHealth(int amount, bool heal)
    {
        player.GetComponent<PlayerHealth>().AddMaxHealth(amount, heal);
    }

    /// <summary>
    /// Avisa al jugador que aumente su velocidad
    /// </summary>
    public void AddSpeed(float amount)
    {
        player.GetComponent<PlayerMovement>().AddSpeed(amount);
    }

    /// <summary>
    /// Avisa al jugador que aumente su daño
    /// </summary>
    public void AddDamage(float amount)
    {
        player.GetComponentInChildren<PlayerShooting>().AddDamage(amount);
    }
}
