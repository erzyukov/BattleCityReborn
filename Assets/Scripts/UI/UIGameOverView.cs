using System.Collections;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за отображение окна окончания игры
    /// </summary>
    public class UIGameOverView : MonoBehaviour
    {
        [Tooltip("Объект содержащий текст")]
        [SerializeField]
        private RectTransform gameOverTextRect;

        [Tooltip("Скорость движения текста")]
        [SerializeField]
        private float speed;

        private float _targetMaxOffetY;

        private void Start()
        {
            _targetMaxOffetY = gameOverTextRect.sizeDelta.y / 2;

            gameObject.SetActive(false);
            gameOverTextRect.gameObject.SetActive(false);
        }

        /// <summary>
        /// Отображает окно конца игры
        /// </summary>
        public void SetGameOverState()
        {
            gameObject.SetActive(true);
            gameOverTextRect.gameObject.SetActive(true);

            StartCoroutine(ShowGameOverTextCoroutine());
        }

        IEnumerator ShowGameOverTextCoroutine()
        {
            while (gameOverTextRect.offsetMax.y < _targetMaxOffetY)
            {
                yield return null;
                gameOverTextRect.offsetMax = new Vector2(gameOverTextRect.offsetMax.x, gameOverTextRect.offsetMax.y + speed * Time.deltaTime);
                gameOverTextRect.offsetMin = new Vector2(gameOverTextRect.offsetMin.x, gameOverTextRect.offsetMin.y + speed * Time.deltaTime);
            }
        }
    }
}