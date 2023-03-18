using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float leftBound = -15;
    private float speed = 25.0f;
    private float sprintSpeedMultiplier = 20f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Dash Mode!");
            transform.Translate(Vector3.left * Time.deltaTime * speed * sprintSpeedMultiplier);
        }
        if (playerControllerScript.gameOver == false )
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
