using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectile_lifetime;

    public bool isEnemyProjectile = false;

    public GameObject player;

    private Vector2 last_pos;
    private Vector2 current_pos;
    private Vector2 player_pos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());

        //if(!isEnemyProjectile)
        //{
        //    // 
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyProjectile)
        {
            // current position
            current_pos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player_pos, 1f * Time.deltaTime);

            if(current_pos == last_pos)
            {
                Destroy(gameObject);
            }

            // last position
            last_pos = current_pos;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy" && !isEnemyProjectile)
        {
            col.gameObject.GetComponent<EnemyController>().Die();
        }

        if (col.tag == "Player" && isEnemyProjectile)
        {
            //
            Destroy(gameObject);
        }
    }

    public void GetPlayer(Transform player)
    {
        player_pos = player.position;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(projectile_lifetime);
        Destroy(gameObject);
    }
}
