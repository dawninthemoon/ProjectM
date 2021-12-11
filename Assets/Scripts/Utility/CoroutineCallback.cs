using System;
using System.Collections;

namespace Utills
{
    public static class CoroutineCallback
    {
        public static IEnumerator WaitThenCallback(Action callback)
        {
            yield return null;

            callback();
        }

        public static IEnumerator WaitThenCallback(float time, Action callback)
        {
            yield return YieldInstructionCache.WaitForSeconds(time);

            callback();
        }
    }
}