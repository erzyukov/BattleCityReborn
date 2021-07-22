using System.Collections;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за отображение окна паузы
    /// </summary>
    public class UIPauseView : MonoBehaviour
    {
        [Tooltip("Объект содержащий текст паузы")]
        [SerializeField]
        private GameObject pauseTextObject;

        private Coroutine _blinkCoroutine;

        private void Start()
        {
            gameObject.SetActive(false);
            pauseTextObject.SetActive(false);
        }

        /// <summary>
        /// Отображает или прячет окно паузы
        /// </summary>
        /// <param name="state">Состояние</param>
        public void SetPauseState(bool state)
        {
            gameObject.SetActive(state);
            pauseTextObject.SetActive(state);
            
            if (state)
                _blinkCoroutine = StartCoroutine(TextBlinkCoroutine());
            else if (_blinkCoroutine != null)
                StopCoroutine(_blinkCoroutine);
        }

        IEnumerator TextBlinkCoroutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                pauseTextObject.SetActive(!pauseTextObject.activeSelf);
            }
        }
    }
}