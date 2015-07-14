using Assets.Scripts.AlternativeInput;
using UnityEngine;

namespace Assets.Scripts.Character
{
	[RequireComponent(typeof (EndlessRunCharacter))]
	public class EndlessRunController : MonoBehaviour
	{
		private EndlessRunCharacter m_Character;
		private bool m_Jump;

		[SerializeField] private float m_RunForce = 1.0f;		
		
		private void Awake()
		{
			m_Character = GetComponent<EndlessRunCharacter>();
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
			    m_Jump = Input.GetButtonDown("Jump") || TouchInputBehaviour.GetJump();
			}
		}
		
		
		private void FixedUpdate()
		{
			// Read the inputs.
		    bool crouch = Input.GetButton("Crouch") || TouchInputBehaviour.GetCrouch();
			m_Character.ApplyControl(m_RunForce, crouch, m_Jump);
			m_Jump = false;
		}
	}
}
