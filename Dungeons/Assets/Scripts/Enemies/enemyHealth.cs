using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int max_health = 2;
    public int current_health;

    public bool is_hit = false;

    public ScoreScript score_ref;
    //public UIController score_ref;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        current_health -= damage;

        if(current_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        score_ref = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreScript>();
        score_ref.score += 5;
        Debug.Log(name + " died!");
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
