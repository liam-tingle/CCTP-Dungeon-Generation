using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attack_point;
    public LayerMask enemy_layers;
    public AudioSource oof;
    public enemyHealth health_ref;
    public PlayerController player_ref;

    public int attack_damage = 1;
    public float attack_range = 0.25f;

    //Update is called once per frame
    void FixedUpdate()
    {
        if (player_ref.attacking)
        {
            StartCoroutine(DamageEnemy());
        }
    }

    public IEnumerator DamageEnemy()
    {
        Collider2D[] hit_enemies = Physics2D.OverlapCircleAll(attack_point.position, attack_range, enemy_layers);

        foreach (Collider2D enemy in hit_enemies)
        {
            oof.Play();
            enemy.GetComponent<enemyHealth>().TakeDamage(attack_damage);
            Debug.Log("We hit " + enemy.name);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attack_point == null)
            return;

        Gizmos.DrawWireSphere(attack_point.position, attack_range);
    }
}
