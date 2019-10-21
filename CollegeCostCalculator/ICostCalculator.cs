using System;

namespace CollegeCostCalculator
{
    public interface ICostCalculator
    {
        Decimal GetCost(String collegeName, bool includeRoomBoard = true);
    }
}
