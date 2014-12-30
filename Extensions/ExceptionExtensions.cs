#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static class ExceptionExtensions {
        public static void Log(this Exception ex) {
            ex.ThrowIfNull("ex");

            Util.Log.Fatal(ex);
        }
    }
}