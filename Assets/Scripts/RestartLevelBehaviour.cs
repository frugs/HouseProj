using System.Collections;

using UnityEngine;

using Assets.Scripts.UI;
using Assets.Scripts.Character;

namespace Assets.Scripts
{
	[RequireComponent(typeof(Collider2D))]
	public class RestartLevelBehaviour : MonoBehaviour
	{
		[SerializeField]
		private ScoreDisplayBehaviour _scoreDisplay;
		[SerializeField]
		private FullscreenScoreDisplayBehaviour _fullscreenScoreDisplay;
        
		public void OnTriggerEnter2D (Collider2D other)
		{
			if (other.tag == "Player") {
				other.GetComponent<EndlessRunCharacter>().Kill();
                StartCoroutine(RestartLevel());
			}
		}

		private IEnumerator RestartLevel()
		{
			_scoreDisplay.gameObject.SetActive(false);
			_fullscreenScoreDisplay.gameObject.SetActive(true);
			yield return new WaitForSeconds(1.5f);
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}