using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeekerJob
{
    public class Spawner : Singleton<Spawner>
    {
        [SerializeField] private int numberSeeker;
        [SerializeField] private Seeker seekerPref;
        [SerializeField] private int numberTarget;
        [SerializeField] private Target targetPref;

        [SerializeField] public float heightClamp;
        [SerializeField] public float widthClamp;
        [SerializeField] public float lenghtClamp;

        public List<Seeker> spawnedSeekers = new List<Seeker>();
        public List<Target> spawnedTargets = new List<Target>();

        protected override void Awake()
        {
            base.Awake();

            Generate();
        }

        [Button]
        public void Generate()
        {
            Clear();
            Spawn();
        }

        private void Spawn()
        {
            SpawnSeekers();
            SpawnTarget();            
        }

        private void SpawnSeekers()
        {
            for (int i = 0; i < numberSeeker; i++)
            {
                Seeker seeker = Instantiate(seekerPref, 
                    new Vector3(
                        Random.Range(-widthClamp, widthClamp), 
                        Random.Range(-heightClamp, heightClamp),
                        Random.Range(-lenghtClamp, lenghtClamp)), 
                    Quaternion.identity);
                spawnedSeekers.Add(seeker);
                seeker.Init();
                seeker.Enter();
            }
        }

        private void SpawnTarget()
        {
            for (int i = 0; i < numberTarget; i++)
            {
                Target target = Instantiate(targetPref,
                    new Vector3(
                        Random.Range(-widthClamp, widthClamp),
                        Random.Range(-heightClamp, heightClamp),
                        Random.Range(-lenghtClamp, lenghtClamp)),
                    Quaternion.identity);
                spawnedTargets.Add(target);
                target.Init();
                target.Enter();
            }
        }

        private void Clear()
        {
            ClearSeekers();
            ClearTargets();
        }

        private void ClearSeekers()
        {
            foreach (var moveActor in spawnedSeekers)
            {
                moveActor.Exit();
                Destroy(moveActor.gameObject);
            }
        }

        private void ClearTargets()
        {
            foreach (var moveActor in spawnedTargets)
            {
                moveActor.Exit();
                Destroy(moveActor.gameObject);
            }
        }
    }
}
