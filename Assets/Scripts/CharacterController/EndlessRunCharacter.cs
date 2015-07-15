using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof (Rigidbody2D), typeof (Animator))]
	public class EndlessRunCharacter : MonoBehaviour
	{
		[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
		[SerializeField] private float m_JumpVelocity = 13.1f;                  // Amount of force added when the player jumps.
		[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
		
		private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
		const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
		private bool m_Grounded;            // Whether or not the player is grounded.
		private Animator m_Anim;            // Reference to the player's animator component.
		private Rigidbody2D m_Rigidbody2D;
	    private bool _isDead;

	    private void Awake()
		{
			// Setting up references.
			m_GroundCheck = transform.Find("GroundCheck");
			m_Anim = GetComponent<Animator>();
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			m_Grounded = false;
			
			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
					m_Grounded = true;
			}
			m_Anim.SetBool("Ground", m_Grounded);
		}
		
		public void ApplyControl(float move, bool crouch, bool jump)
		{
		    //only control the player if grounded or and not dead
		    if (m_Grounded && !_isDead) 
            {
                // Set whether or not the character is crouching in the animator
                m_Anim.SetBool("Crouch", crouch);

		        // Move the character
		        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the player should jump...
                if (m_Grounded && jump && m_Anim.GetBool("Ground"))
                {
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.velocity = m_Rigidbody2D.velocity + new Vector2(0f, m_JumpVelocity);
                }
		    }
		}
		
		public void Kill () 
        {
		    if (!_isDead) 
            {
		        _isDead = true;
		    	m_Anim.SetTrigger ("Die");
            }
        }

		public bool isDead() {
			return _isDead;
		}
	}
}
