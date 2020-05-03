// -----------------------------------------------------------------------
// <copyright file="IntegerSettingItem.cs">
//
// </copyright>
// <summary>Contains class IntegerSettingItem</summary>
// -----------------------------------------------------------------------

using System;

namespace Common.Settings.Items
{
    /// <summary>
    /// Setting item of time integer
    /// </summary>
    public class IntegerSettingItem : SettingItemBase<int>
    {
        /// </summary>
        /// Gets or set settings min value
        /// </summary>
        public int MinValue { get; set; }

        /// </summary>
        /// Gets or set settings min value
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// Default contructor
        /// </summary>
        public IntegerSettingItem ()
        {
            MinValue = 0;
            MaxValue = Int32.MaxValue;
        }

        /// <inheritdoc />
        public override PamameterValueType DisplayType => PamameterValueType.Field;

        /// <inheritdoc />
        public override string StringValue
        {
            get => Value < MinValue ? string.Empty : Value.ToString();
            set
            {
                int val = MinValue - 1;
                Int32.TryParse(value, out val);
                Value = val;
                NotifyPropertyChanged(nameof(Value));
            }
        }

        /// <inheritdoc />
        public override void InitializeFields(Type Resources)
        {
            MinValue = Int32.TryParse(Resources.GetProperty(nameof(MinValue)).GetValue(null) as string, out int minValue) ? minValue : 0;
            MaxValue = Int32.TryParse(Resources.GetProperty(nameof(MinValue)).GetValue(null) as string, out int maxValue) ? minValue : Int32.MaxValue;
        }
    }
}
