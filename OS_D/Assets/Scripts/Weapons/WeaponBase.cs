using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Player owner = null;
    [Header("Weapon stats")]
    public WeaponType weaponType = WeaponType.RANGE;
    public float attackCooldownTime = 1f;
    public float attackAnimationTime = 0.5f;
    private bool canAttack = true;
    public GameObject shootMark;
    protected Vector2 direction;
    //Modifiers

    protected virtual void Start()
    {
        
    }
    
    void Update()
    {
        if (owner != null)
        {
            transform.position = owner.transform.position;
            RotateWeapon();
            if (canAttack && Input.GetMouseButton(0))
            {
                Attack();
                if (weaponType == WeaponType.MELEE)
                {
                    StartCoroutine(StartAttackAnimation());
                }
                StartCoroutine(StartAttackCooldown());
            }
        }
    }

    protected virtual void Attack()
    {

    }

    private IEnumerator StartAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }

    private IEnumerator StartAttackAnimation()
    {
        owner.moveSpeed = 0;
        yield return new WaitForSeconds(attackAnimationTime);
        owner.moveSpeed = 7f; //исправить
    }

    private void RotateWeapon()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnWeaponEquip(Player player)
    {
        owner = player;
        //onwer+modifiers

    }

    public void OnWeaponRemove()
    {
        //owner-modifiers
        owner = null;
    }
}
