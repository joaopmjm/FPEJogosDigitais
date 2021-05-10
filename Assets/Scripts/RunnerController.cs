using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerController : MonoBehaviour
{
     [SerializeField]
     private Transform player;
     [SerializeField]
     private UnityEngine.AI.NavMeshAgent agent;
     [SerializeField]
     Renderer renderer;
     float speed = 1f;
 
     private void Start()
     {
         player = GameObject.FindGameObjectWithTag("Player").transform;
         if (agent == null) agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
         agent.speed = speed;
     }
    public void IncreaseSpeed(float newSpeed)
    {
        agent.speed += newSpeed;
    }
     private void EndTheGame()
     {
         Cursor.visible = true;
         SceneManager.LoadScene("Endgame");
     }
     private void Update()
     {
         if(renderer.isVisible | GameData.Paused)
         {
             Debug.Log("Is Being Seen or Paused");
             agent.enabled = false;
         }
         else
         {
             Debug.Log("Is Not Being Seen");
             agent.enabled = true;
             RunAwayFromPlayer();
         }
         
     } 
     void RunAwayFromPlayer()
     {
         if(Vector3.Distance(player.position, gameObject.transform.position) > 2)
         {
            agent.SetDestination(player.position);
         }
         else
         {
             EndTheGame();
         }
     }
}
