#region
using System;
using System.Threading;

#endregion

namespace bscheiman.Common.Helpers {
    public static class RandomHelper {
        internal static int SeedCounter = new Random().Next();
        [ThreadStatic] private static Random _rng;

        public static Random Instance {
            get {
                if (_rng == null) {
                    int seed = Interlocked.Increment(ref SeedCounter);

                    _rng = new Random(seed);
                }

                return _rng;
            }
        }
    }
}