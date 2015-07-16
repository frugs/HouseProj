using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    [RequireComponent(typeof (InputField))]
    public class FocusOnEnableInputFieldBehaviour : MonoBehaviour {
        public void OnEnable() {
            GetComponent<InputField>().ActivateInputField();
        }
    }
}