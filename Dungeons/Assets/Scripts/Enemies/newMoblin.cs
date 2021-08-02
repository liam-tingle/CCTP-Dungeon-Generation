using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMoblin : MonoBehaviour
{ 
    public float follow_radius;
    public float attack_radius;
    public float move_speed = 1;

    public bool should_rotate;
    public bool knocked = false;

    public LayerMask determine_player;
    public Transform target;

    Rigidbody2D m_rb;
    public Animator anim;
    Vector2 movement;
    public Vector3 direction;
    public DamagePlayer damage_ref;

    bool Following;
    public bool Attacking;

    bool can_attack = true;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("IsMoving", Following);
        anim.SetBool("IsAttacking", Attacking);

        if (Vector3.Distance(target.position, transform.position) <= follow_radius && knocked == false)
        {
            Following = true;
            Attacking = false;

            if (Vector3.Distance(target.position, transform.position) <= attack_radius && can_attack == true && knocked == false)
            {
                Following = false;
                Attacking = true;
            }
        }
        else
        {
            Following = false;
            Attacking = false;
        }

        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;

        if (Following && !Attacking && knocked == false)
        { 
            MoveEnemy(movement);
        }
        else if (Attacking && can_attack && knocked == false)
        {
            anim.SetBool("IsMoving", false);
            StartCoroutine(Attack());
            m_rb.velocity = Vector2.zero;
        }

        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        anim.SetFloat("LastMoveX", direction.x);
        anim.SetFloat("LastMoveY", direction.y);
    }

    void MoveEnemy(Vector2 direction)
    {
        m_rb.MovePosition((Vector2)transform.position + (direction * move_speed * Time.deltaTime));
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("IsAttacking", true);
        StartCoroutine(damage_ref.DoDamage());
        can_attack = false;
        yield return new WaitForSeconds(2f);
        anim.SetBool("IsAttacking", false);
        can_attack = true;
    }
}
