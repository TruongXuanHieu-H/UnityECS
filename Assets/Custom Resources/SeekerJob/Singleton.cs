using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeekerJob
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        public static T instance => _instance;

        protected virtual void Awake()
        {
            if (_instance == null || _instance == this)
                _instance = this as T;
            else
                Destroy(this.gameObject);
        }
    }
}
