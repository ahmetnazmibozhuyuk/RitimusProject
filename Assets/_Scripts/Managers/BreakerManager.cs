using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS.Managers
{
    [RequireComponent(typeof(BreakerUIManager))]
    public class BreakerManager : Singleton<BreakerManager>
    {
        private BreakerUIManager _breakerUIManager;

        protected override void Awake()
        {
            base.Awake();
            _breakerUIManager = GetComponent<BreakerUIManager>();
        }

    }
}
