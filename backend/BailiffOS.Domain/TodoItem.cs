namespace BailiffOS.Domain;

/// <summary>
/// Represents a task to be completed within the system.
/// </summary>
/// <param name="Id">Unique identifier for the todo item.</param>
/// <param name="Title">Short title describing the task.</param>
/// <param name="IsCompleted">Indicates whether the task is completed.</param>
public record TodoItem(Guid Id, string Title, bool IsCompleted);
