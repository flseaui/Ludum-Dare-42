using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private float _speed;

	private Vector2 _velocity;
	private Vector2 _acceleration;
	private float _gravity;
	
	// Use this for initialization
	void Start () {
		
	}

	private void FixedUpdate()
	{
		var rb = GetComponent<Rigidbody2D>();
		rb.MovePosition(new Vector2(rb.position.x + _velocity.x, rb.position.y + _velocity.y));
	}

	// Update is called once per frame
	private void Update ()
	{
		var left = Input.GetKey(KeyCode.A);
		var right = Input.GetKey(KeyCode.D);
		var jump = Input.GetKey(KeyCode.W);

		if (left && right || (!left && !right))
			_acceleration.x = 0;
		else 
		{
			if (left)
				_acceleration.x = -_speed;
			if (right)
				_acceleration.x = _speed;
		}

		if (jump)
		{
			_acceleration.y = _speed;
		}
		else
		{
			_acceleration.y = 0;
		}
		
		_velocity = _acceleration;
	}
}
