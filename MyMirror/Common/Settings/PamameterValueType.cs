// -----------------------------------------------------------------------
// <copyright file="PamameterValueType.cs">
//
// </copyright>
// <summary>Enumerate widgets possible positions</summary>
// -----------------------------------------------------------------------

namespace Common.Settings
{
    /// <summary>
    /// Enumerate all the parameters types
    /// </summary>
    public enum PamameterValueType
    {
        /// <summary>
        /// Parameter of type "List of string"
        /// </summary>
        ListOfString,

        /// <summary>
        /// Parameter of type "List of integer"
        /// </summary>
        ListOfInteger,

        /// <summary>
        /// Parameter of type "Field Integer"
        /// </summary>
        FieldInteger,

        /// <summary>
        /// Parameter of type "Field String"
        /// </summary>
        FieldString,

        /// <summary>
        /// Parameter of type "Field URL"
        /// </summary>
        FieldUrl,

        /// <summary>
        /// Parameter of type "Boolean"
        /// </summary>
        Boolean
    }
}