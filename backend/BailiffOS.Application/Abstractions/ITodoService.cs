using BailiffOS.Domain;

namespace BailiffOS.Application.Abstractions;

/// <summary>
/// Application service abstraction for querying and mutating Todo items.
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Retrieves all todo items.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Read-only list of <see cref="TodoItem"/>.</returns>
    Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Adds a new todo item with the specified title.
    /// </summary>
    /// <param name="title">The title of the todo item.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created <see cref="TodoItem"/>.</returns>
    Task<TodoItem> AddAsync(string title, CancellationToken ct = default);
}
