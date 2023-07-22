using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
/*     public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContractFilter2D movementFilter;

    Vector2 movementInput;
    RigidBody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); */

    // Start is called before the first frame update
    void Start()
    {
        /* rb = GetComponent<RigidBody2D>(); */
        
    }

/*     private void FixedUpdate() {
        if(movementInput != Vector2.zero){
            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed = Time.fixedDeltaTime + collisionOffset);
        }
    } */

/*     void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    } */
}
