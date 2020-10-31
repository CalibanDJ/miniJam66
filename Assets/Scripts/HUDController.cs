using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{


    public Color selected;
    public Color unselected;

    PlayerController player;
    GameObject key1L;
    GameObject key1R;
    GameObject key2L;
    GameObject key2R;

    Image b1L;
    Image b1R;
    Image b2R;
    Image b2L;

    int min = 1;
    bool sized = false;


    //battery variables
    public int max_hp = 5;
    public uint last_hp;
    public float battery_margin = 0.9f;
    GameObject battery;
    float battery_height;
    float battery_width;
    GameObject[] hp = new GameObject[100];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        last_hp = player.actual_hp;

        key1L = GameObject.Find("Text-Key1-L");
        key1R = GameObject.Find("Text-Key1-R");
        key2L = GameObject.Find("Text-Key2-L");
        key2R = GameObject.Find("Text-Key2-R");
        key1R.GetComponent<UnityEngine.UI.Text>().text = player.key_primary[0].ToString();
        key1L.GetComponent<UnityEngine.UI.Text>().text = player.key_primary[1].ToString();
        key2R.GetComponent<UnityEngine.UI.Text>().text = player.key_secondary[0].ToString();
        key2L.GetComponent<UnityEngine.UI.Text>().text = player.key_secondary[1].ToString();

        b1L = GameObject.Find("Key1-L").GetComponent<Image>();
        b1R = GameObject.Find("Key1-R").GetComponent<Image>();
        b2L = GameObject.Find("Key2-L").GetComponent<Image>();
        b2R = GameObject.Find("Key2-R").GetComponent<Image>();

        b1L.color = unselected;
        b1R.color = unselected;
        b2L.color = unselected;
        b2R.color = unselected;

        battery = GameObject.Find("Battery");
        battery_height = battery.GetComponent<RectTransform>().rect.height * battery_margin;
        battery_width = battery.GetComponent<RectTransform>().rect.width * battery_margin;
        float w_shift = (battery.GetComponent<RectTransform>().rect.width * (1 - battery_margin) / 2);
        float h_shift = (battery.GetComponent<RectTransform>().rect.height * (1 - battery_margin) / max_hp);

        for (int i = 0; i < player.actual_hp; i++)
        {
            GameObject NewObj = new GameObject("life "+(i+1));
            hp[i] = NewObj;
            Image NewImage = NewObj.AddComponent<Image>();
            NewImage.color = GetColorBattery();
            NewObj.GetComponent<RectTransform>().SetParent(battery.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            NewObj.GetComponent<RectTransform>().sizeDelta = new Vector2(battery_width, battery_height/max_hp);
            NewObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            NewObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            NewObj.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
            NewObj.GetComponent<RectTransform>().position = new Vector3(battery.transform.position.x + w_shift,
                battery.transform.position.y - (battery_height / max_hp + h_shift) * (max_hp - i - 1) - h_shift,
                battery.transform.position.z);
            NewObj.SetActive(true);
        }
    }

    Color GetColorBattery()
    {
        return player.actual_hp >= (2 *(float)max_hp /3)? new Color32(0, 255, 0, 255) :
            player.actual_hp >= ((float)max_hp / 3) ? new Color32(255, 204, 0, 255) :
            new Color32(255, 0, 0, 255);
    }

    void MajColor()
    {
        for (int i = 0; i < player.actual_hp; i++)
        {
            hp[i].GetComponent<Image>().color = GetColorBattery();
        }
    }

    void SetSized()
    {
        if (!sized)
        {
            min = Mathf.Min(key1R.GetComponent<UnityEngine.UI.Text>().cachedTextGenerator.fontSizeUsedForBestFit,
                key1L.GetComponent<UnityEngine.UI.Text>().cachedTextGenerator.fontSizeUsedForBestFit,
                key2R.GetComponent<UnityEngine.UI.Text>().cachedTextGenerator.fontSizeUsedForBestFit,
                key2L.GetComponent<UnityEngine.UI.Text>().cachedTextGenerator.fontSizeUsedForBestFit);
            
            if (min > 0)
            {
                key1R.GetComponent<UnityEngine.UI.Text>().resizeTextMaxSize = min;
                key1L.GetComponent<UnityEngine.UI.Text>().resizeTextMaxSize = min;
                key2R.GetComponent<UnityEngine.UI.Text>().resizeTextMaxSize = min;
                key2L.GetComponent<UnityEngine.UI.Text>().resizeTextMaxSize = min;
                sized = true;
            }
           
        }
    }

    void MajLife()
    {
        if(player.actual_hp != last_hp)
        {
            for(uint i = player.actual_hp; i < last_hp; i++)
            {
                Destroy(hp[i]);
            }
            last_hp = player.actual_hp;
        }
    }

    void FixedUpdate()
    {
        SetSized();
        MajLife();
        MajColor();
        if (player.lastAct1 == 0)
        {
            b1R.color = selected;
            b1L.color = unselected;
        } else if (player.lastAct1 == 1)
        {
            b1R.color = unselected;
            b1L.color = selected;
        }

        if (player.lastAct2 == 0)
        {
            b2R.color = selected;
            b2L.color = unselected;
        }
        else if (player.lastAct2 == 1)
        {
            b2R.color = unselected;
            b2L.color = selected;
        }
    }


 
    
}
