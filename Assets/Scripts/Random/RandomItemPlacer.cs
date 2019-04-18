using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomItemPlacer : MonoBehaviour {

    public int itemsPerLevel;
    public GameObject[] items;
    public Transform[] itemPositions;

    public static RandomItemPlacer instance;

    GameObject[] usedItems;
    int posPlaceIndex = 0;
    int usedIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            usedItems = new GameObject[2 * itemsPerLevel];
            itemPositions = new Transform[itemsPerLevel];
            SceneManager.sceneLoaded += PlaceItemsRandom;
        }
        else Destroy(gameObject);
    }


    public void PlaceItemsRandom(Scene scene, LoadSceneMode mode)
    {
        if (!CheckPosNull())
        {
            int posIndex = 0;

            for (int i = 0; i < itemsPerLevel; i++)
            {
                int rnd;

                do rnd = Random.Range(0, items.Length - 1);
                while (CheckIfUsed(items[rnd]));

                Instantiate(items[rnd], itemPositions[posIndex]);
                posIndex++;

                usedItems[usedIndex] = items[rnd];
                usedIndex++;
            }
        }
    }

    bool CheckIfUsed(GameObject item)
    {
        bool check = false;
        int i = 0;

        while(i < usedItems.Length && !check)
        {
            if (item == usedItems[i]) check = true;
            i++;
        }

        return check;
    }

    public void AddPosition(Transform pos)
    {
        itemPositions[posPlaceIndex] = pos;

        posPlaceIndex++;
        if (posPlaceIndex == itemsPerLevel) posPlaceIndex = 0;
    }

    public bool CheckPosNull()
    {
        bool check = false;
        int i = 0;

        while (i < itemPositions.Length && !check)
        {
            if (itemPositions[i] == null) check = true;
            i++;
        }

        return check;
    }
}
