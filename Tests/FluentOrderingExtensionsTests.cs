#region
using System;
using System.Collections.Generic;
using System.Diagnostics;
using bscheiman.Common.Extensions;
using bscheiman.Common.Helpers;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class FluentOrderingExtensions {
        [Test]
        public void OrderBy() {
            var list = new List<Invoice>();
            var rand = RandomHelper.Instance;

            list.AddRange(new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            }, new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            }, new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            }, new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            }, new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            }, new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            }, new Invoice {
                Id = rand.NextString(6),
                Name = rand.NextString(8) + " " + rand.NextString(10),
                Amount = rand.NextDouble(100, 500)
            });

            Debug.WriteLine(list.OrderFluentlyBy(i => i.Id).ThenBy(i => i.Name).ThenByDescending(i => i.Amount));
            Debug.WriteLine(list.OrderFluentlyByDescending(i => i.Id).ThenBy(i => i.Name).ThenBy(i => i.Amount));
        }

        private class Invoice {
            public double Amount { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}