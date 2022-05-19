using System;

namespace Common.Utilities.Randomizer
{
  public class StockRandomNumberGenerator : IRandomNumberGenerator
  {
    private readonly Random _random;

    public StockRandomNumberGenerator()
    {
      _random = new Random();
    }

    public int GetNumber(int lowerValue, int upperValue) => _random.Next(lowerValue, upperValue);
  }
}
