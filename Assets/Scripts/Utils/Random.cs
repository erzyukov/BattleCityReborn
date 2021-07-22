using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BS.Utils
{
    class Random
    {
        public static Vector2 GetRandomDirection()
        {
            var arr = new Vector2[4] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }

        public static Vector2 GetRandomDirection(Vector2 except)
        {
            var list = new List<Vector2> { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            list.Remove(except);
            var arr = list.ToArray();
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }
    }
}