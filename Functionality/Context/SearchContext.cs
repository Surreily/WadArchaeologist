using System;
using System.Collections.Generic;
using System.Text;

namespace Surreily.WadArchaeologist.Functionality.Context {
    public class SearchContext {
        /// <summary>
        /// Get or set a value indicating whether the search should ignore the WAD directory.
        /// </summary>
        public bool ShouldIgnoreDirectory { get; set; }

        /// <summary>
        /// Get or set a value specifying the minimum number of sides per map.
        /// </summary>
        public int MinimumNumberOfSidesPerMap { get; set; }
    }
}
