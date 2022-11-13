using ExScore.ModelSpace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExScore.ScoreSpace
{
  /// <summary>
  /// MAR ratio
  /// Minimum acceptance return or a ratio between returns and max loss
  /// CAGR = Compound annual growth rate
  /// DD = Maximum drawdown in a series
  /// MAR = CAGR / DD
  /// </summary>
  public class MAR
  {
    /// <summary>
    /// Input values
    /// </summary>
    public virtual IList<InputData> Items { get; set; } = new List<InputData>();

    /// <summary>
    /// Calculate
    /// </summary>
    /// <returns></returns>
    public virtual double Calculate()
    {
      var count = Items.Count;

      if (count < 2)
      {
        return 0;
      }

      var maxLoss = 0.0;
      var cagr = new CAGR
      {
        Items = Items
      };

      for (var i = 1; i < count; i++)
      {
        var currentValue = Items.ElementAtOrDefault(i).Value;
        var previousValue = Items.ElementAtOrDefault(i - 1).Value;

        if (previousValue > currentValue)
        {
          maxLoss = Math.Max(maxLoss, previousValue - currentValue);
        }
      }

      if (maxLoss == 0)
      {
        return 0.0;
      }

      return cagr.Calculate() / maxLoss;
    }
  }
}
