#region
using System;
using System.Threading;

#endregion

namespace bscheiman.Common.Helpers {
    public static class RandomHelper {
        [ThreadStatic] private static Random _rng;
        private static int _seedCounter = new Random().Next();

        public static Random Instance {
            get {
                if (_rng == null) {
                    int seed = Interlocked.Increment(ref _seedCounter);

                    _rng = new Random(seed);
                }

                return _rng;
            }
        }
    }
}