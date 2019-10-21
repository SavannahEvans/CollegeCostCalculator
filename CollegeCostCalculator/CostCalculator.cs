using System;

namespace CollegeCostCalculator
{
    /// <summary>
    /// Calculates the cost of tuition for a college with or without room and board costs.
    /// </summary>
    public class CostCalculator : ICostCalculator
    {
        private CollegeInfoReader Reader;

        /// <summary>
        /// Creates a new CostCalculator and instatiates a new CollegeInfoReader with college data.
        /// </summary>
        /// <Exception>Thrown when college data cannot be retrieved.</Exception>
        public CostCalculator()
        {
            Reader = new CollegeInfoReader();
        }

        /// <summary>
        /// If the specified college exists, returns the cost of in-state tuition plus room and board,
        /// or only tuition if specified.
        /// </summary>
        /// <remarks>
        /// If the in-state tuition data of the specified college is missing the cost of out-of-state
        /// tuition is used in it's place.
        /// </remarks>
        /// <param name="collegeName">The exact matching name of a college.</param>
        /// <param name="includeRoomBoard">Optional, defaut true.</param>
        /// <returns>Cost of tuition or cost of tuition plus room and board.</returns>
        /// <Exception>Thrown when college name is empty or does not exist.</Exception>
        /// <Exception>Thrown when college does not have tuition data.</Exception>
        public Decimal GetCost(String collegeName, bool includeRoomBoard = true)
        {
            if (collegeName == null || collegeName.Length == 0)
            {
                throw new Exception("Error: College name is required");
            }

            if (Reader.Colleges.TryGetValue(collegeName.Trim(), out CollegeInfo info))
            {
                Decimal cost = info.TuitionInState ?? info.TuitionOutState ?? throw new Exception("Could not find tuition data.");
                if (includeRoomBoard)
                {
                    cost = cost + (info.RoomBoard ?? 0);
                }
                return cost;
            }
            else
            {
                throw new Exception("Error: College not found");
            }
        }
    }
}
