namespace Week1.TextEditorTask
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
    }
}
