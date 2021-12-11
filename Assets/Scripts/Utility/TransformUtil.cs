using System.Collections.Generic;
using UnityEngine;

namespace Utills
{
    public static class TransformUtil
    {
        public static Quaternion ChangeX(this Quaternion rotation, float eulerX)
        {
            var newQuaternion = Quaternion.Euler(eulerX,
                rotation.eulerAngles.y,
                rotation.eulerAngles.z);

            return newQuaternion;
        }

        public static Quaternion ChangeY(this Quaternion rotation, float eulerY)
        {
            var newQuaternion = Quaternion.Euler(rotation.eulerAngles.x,
                eulerY,
                rotation.eulerAngles.z);

            return newQuaternion;
        }

        public static Quaternion ChangeZ(this Quaternion rotation, float eulerZ)
        {
            var newQuaternion = Quaternion.Euler(rotation.eulerAngles.x,
                rotation.eulerAngles.y,
                eulerZ);

            return newQuaternion;
        }


        public static Vector3 ChangeX(this Vector3 position, float value)
        {
            var newPosition = new Vector3(value,
                position.y,
                position.z);

            return newPosition;
        }

        public static Vector3 ChangeY(this Vector3 position, float value)
        {
            var newPosition = new Vector3(position.x,
                value,
                position.z);

            return newPosition;
        }

        public static Vector3 ChangeZ(this Vector3 position, float value)
        {
            var newPosition = new Vector3(position.x,
                position.y,
                value);

            return newPosition;
        }


        public static Vector2 ChangeX(this Vector2 position, float value)
        {
            var newPosition = new Vector2(value,
                position.y);

            return newPosition;
        }

        public static Vector2 ChangeY(this Vector2 position, float value)
        {
            var newPosition = new Vector2(position.x,
                value);

            return newPosition;
        }

        public class Vector2IntComparer : IComparer<Vector2Int>
        {
            int IComparer<Vector2Int>.Compare(Vector2Int v1, Vector2Int v2)
            {
                if (v1.x < v2.x)
                {
                    return -1;
                }
                else if (v1.x == v2.x)
                {
                    if (v1.y < v2.y)
                    {
                        return -1;
                    }
                    else if (v1.y == v2.y)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
        }

        public class Vector2IntReverseComparer : IComparer<Vector2Int>
        {
            int IComparer<Vector2Int>.Compare(Vector2Int v1, Vector2Int v2)
            {
                if (v1.y < v2.y)
                {
                    return -1;
                }
                else if (v1.y == v2.y)
                {
                    if (v1.x < v2.x)
                    {
                        return -1;
                    }
                    else if (v1.x == v2.x)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}