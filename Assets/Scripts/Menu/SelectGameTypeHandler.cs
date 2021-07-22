using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент позволяющий выбрать тип игры, по количеству игроков
    /// </summary>
    public class SelectGameTypeHandler : MonoBehaviour
    {
        [Tooltip("Указатель с помощью которого выбирается тип игры")]
        [SerializeField]
        private RectTransform pointer;

        private float _pointerHeight;
        private float _startOffsetMaxY;

        private void Awake()
        {
            _pointerHeight = pointer.sizeDelta.y;
            _startOffsetMaxY = pointer.offsetMax.y;
        }

        private void Update()
        {
            switch (Input.GetAxisRaw("Vertical"))
            {
                case 1:
                    MovePointer(_startOffsetMaxY);
                    GameState.Instance.CountType = GameState.GameType.One;
                    break;
                case -1:
                    MovePointer(_startOffsetMaxY - _pointerHeight);
                    GameState.Instance.CountType = GameState.GameType.Two;
                    break;
            }
        }

        private void MovePointer(float position)
        {
            pointer.offsetMax = new Vector2(pointer.offsetMax.x, position);
            pointer.offsetMin = new Vector2(pointer.offsetMin.x, position - _pointerHeight);
        }
    }
}