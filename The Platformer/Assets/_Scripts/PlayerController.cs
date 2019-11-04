using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
/// <summary>
/// Source File: PlayerController.cs
/// Author: Geerthan Kanthasamy
/// This program allows the player to control the Player GameObject's movements (and jump)
/// also contains logic for when colliding with an object tagged as "Crystal"
/// </summary>
public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    public Transform activeSpawnpoint;
    public GameController gameController;

    [Header("Object Properties")]
    public Animator playerAnimator;
    public SpriteRenderer playerSpriteRenderer;
    public Rigidbody2D playerRigidBody;

    [Header("Physics Related")]
    public float moveForce;
    public float jumpForce;
    public bool isGrounded;
    public Transform groundTarget;
    public Vector2 maximumVelocity = new Vector2(10.0f, 15.0f);

    [Header("Sounds")]
    public AudioSource jumpSound;
    public AudioSource crystalSound;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));

        // Idle State
        if (Input.GetAxis("Horizontal") == 0)
        {
            playerState = PlayerState.IDLE;
            playerAnimator.SetInteger("State", (int)PlayerState.IDLE);
        }


        // Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            //sets player sprite facing the right
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            if (isGrounded)
            {
                playerState = PlayerState.WALK;
                playerAnimator.SetInteger("State", (int)PlayerState.WALK);
                //force behind forward motion and allows for slope climbing
                playerRigidBody.AddForce(new Vector2(1, 1) * moveForce);
            }
        }

        // Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            //flips the player object on it's x-axis (includes the sprite and the collider)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            if (isGrounded)
            {
                playerState = PlayerState.WALK;
                playerAnimator.SetInteger("State", (int)PlayerState.WALK);
                playerRigidBody.AddForce(new Vector2(-1, 1) * moveForce);
            }
        }

        // Jump
        if ((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            playerState = PlayerState.JUMP;
            playerAnimator.SetInteger("State", (int)PlayerState.JUMP);
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpSound.Play();
        }

        playerRigidBody.velocity = new Vector2(
            Mathf.Clamp(playerRigidBody.velocity.x, -maximumVelocity.x, maximumVelocity.x),
            Mathf.Clamp(playerRigidBody.velocity.y, -maximumVelocity.y, maximumVelocity.y)
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Crystal"))
        {
            gameController.Score += 100;
            crystalSound.Play();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameController.Lives -= 1;
            this.gameObject.transform.position = activeSpawnpoint.position;
        }
    }
}
