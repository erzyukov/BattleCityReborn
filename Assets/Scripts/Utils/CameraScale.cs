using UnityEngine;

namespace BS
{
	/// <summary>
	/// Масштабирование экрана
	/// </summary>
    [RequireComponent(typeof(Camera))]
	public class CameraScale : MonoBehaviour
    {
        private void Start()
        {
			// set the desired aspect ratio
			var targetaspect = (float) 256 / (float) 224;

			// determine the game window's current aspect ratio
			var windowaspect = (float)Screen.width / (float)Screen.height;

			// current viewport height should be scaled by this amount
			var scaleheight = windowaspect / targetaspect;

			// obtain camera component so we can modify its viewport
			var camera = GetComponent<Camera>();

			// if scaled height is less than current height, add letterbox
			if (scaleheight < 1.0f)
			{
				var rect = camera.rect;

				rect.width = 1.0f;
				rect.height = scaleheight;
				rect.x = 0;
				rect.y = (1.0f - scaleheight) / 2.0f;

				camera.rect = rect;
			}
			else // add container box
			{
				var scalewidth = 1.0f / scaleheight;

				var rect = camera.rect;

				rect.width = scalewidth;
				rect.height = 1.0f;
				rect.x = (1.0f - scalewidth) / 2.0f;
				rect.y = 0;

				camera.rect = rect;
			}
		}
    }
}