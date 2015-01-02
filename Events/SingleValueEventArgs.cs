#region
using System;

#endregion

namespace bscheiman.Common.Events {
    public class GenericEventArgs<TFirst> : EventArgs {
        public TFirst Data { get; set; }

        public GenericEventArgs(TFirst data) {
            Data = data;
        }
    }

    public class GenericEventArgs<TFirst, TSecond> : GenericEventArgs<TFirst> {
        public TSecond Data2 { get; set; }

        public GenericEventArgs(TFirst value, TSecond value2) : base(value) {
            Data2 = value2;
        }
    }

    public class GenericEventArgs<TFirst, TSecond, TThird> : GenericEventArgs<TFirst, TSecond> {
        public TThird Data3 { get; set; }

        public GenericEventArgs(TFirst value, TSecond value2, TThird value3) : base(value, value2) {
            Data3 = value3;
        }
    }
}