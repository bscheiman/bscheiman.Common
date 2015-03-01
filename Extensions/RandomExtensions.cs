#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public enum CharType {
        AlphabeticLower,
        AlphabeticUpper,
        AlphabeticAny,
        AlphanumericLower,
        AlphanumericUpper,
        AlphanumericAny,
        Numeric
    }

    public static class RandomExtensions {
        private const double AlphanumericProbabilityNumericAny = 10.0 / 62.0;
        private const double AlphanumericProbabilityNumericCased = 10.0 / 36.0;

        public static bool NextBool(this Random random, double probability = 0.5) {
            return random.NextDouble() <= probability;
        }

        public static char NextChar(this Random random, CharType mode = CharType.AlphanumericAny) {
            switch (mode) {
                case CharType.AlphabeticAny:
                    return random.NextAlphabeticChar();

                case CharType.AlphabeticLower:
                    return random.NextAlphabeticChar(false);

                case CharType.AlphabeticUpper:
                    return random.NextAlphabeticChar(true);

                case CharType.AlphanumericLower:
                    return random.NextAlphanumericChar(false);

                case CharType.AlphanumericUpper:
                    return random.NextAlphanumericChar(true);

                case CharType.Numeric:
                    return random.NextNumericChar();

                default:
                    return random.NextAlphanumericChar();
            }
        }

        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue) {
            var min = minValue.ToEpoch();
            var max = maxValue.ToEpoch();

            return ((long) random.NextDouble(min, max)).FromEpoch();
        }

        public static DateTime NextDateTime(this Random random) {
            return random.NextDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

        public static double NextDouble(this Random random, double minValue, double maxValue) {
            if (maxValue < minValue)
                throw new ArgumentException("Minimum value must be less than maximum value.");

            double difference = maxValue - minValue;

            if (!double.IsInfinity(difference))
                return minValue + (random.NextDouble() * difference);

            double halfDifference = (maxValue * 0.5) - (minValue * 0.5);

            return random.NextBool()
                ? minValue + (random.NextDouble() * halfDifference)
                : (minValue + halfDifference) + (random.NextDouble() * halfDifference);
        }

        public static string NextString(this Random random, int numChars, CharType mode) {
            var chars = new char[numChars];

            for (int i = 0; i < numChars; ++i)
                chars[i] = random.NextChar(mode);

            return new string(chars);
        }

        public static string NextString(this Random random, int numChars) {
            return random.NextString(numChars, CharType.AlphanumericAny);
        }

        public static TimeSpan NextTimeSpan(this Random random, TimeSpan minValue, TimeSpan maxValue) {
            return TimeSpan.FromMilliseconds(random.NextDouble(minValue.TotalMilliseconds, maxValue.TotalMilliseconds));
        }

        public static TimeSpan NextTimeSpan(this Random random) {
            return random.NextTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue);
        }

        private static char NextAlphabeticChar(this Random random, bool uppercase) {
            return uppercase ? (char) random.Next(65, 91) : (char) random.Next(97, 123);
        }

        private static char NextAlphabeticChar(this Random random) {
            return random.NextAlphabeticChar(random.NextBool());
        }

        private static char NextAlphanumericChar(this Random random, bool uppercase) {
            bool numeric = random.NextBool(AlphanumericProbabilityNumericCased);

            return numeric ? random.NextNumericChar() : random.NextAlphabeticChar(uppercase);
        }

        private static char NextAlphanumericChar(this Random random) {
            bool numeric = random.NextBool(AlphanumericProbabilityNumericAny);

            return numeric ? random.NextNumericChar() : random.NextAlphabeticChar(random.NextBool());
        }

        private static char NextNumericChar(this Random random) {
            return (char) random.Next(48, 58);
        }
    }
}