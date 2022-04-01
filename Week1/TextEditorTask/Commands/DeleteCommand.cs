namespace Week1.TextEditorTask.Commands
{
    public class DeleteCommand : ICommand
    {
        private Editor _editor;
        private char _char;

        public DeleteCommand(Editor editor)
        { 
            _editor = editor;
        }
        public void Execute()
        {
            _editor.DeleteChar();
            _char = _editor.currChar;
        }

        public void Redo()
        {
            _editor.DeleteChar();
        }

        public void Undo()
        {
            _editor.InsertChar(_char);
        }
    }
}
