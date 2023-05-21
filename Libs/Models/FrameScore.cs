using System;
using System.Collections.Generic;
using System.Linq;

namespace Estimator.Models
{
  /// <summary>
  /// Single time frame data
  /// </summary>
  public class FrameData
  {
    public double Gains { get; set; }
    public double Losses { get; set; }
  }

  /// <summary>
  /// Single time frame data
  /// </summary>
  public class FrameResponse
  {
    public IDictionary<string, FrameData> Days { get; set; } = new Dictionary<string, FrameData>();
    public IDictionary<string, FrameData> Hours { get; set; } = new Dictionary<string, FrameData>();
    public IDictionary<string, FrameData> Months { get; set; } = new Dictionary<string, FrameData>();
  }

  /// <summary>
  /// Statistics grouped by time frames
  /// </summary>
  public class FrameScore
  {
    /// <summary>
    /// Input values
    /// </summary>
    public virtual IList<InputData> Items { get; set; } = new List<InputData>();

    /// <summary>
    /// Calculate
    /// </summary>
    /// <returns></returns>
    public virtual FrameResponse Calculate()
    {
      var count = Items.Count;
      var stats = new FrameResponse();

      for (var i = 0; i < count; i++)
      {
        var current = Items.ElementAtOrDefault(i);
        var previous = Items.ElementAtOrDefault(i - 1);

        CreateStatsByFrame(current, previous, current.Time.Hour.ToString(), stats.Hours);
        CreateStatsByFrame(current, previous, current.Time.Month.ToString(), stats.Months);
        CreateStatsByFrame(current, previous, current.Time.DayOfWeek.ToString(), stats.Days);
      }

      return stats;
    }

    /// <summary>
    /// Split statistics by time frame
    /// </summary>
    /// <param name="currentInput"></param>
    /// <param name="previousInput"></param>
    /// <param name="index"></param>
    /// <param name="items"></param>
    protected void CreateStatsByFrame(
      InputData currentInput,
      InputData previousInput,
      string index,
      IDictionary<string, FrameData> items)
    {
      var group = items.TryGetValue(index, out FrameData o) ? o : new FrameData();

      group.Gains += Math.Abs(Math.Max(currentInput.Value - previousInput.Value, 0.0));
      group.Losses += Math.Abs(Math.Min(currentInput.Value - previousInput.Value, 0.0));
    }
  }
}
