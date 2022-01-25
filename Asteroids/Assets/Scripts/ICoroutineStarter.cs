using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public interface ICoroutineStarter
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
