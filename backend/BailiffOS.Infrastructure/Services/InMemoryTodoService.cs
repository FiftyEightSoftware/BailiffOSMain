using BailiffOS.Application.Abstractions;
using BailiffOS.Domain;

namespace BailiffOS.Infrastructure.Services;

/// <summary>
/// In-memory implementation of <see cref="ITodoService"/> used for development and testing.
/// Stores items in a local list for the lifetime of the application.
/// </summary>
public sealed class InMemoryTodoService : ITodoService
{
    private readonly List<TodoItem> _items = new();

    /// <summary>
    /// Returns all todo items currently stored in memory.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Read-only list of <see cref="TodoItem"/>.</returns>
    public Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult((IReadOnlyList<TodoItem>)_items.AsReadOnly());

    /// <summary>
    /// Creates and stores a new <see cref="TodoItem"/> with the provided title.
    /// </summary>
    /// <param name="title">The title of the todo item.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created <see cref="TodoItem"/>.</returns>
    public Task<TodoItem> AddAsync(string title, CancellationToken ct = default)
    {
        var item = new TodoItem(Guid.NewGuid(), title, false);
        _items.Add(item);
        return Task.FromResult(item);
    }
}
