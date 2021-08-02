using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public newMoblin mob_ref;
    public Transform attack_point;
    public LayerMask player_layer;
    public AudioSource oof;
    public HealthScript health_ref;

    public int attack_damage = 1;

    public float attack_range = 0.25f;

    public IEnumerator DoDamage()
    {
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(attack_point.position, attack_range, player_layer);


        foreach (Collider2D player in hit_player)
        {
            if (mob_ref.Attacking)
            {
                oof = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
                oof.Play();

                health_ref = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
                health_ref.TakeDamage(attack_damage);

                yield return new WaitForSeconds(2f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attack_point == null)
            return;

        Gizmos.DrawWireSphere(attack_point.position, attack_range);
    }
}
