namespace DesignPatternsBootcamp.Behavioral.Command;

/// <summary>
/// The <b>Invoker</b> (provided): it runs commands and keeps the undo/redo history. Notice it works
/// against <see cref="ICommand"/> only — it has no idea what "add order" or "amend" actually do.
/// That decoupling is the whole point: this one class gives every action undo/redo for free.
/// </summary>
public sealed class BlotterHistory
{
    private readonly Stack<ICommand> _undo = new();
    private readonly Stack<ICommand> _redo = new();

    public bool CanUndo => _undo.Count > 0;
    public bool CanRedo => _redo.Count > 0;

    public void Do(ICommand command)
    {
        command.Execute();
        _undo.Push(command);
        _redo.Clear(); // a fresh action invalidates the redo branch
    }

    public void Undo()
    {
        if (_undo.Count == 0) return;
        ICommand command = _undo.Pop();
        command.Undo();
        _redo.Push(command);
    }

    public void Redo()
    {
        if (_redo.Count == 0) return;
        ICommand command = _redo.Pop();
        command.Execute();
        _undo.Push(command);
    }
}
