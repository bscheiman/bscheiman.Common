#region
using System.Text;

#endregion

namespace bscheiman.Common.Extensions {
    public static class StringBuilderExtensions {
        public static void AppendLine(this StringBuilder builder, string format, params object[] args) {
            builder.ThrowIfNull("builder");
            format.ThrowIfNull("format");
            args.ThrowIfNull("args");

            builder.AppendFormat(format, args);
            builder.AppendLine();
        }
    }
}
