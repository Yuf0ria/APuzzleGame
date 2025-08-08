using UnityEngine;
using System.Collections;

public class SpikesHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player hit spikes");
        if (other.CompareTag("Player"))
        {
            Animator playerAnimator = other.GetComponent<Animator>();
            StartCoroutine(StunPlayer(playerAnimator));
            
        }
    }

    IEnumerator StunPlayer(Animator animator)
    {
        PlayerMovement.isStunned = true;
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("isHit", false);
        PlayerMovement.isStunned = false;

        GameObject playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject != null)
        {
            PlayerControls player = playerGameObject.GetComponent<PlayerControls>();

            if (player != null)
            {
                int lastActivatedIndex = -1;
                for (int i = player.activateStar.Length - 1; i >= 0; i--)
                {
                    if (player.activateStar[i].isActivated)
                    {
                        lastActivatedIndex = i;
                        break;
                    }
                }

                if (lastActivatedIndex != -1)
                {
                    Vector3 lastStarPos = player.activateStar[lastActivatedIndex].transform.position;
                    playerGameObject.transform.position = lastStarPos;
                }
                else
                {
                    Debug.Log("No activated star found to respawn the player.");
                }
            }
        }
    }
}
