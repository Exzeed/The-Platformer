using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
/// <summary>
/// Source File: EnemyController.cs
/// Author: Geerthan Kanthasamy
/// This program allows the enemy to move
/// also contains logic for when in close proximity with a wall or edge
/// </summary>
public class EnemyController : MonoBehaviour
{
    public EnemyState enemyState;
    public Animator enemyAnimator;
    public SpriteRenderer enemySpriteRenderer;
    public Rigidbody2D enemyRigidBody;
    public bool isGrounded;
    public bool hasGroundAhead;
    public bool hasWallAhead;
    public Transform lookAhead;
    public Transform wallAhead;
    public bool isFacingRight = true;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.WALK;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        hasWallAhead = Physics2D.Linecast(
            transform.position,
            wallAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded)
        {
            if (isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(movementSpeed, 0.0f);
            }

            if (!isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(-movementSpeed, 0.0f);
            }

            if (!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 2.0f, 1.0f);
                isFacingRight = !isFacingRight;
            }
        }
    }
}
