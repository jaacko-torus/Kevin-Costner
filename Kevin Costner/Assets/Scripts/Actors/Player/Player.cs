using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Animator animator;
	private Rigidbody2D rigidbody2D;
	
	
	private bool moving;
	private Vector2 input, movement, facing;
	private float speed;
	
	
	private void Awake() {
		this.animator    = GetComponent<Animator>();
		this.rigidbody2D = GetComponent<Rigidbody2D>();
		
		
		
		this.moving = false;
		
		this.input = new Vector2(0, 0);
		this.movement = new Vector2(0, 0);
		this.facing = new Vector2(0, 0);
		
		this.speed = 1f;
		
		
		
		this.animator.SetBool("Moving", false);
		this.animator.SetFloat("Horizontal" ,  0);
		this.animator.SetFloat("Vertical"   , -1);
	}
	
	private void FixedUpdate() {
		this.InputManager();
		this.MovementManager();
		this.AnimationManager();
	}
	
	private void InputManager() {
		this.input.x = Input.GetAxisRaw("Horizontal");
		this.input.y = Input.GetAxisRaw("Vertical");
		
		if (this.input.magnitude != 1f) { this.input = Vector2.zero; }
	}
	
	private void MovementManager() {
		// I need to define a new vector since `position` is in Vector3 form
		this.movement = this.input * this.speed * Time.fixedDeltaTime;
		
		this.moving = this.movement.magnitude > 0 ? true : false;
		
		if (this.moving) {
			this.rigidbody2D.MovePosition(new Vector2(
				this.transform.position.x + this.movement.x,
				this.transform.position.y + this.movement.y
			));
		}
	}
	
	private void AnimationManager() {
		if (this.moving) {
			this.animator.SetBool("Moving", true);
			this.animator.SetFloat("Horizontal" , this.input.x);
			this.animator.SetFloat("Vertical"   , this.input.y);
		} else {
			this.animator.SetBool("Moving", false);
		}
	}
}
