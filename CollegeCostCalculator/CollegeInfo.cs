using System;

namespace CollegeCostCalculator
{
    /// <summary>
    /// Holds basic information about a college. Any missing values will be null.
    /// </summary>
    [Serializable()]
    internal struct CollegeInfo
    {
        public string Name { get; set; }

        public Decimal? TuitionInState { get; set; }

        public Decimal? TuitionOutState { get; set; }

        public Decimal? RoomBoard { get; set; }

        internal CollegeInfo(string name, Decimal? tuitionInState, Decimal? tuitionOutState, Decimal? roomBoard)
        {
            Name = name;
            TuitionInState = tuitionInState;
            TuitionOutState = tuitionOutState;
            RoomBoard = roomBoard;
        }
    }
}
