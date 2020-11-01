using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerController : MonoBehaviour
{
    public GameObject weaponPrefab;

    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    public Item itemToEquip;

    // Start is called before the first frame update
    void Start()
    {
        
        EquippedWeapon = Instantiate(weaponPrefab, playerHand.transform);
        EquippedWeapon.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    // Update is called once per frame
    void PerformWeaponAttack()
    {
        EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
    }
}
