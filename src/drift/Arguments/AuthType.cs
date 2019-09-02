// -----------------------------------------------------------------------
// <copyright file="AuthType.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Arguments
{
    /// <summary>
    /// Defines Azure DevOps authentication types
    /// </summary>
    public enum AuthType
    {
        /// <summary>
        /// Use personal access token
        /// </summary>
        Pat,

        /// <summary>
        /// Use basic authentication
        /// </summary>
        Basic,

        /// <summary>
        /// Use NTLM authentication
        /// </summary>
        Ntlm,

        /// <summary>
        /// Use intercative authentication prompt like in Visual Studio
        /// </summary>
        Interactive,
    }
}
