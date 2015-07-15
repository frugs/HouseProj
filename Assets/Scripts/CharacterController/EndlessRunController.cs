using Assets.Scripts.AlternativeInput;
using UnityEngine;

namespace Assets.Scripts.Character
{
	[RequireComponent(typeof (EndlessRunCharacter))]
	public class EndlessRunController : MonoBehaviour
	{
		private EndlessRunCharacter m_Character;
		private bool m_Jump;

		private void Awake()
		{
			m_Character = GetComponent<EndlessRunCharacter>();
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
			    m_Jump = Input.GetButtonDown("Jump") || TouchInput.GetJump();
			}
		}
		
		
		private void FixedUpdate()
		{
			// Read the inputs.
		    bool crouch = Input.GetButton("Crouch") || TouchInput.GetCrouch();
			m_Character.ApplyControl(crouch, m_Jump);
			m_Jump = false;
		}
	}
}
