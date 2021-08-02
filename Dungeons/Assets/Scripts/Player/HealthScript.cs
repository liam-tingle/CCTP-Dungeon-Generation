using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public int health;
    public int num_hearts;

    public Image[] hearts;

    public Sprite full_hearts;
    public Sprite empty_hearts;

    public GameObject player;
    public GameObject game_over;
    public Animator anim;

    void Update()
    {
        if(health > num_hearts)
        {
            health = num_hearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = full_hearts;
            }
            else
            {
                hearts[i].sprite = empty_hearts;
            }

            if(i < num_hearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " died!");
        StartCoroutine(GameOver());
        //Destroy(gameObject);
    }

    IEnumerator GameOver()
    {
        player.transform.position = new Vector3(0, 0, 100);
        game_over.SetActive(true);
        anim.SetBool("GameOver", true);
        yield return new WaitForSeconds(4f);
        game_over.SetActive(false);
        anim.SetBool("GameOver", false);
        health = 5;
        SceneManager.LoadScene(0);
    }
}
