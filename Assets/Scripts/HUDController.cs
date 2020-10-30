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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
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

    void FixedUpdate()
    {
        SetSized();

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
