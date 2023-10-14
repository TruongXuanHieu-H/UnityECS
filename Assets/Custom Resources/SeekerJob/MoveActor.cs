using SeekerJob;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeekerJob
{
    public class MoveActor : MonoBehaviour, IActor
    {
        public float speed;
        public Vector3 direction;
        public Vector3 clampArea;
        public bool isEnter;


        [Button]
        public virtual void Init()
        {
            speed = Random.Range(0.1f, 0.2f);
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        [Button]
        public virtual void Enter()
        {
            clampArea = new Vector3(Spawner.instance.widthClamp, Spawner.instance.heightClamp, Spawner.instance.lenghtClamp);
            isEnter = true;
        }

        [Button]
        public virtual void Exit()
        {
            clampArea = Vector2.zero;
            isEnter = false;
        }

        protected virtual void Update()
        {
            if (!isEnter)
                return;

            transform.position += speed * direction * Time.deltaTime;

            List<bool> isInside = CheckInsideArea();
            Vector3 clampPosition = transform.position;
            if (!isInside[0])
            {
                direction.x = -direction.x;
                clampPosition.x = Clamp(clampPosition.x, -Spawner.instance.widthClamp, Spawner.instance.widthClamp);
            }
            if (!isInside[1])
            {
                direction.y = -direction.y;
                clampPosition.y = Clamp(clampPosition.y, -Spawner.instance.heightClamp, Spawner.instance.heightClamp);
            }
            if (!isInside[2])
            {
                direction.z = -direction.z;
                clampPosition.z = Clamp(clampPosition.z, -Spawner.instance.lenghtClamp, Spawner.instance.lenghtClamp);
            }
            transform.position = clampPosition;
        }

        protected virtual List<bool> CheckInsideArea()
        {
            List<bool> results = new List<bool>();
            Vector3 currentPosition = transform.position;
            results.Add(-clampArea.x <= currentPosition.x && currentPosition.x <= clampArea.x);
            results.Add(-clampArea.y <= currentPosition.y && currentPosition.y <= clampArea.y);
            results.Add(-clampArea.z <= currentPosition.z && currentPosition.z <= clampArea.z);
            return results;
        }

        protected float Clamp(float value, params float[] clampValue)
        {
            if (clampValue == null || clampValue.Length == 0)
                return value;

            float[] diff = new float[clampValue.Length];
            int minIndex = 0;
            for (int i = 0; i < clampValue.Length; i++)
            {
                diff[i] = Mathf.Abs(value - clampValue[i]);
                if (diff[i] < diff[minIndex])
                    minIndex = i;
            }
            return clampValue[minIndex];
        }
    }
}
