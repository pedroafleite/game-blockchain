using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public LayerMask interactableLayer;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        // Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        setRotation(1, 0);
    }

    private void FixedUpdate() {
        // If movement input is not 0, try to move
        if(movementInput != Vector2.zero){
            bool success = tryMove(movementInput); // Check for potential collisions
            if(!success) {
                success = tryMove(new Vector2(movementInput.x, 0));
                if(!success)
                    success = tryMove(new Vector2(0, movementInput.y));
            }

            setRotation(movementInput.x, movementInput.y);

            if (movementInput.x < 0 && movementInput.y > 0)
                setRotation(0, 1);
            if (movementInput.x < 0 && movementInput.y < 0)
                setRotation(0, -1);

            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    private bool tryMove(Vector2 direction) {
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished 
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
        
        if(count == 0) {                
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    private void setRotation(float x, float y) {
        animator.SetFloat("moveX", x);
        animator.SetFloat("moveY", y);
    }

    private void Interact() {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 0.3f);

        var collider = Physics2D.OverlapCircle(interactPos, 1f, interactableLayer);
        if(collider != null) {
            Debug.Log("there is an NPC here!");
        } else {
            Debug.Log("No NPC here!");
        }
    }
    
}
