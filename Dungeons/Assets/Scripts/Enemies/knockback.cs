using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public float hitback;
    public float fly_duration;

    public GameObject mob;
    public newMoblin mob_ref;

    public Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        mob_ref = mob.GetComponent<newMoblin>();
        enemy = mob.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if(other.gameObject.CompareTag("Enemy"))
        {
            
                mob_ref.knocked = true;
                Vector2 difference = mob.transform.position - transform.position;
                difference = difference.normalized * hitback;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                Debug.Log("should knockback");
                StartCoroutine(Knock(enemy));
            
        }
    }

    IEnumerator Knock(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(fly_duration);
        enemy.velocity = Vector2.zero;
        mob_ref.knocked = false;
    }
}
