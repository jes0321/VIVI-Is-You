using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGMPool
{
    //public int ¼±ÇÑ½Ü³ªÀÌ { get; set; } = 43;

    public interface IPoolable
    {
        public PoolTypeSO PoolType { get; }
        public GameObject GameObject { get; }
        public void SetUpPool(Pool pool);
        public void ResetItem();
    }
}
