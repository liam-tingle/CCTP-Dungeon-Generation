using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    public GameObject maze_gen;
    public GameObject player;
    public GameObject transition;
    public ScoreScript score_ref;

    public Animator anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            maze_gen = GameObject.FindGameObjectWithTag("DungeonGen");
            score_ref.level += 1;
            StartCoroutine(TransitionDungeon());
        }
    }

    IEnumerator TransitionDungeon()
    {
        transition.SetActive(true);
        anim.SetBool("Transition", true);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<HealthScript>().enabled = false;
        yield return new WaitForSeconds(2);
        transition.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<HealthScript>().enabled = true;
        anim.SetBool("Transition", false);
        maze_gen.GetComponent<MazeGeneration>().ClearDungeon();
        maze_gen.GetComponent<MazeGeneration>().RunDungeon();
    }
}
