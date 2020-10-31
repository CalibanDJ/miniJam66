using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerController : MonoBehaviour
{

    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    public Item itemToEquip;

    // Start is called before the first frame update
    void Start()
    {
        
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Fork"), 
            playerHand.transform.position, playerHand.transform.rotation);
        EquippedWeapon.transform.position = new Vector3(EquippedWeapon.transform.position.x,
            EquippedWeapon.transform.position.y - EquippedWeapon.transform.localScale.y,
            EquippedWeapon.transform.position.z );
        EquippedWeapon.transform.SetParent(playerHand.transform);
    }

    // Update is called once per frame
    void PerformWeaponAttack()
    {
        EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
    }
}
