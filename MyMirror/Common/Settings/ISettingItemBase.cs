// -----------------------------------------------------------------------
// <copyright file="ISettingItemBase.cs">
//
// </copyright>
// <summary>Contains interface ISettingItemBase</summary>
// -----------------------------------------------------------------------

using System;

namespace Common.Settings
{
    /// <summary>
    /// Interface for setting item base
    /// </summary>
    public interface ISettingItemBase
    {
        /// <summary>
        /// Gets or set settings name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or set settings displayed name
        /// </summary>
        string Translation { get; set; }

        /// <summary>
        /// Gets or set settings setting string value
        /// </summary>
        string StringValue { get; set; }

        /// <summary>
        /// Gets or set settings setting display type
        /// </summary>
        PamameterValueType DisplayType { get; }

        /// <summary>
        /// Initialize fields with resources
        /// </summary>
        /// <param name="Resources">Resources</param>
        void InitializeFields(Type Resources);
    }
}
