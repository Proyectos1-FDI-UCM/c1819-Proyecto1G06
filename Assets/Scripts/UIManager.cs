using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] curLives = new Image[10];
    public Image[] maxLives = new Image[10];
    public RawImage mapRaw;
    public Text damage, speed, multiplier, dmgadd, spdadd;
    public Text itemName, itemDesc;
    public Image bossHealthBack, bossHealth;
    public RectTransform itemListHolder;
    public float displayTime;

    int lastActiveMaxLife = 0;
    int lastActiveCurLife = 0;
    int lastItemListSlot = 0;

    /// <summary>
    /// Busca la última vida activa
    /// </summary>
    private void Awake()
    {
        lastActiveMaxLife = maxLives.Length - 1;
        lastActiveCurLife = curLives.Length - 1;
        GameManager.instance.ui = this;
        ToggleBossHealth(false);
        foreach(Transform child in itemListHolder)
        {
            child.gameObject.SetActive(false);
        }
        DisableItemText();
    }

    /// <summary>
    /// Actualiza la vida de la UI para que refleje la vida actual y máxima del jugador
    /// </summary>
    public void UpdateLives(int curHealth, int maxHealth)
    {
        if(maxHealth - 1 < lastActiveMaxLife)
        {
            for(int i = lastActiveMaxLife; i > maxHealth - 1; i--)
            {
                maxLives[i].enabled = false;
            }

        } else
        {
            for(int i = maxHealth - 1; i > lastActiveMaxLife; i--)
            {
                maxLives[i].enabled = true;
            }
        }

        lastActiveMaxLife = maxHealth - 1;

        if(curHealth - 1 < lastActiveCurLife)
        {
            for(int i = lastActiveCurLife; i > curHealth - 1; i--)
            {
                curLives[i].enabled = false;
            }
        } else
        {
            for(int i = curHealth - 1; i > lastActiveCurLife; i--)
            {
                curLives[i].enabled = true;
            }
        }

        lastActiveCurLife = curHealth - 1;
    }


    /// <summary>
    /// Actualiza el mapa según map
    /// </summary>
    public void UpdateMap(RoomMapInfo[,] map)
    {
        Texture2D texture = new Texture2D(map.GetLength(1), map.GetLength(0));
        texture.filterMode = FilterMode.Point; 
        for(int fil = 0; fil < map.GetLength(0); fil++)
        {
            for(int col = 0; col < map.GetLength(1); col++)
            {
                
                switch(map[fil, col].vision)
                {
                    case RoomMap.Current:
                        texture.SetPixel(col, fil, new Color32(165, 165, 78, 255));
                        break;
                    case RoomMap.Visited:
                        texture.SetPixel(col, fil, new Color32(127, 127, 127, 255));
                        break;
                    case RoomMap.NonVisited:
                        texture.SetPixel(col, fil, new Color32(76, 76, 76, 255));
                        break;
                    case RoomMap.Nonexistant:
                    case RoomMap.NotSeen:
                        texture.SetPixel(col, fil, new Color32(255, 255, 255, 0));
                        break;
                }               
            }
        }
        texture.Apply();
        mapRaw.texture = texture;
    }

    /// <summary>
    /// Actualiza el texto de daño
    /// </summary>
    public void UpdateDamage(float damage, float multiplier)
    {
        this.damage.text = damage.ToString("F2");
        this.multiplier.text = "x" + multiplier.ToString("F2");
    }

    /// <summary>
    /// Actualiza el texto de la velocidad
    /// </summary>
    public void UpdateSpeed(float speed)
    {
        this.speed.text = (speed * (3f/7f)).ToString("F2");
    }

    /// <summary>
    /// Activa o desactiva la barra de vida del jefe y la llena
    /// </summary>
    /// <param name="active">Controla la activación</param>
    public void ToggleBossHealth(bool active)
    {
        if (!active)
        {        
          bossHealth.fillAmount = 1f;
        }
        bossHealth.gameObject.SetActive(active);
        bossHealthBack.gameObject.SetActive(active);       
    }

    /// <summary>
    /// Actualiza el porcentaje de la barra de vida
    /// </summary>
    public void UpdateBossHealth(float health, float maxHealth)
    {
        bossHealth.fillAmount = health / maxHealth;
    }

    /// <summary>
    /// Añade un sprite de un objeto a una lista cuando se coge.
    /// </summary>
    /// <param name="sprite">El sprite añadido</param>
    public void AddItem(Sprite sprite)
    {
        Transform child = itemListHolder.GetChild(lastItemListSlot);
        child.gameObject.SetActive(true);
        child.GetComponent<Image>().sprite = sprite;
        lastItemListSlot++;
    }

    /// <summary>
    /// Coloca en la lista de sprites los sprites dados
    /// </summary>
    public void UpdateItems(Sprite[] sprites)
    {
        int i = 0;
        while(i < sprites.Length)
        {
            if (sprites[i] != null)
            {
                Transform child = itemListHolder.GetChild(lastItemListSlot);
                child.gameObject.SetActive(true);
                child.GetComponent<Image>().sprite = sprites[i];
            }
            i++;
            lastItemListSlot = i;
        }
    }

    public void DisplayItemText(string name, string desc)
    {
        itemName.text = name;
        itemDesc.text = desc;

        CancelInvoke("DisableItemText");
        Invoke("DisableItemText", displayTime);
    }

    private void DisableItemText()
    {
        itemName.text = "";
        itemDesc.text = "";
    }

    public void TextAddDmg(float dmg)
    {
        float num = dmg;
        if (dmgadd.text != "") num += float.Parse(dmgadd.text);

        if (Mathf.Abs(num) > 0.009)
        {
            if (num >= 0)
            {
                dmgadd.color = Color.green;
                dmgadd.text = "+" + num.ToString("F2");
            }
            else
            {
                dmgadd.color = Color.red;
                dmgadd.text = num.ToString("F2");
            }

            CancelInvoke("DisableAddDmg");
            Invoke("DisableAddDmg", displayTime);
        }
    }

    private void DisableAddDmg()
    {
        dmgadd.text = "";
    }

    public void TextAddSpeed(float speed)
    {
        float num = speed * 3 / 7;
        if (spdadd.text != "") num += float.Parse(spdadd.text);

        if(Mathf.Abs(num) > 0.009)
        {
            if (num >= 0)
            {
                spdadd.color = Color.green;
                spdadd.text = "+" + num.ToString("F2");
            }
            else
            {
                spdadd.color = Color.red;
                spdadd.text = num.ToString("F2");
            }

            CancelInvoke("DisableAddSpeed");
            Invoke("DisableAddSpeed", displayTime);
        }
    }

    private void DisableAddSpeed()
    {
        spdadd.text = "";
    }


}
