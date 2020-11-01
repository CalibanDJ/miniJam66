using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerController : MonoBehaviour
{

    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    public Item itemToEquip;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Fork"),
            playerHand.transform.position, playerHand.transform.rotation);
        animator = EquippedWeapon.GetComponent<Animator>();
        EquippedWeapon.transform.position = new Vector3(EquippedWeapon.transform.position.x,
            EquippedWeapon.transform.position.y - EquippedWeapon.transform.localScale.y,
            EquippedWeapon.transform.position.z );
        EquippedWeapon.transform.SetParent(playerHand.transform);
    }

    // Update is called once per frame
    public void PerformWeaponAttack(bool left)
    {
        animator.SetBool("left", left);
        animator.SetTrigger("attack");
        EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
    }
}
