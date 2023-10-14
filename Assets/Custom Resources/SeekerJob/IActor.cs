using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeekerJob
{
    public interface IActor
    {
        public void Init();
        public void Enter();
        public void Exit();
    }
}
