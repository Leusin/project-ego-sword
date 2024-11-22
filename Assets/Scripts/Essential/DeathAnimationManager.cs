using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        private DeathAnimationLoader _deathAnimationLeader;
        private List<RuntimeAnimatorController> _candidates = new List<RuntimeAnimatorController>();

        void SetupDeathAnimationLoader()
        {
            if( _deathAnimationLeader == null )
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                _deathAnimationLeader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GeneralBodyPart generalBodyPart, AttacCondition info)
        {
            SetupDeathAnimationLoader();

            _candidates.Clear();

            foreach(DeathAnimationData data in _deathAnimationLeader.DeathAnimationDataList)
            {
                if(info.lunchIntoAir)
                {
                    if(data.lunchIntoAir)
                    {
                        _candidates.Add(data.animator);
                    }
                }
                else
                {
                    foreach (GeneralBodyPart part in data.generalBodyParts)
                    {
                        if (part == generalBodyPart)
                        {
                            _candidates.Add(data.animator);
                            break;
                        }
                    }
                }
            }

            return _candidates[Random.Range(0, _candidates.Count)];
        }
    }
}