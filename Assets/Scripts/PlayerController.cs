using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private int jumpLimit = 2;
    private int jumpsMade = 0;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private Animator playerAnim;
    private Rigidbody playerRb;
    private bool isGrounded = true;
    public bool gameOver = false;
    public float jumpForce = 10.0f;
    public float gravityModifier = 0.0f;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded && !gameOver)
        {
            Debug.Log("Dashing...");
            playerAnim.speed = 2;
        }
        else
        {
            Debug.Log("Walking...");
            playerAnim.speed = 1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && (jumpsMade<jumpLimit))
        {
            PlayerJump();
            ++jumpsMade;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpsMade = 0;
            PlayerJump();
            ++jumpsMade;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            gameOver = true;
            Debug.Log("Game over!");
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 0.5f);
        }
    }

    void PlayerJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 0.5f);
    }
}
