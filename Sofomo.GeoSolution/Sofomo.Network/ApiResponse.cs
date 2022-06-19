namespace Sofomo.Network;

public class ApiResponse
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public object Data { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ApiResponse"/> is result.
    /// </summary>
    /// <value>
    ///   <c>true</c> if result; otherwise, <c>false</c>.
    /// </value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
    public string[] Errors { get; set; }
}